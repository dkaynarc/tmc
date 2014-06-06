#region Header
/// FileName: ClusterFactory.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)
#endregion

#region UsingStatements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Tmc.Common;
using Tmc.Robotics;
using Tmc.Sensors;
using Tmc.Vision;
using System.Reflection;
using System.IO;
using TmcData;
#endregion

namespace Tmc.Scada.Core
{
    internal sealed class ClusterFactory
    {
        private readonly Dictionary<string, Type> TypeMap;

        private static ClusterFactory _instance;

        public static ClusterFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ClusterFactory();
                }
                return _instance;
            }
        }

        private ClusterFactory()
        {
            TypeMap = new Dictionary<string, Type>();
            BuildTypeMap();
        }

        /// <summary>
        /// Creates a TMC cluster and returns a configuration object that 
        /// encapsulates the hardware and controllers.
        /// </summary>
        /// <param name="fileName">Path to an XML file to parse</param>
        /// <returns>Cluster Configuration object encapsulating the created hardware and controllers.</returns>
        public ClusterConfig CreateCluster(string fileName)
        {
            var doc = XDocument.Load(fileName);
            var root = doc.Element("Cluster");
            var clusterTemplate = LoadClusterTemplate(root);
            return CreateClusterConfig(clusterTemplate);
        }

        private List<Assembly> LoadAllAssemblies()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Where(a => !a.IsDynamic).Select(a => a.Location).ToArray();

            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
            toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));
            return loadedAssemblies;
        }

        private void BuildTypeMap()
        {
            var assemblies = LoadAllAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.FullName.Contains("Tmc"))
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        var isHardware = typeof(IHardware).IsAssignableFrom(type);
                        if (isHardware && !(type.IsInterface || type.IsAbstract))
                        {
                            string name = type.Name.ToLower();
                            var attribs = type.GetCustomAttributes(typeof(NameAttribute), false);
                            if (attribs.Length > 0)
                            {
                                var attrib = attribs[0] as NameAttribute;
                                if (attrib != null)
                                {
                                    name = attrib.Name.ToLower();
                                }
                            }

                            TypeMap.Add(name, type);
                        }
                    }
                }
            }
        }

        private ClusterConfig CreateClusterConfig(ClusterTemplate template)
        {
            ClusterConfig config = new ClusterConfig();

            foreach (var hwTemplate in template.Hardware)
            {
                var hw = CreateHardware(hwTemplate.Type);

                try
                {
                    hw.SetParameters(hwTemplate.Parameters);
                    hw.Initialise();
                }
                catch(Exception ex)
                {
                    var outer = new InvalidOperationException(
                        String.Format("Failed to initialise hardware item Type: {0}, Name: {1}",
                        hw.GetType().ToString(), hw.Name), ex);

                    Logger.Instance.Write(new LogEntry(outer));
                    throw outer;
                }

                if (hw is IRobot)
                {
                    config.Robots.Add(hw.GetType(), hw as IRobot);
                }
                else if (hw is IConveyor)
                {
                    config.Conveyors.Add(hw.GetType(), hw as IConveyor);
                }
                else if (hw is ISensor)
                {
                    config.Sensors.Add(hw.Name, hw as ISensor);
                }
                else if (hw is ICamera)
                {
                    config.Cameras.Add(hw.Name, hw as ICamera);
                }
                else if (hw is IPlc)
                {
                    config.Plcs.Add(hw.Name, hw as IPlc);
                }
            }
             
            config.Controllers = CreateControllers(config);

            return config;
        }

        private Dictionary<Type, IController> CreateControllers(ClusterConfig config)
        {
            var controllers = new Dictionary<Type, IController>();

            controllers.Add(typeof(Assembler), new Assembler(config));
            controllers.Add(typeof(ConveyorController), new ConveyorController(config));
            controllers.Add(typeof(Loader), new Loader(config));
            controllers.Add(typeof(Sorter), new Sorter(config));
            controllers.Add(typeof(TrayVerifier), new TrayVerifier(config));

            return controllers;
        }

        private IHardware CreateHardware(Type type)
        {
            var typeSwitch = new Dictionary<Type, Func<IHardware>>
            {
                { typeof(ICamera),      () => { return CreateCamera() as IHardware; }},
                { typeof(ISensor),      () => { return CreateSensor() as IHardware; }},
                { typeof(IRobot),       () => { return CreateRobot(type) as IHardware; }},
                { typeof(IConveyor),    () => { return CreateConveyor(type) as IHardware; }},
                { typeof(IPlc),         () => { return CreatePlc() as IHardware; }}
            };

            var interfaces = type.GetInterfaces();
            IHardware hwInstance = null;
            foreach (var iface in interfaces)
            {
                Func<IHardware> factory;
                if (typeSwitch.TryGetValue(iface, out factory))
                {
                    hwInstance = factory();
                }
            }

            return hwInstance;
        }

        private ICamera CreateCamera()
        {
            return new Camera();
        }

        private ISensor CreateSensor()
        {
            return new Sensor();
        }

        private IRobot CreateRobot(Type type)
        {
            return RobotFactory.CreateRobot(type);
        }

        private IConveyor CreateConveyor(Type type)
        {
            return ConveyorFactory.CreateConveyor(type);
        }

        private IPlc CreatePlc()
        {
            return new Plc();
        }

        private ClusterTemplate LoadClusterTemplate(XElement xElement)
        {
            string name = null;
            var xmlName = xElement.Attribute("Name");
            if (xmlName != null)
            {
                name = xmlName.Value;
            }

            var clusterTemplate = new ClusterTemplate(name);
            clusterTemplate.Hardware = LoadHardwareTemplates(xElement.Elements("Hardware"));
            return clusterTemplate;
        }

        private List<HardwareTemplate> LoadHardwareTemplates(IEnumerable<XElement> xHardwareElements)
        {
            var templates = new List<HardwareTemplate>();
            foreach (var xHardwareElement in xHardwareElements)
            {
                var type = TypeMap[xHardwareElement.Attribute("Type").Value.ToLower()];
                var hardwareTemplate = new HardwareTemplate(type);
                var attribParams = xHardwareElement.Attributes();
                foreach (var param in attribParams)
                {
                    if (param.Name != "Type")
                    {
                        hardwareTemplate.Parameters.Add(param.Name.LocalName, param.Value);
                    }
                }

                var bodyParams = xHardwareElement.Elements();

                foreach (var param in bodyParams)
                {
                    hardwareTemplate.Parameters.Add(param.Name.LocalName, param.Value);
                }

                templates.Add(hardwareTemplate);
            }
            return templates;
        }
    }
}
