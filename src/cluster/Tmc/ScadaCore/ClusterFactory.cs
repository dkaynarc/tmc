using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Tmc.Common;
using Tmc.Robotics;
using Tmc.Sensors;
using Tmc.Vision;

namespace Tmc.Scada.Core
{
    internal static class ClusterFactory
    {
        private static readonly Dictionary<string, Type> HardwareMapping;
        
        static ClusterFactory()
        {
            HardwareMapping = new Dictionary<string, Type>();
            BuildMappings();
        }

        /// <summary>
        /// Creates a TMC cluster and returns a configuration object that 
        /// encapsulates the hardware and controllers.
        /// </summary>
        /// <param name="fileName">Path to an XML file to parse</param>
        /// <returns>Cluster Configuration object encapsulating the created hardware and controllers.</returns>
        public static ClusterConfig CreateCluster(string fileName)
        {
            var doc = XDocument.Load(fileName);
            var root = doc.Element("Cluster");
            var templateName = root.Attribute("Name").Value;
            var clusterTemplate = LoadClusterTemplate(root);
            return CreateClusterConfig(clusterTemplate);
        }

        private static void BuildMappings()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.FullName.Contains("Tmc"))
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        var isHardware = typeof(IHardware).IsAssignableFrom(type);
                        if (isHardware)
                        {
                            string name = type.FullName;
                            var attribs = type.GetCustomAttributes(typeof(NameAttribute), false);
                            if (attribs.Length > 0)
                            {
                                var attrib = attribs[0] as NameAttribute;
                                if (attrib != null)
                                {
                                    name = attrib.Name.ToLower();
                                }

                                HardwareMapping.Add(name, type);
                            }
                        }
                    }
                }
            }
        }

        private static ClusterConfig CreateClusterConfig(ClusterTemplate template)
        {
            ClusterConfig config = new ClusterConfig();

            foreach (var hwTemplate in template.Hardware)
            {
                var hw = CreateHardware(hwTemplate.Type);

                if (hw is IRobot)
                {
                    config.Robots.Add(hw.GetType(), hw as IRobot);
                }

                if (hw is IConveyor)
                {
                    config.Conveyors.Add(hw.GetType(), hw as IConveyor);
                }

                if (hw is ISensor)
                {
                    config.Sensors.Add(hw.GetType(), hw as ISensor);
                }

                if (hw is ICamera)
                {
                    config.Cameras.Add(hw.Name, hw as ICamera);
                }
                
                hw.SetParameters(hwTemplate.Parameters);
                hw.Initialise();
            }

            config.Controllers = CreateControllers(config);

            return config;
        }

        private static Dictionary<Type, IController> CreateControllers(ClusterConfig config)
        {
            var controllers = new Dictionary<Type, IController>();

            controllers.Add(typeof(Assembler), new Assembler(config));
            controllers.Add(typeof(ConveyorController), new ConveyorController(config));
            controllers.Add(typeof(Loader), new Loader(config));
            controllers.Add(typeof(Palletiser), new Palletiser(config));
            controllers.Add(typeof(Sorter), new Sorter(config));
            controllers.Add(typeof(TrayVerifier), new TrayVerifier(config));

            return controllers;
        }

        private static IHardware CreateHardware(Type type)
        {
            var typeSwitch = new Dictionary<Type, Func<IHardware>>
            {
                { typeof(ICamera),      () => { return CreateCamera(); }},
                { typeof(ISensor),      () => { return CreateSensor(); }},
                { typeof(IRobot),       () => { return CreateRobot(type); }},
                { typeof(IConveyor),    () => { return CreateConveyor(); }}
            };

            return typeSwitch[type]();
        }

        private static ICamera CreateCamera()
        {
            // return new Camera();
            throw new NotImplementedException();
        }

        private static ISensor CreateSensor()
        {
            throw new NotImplementedException();
        }

        private static IRobot CreateRobot(Type type)
        {
            return RobotFactory.CreateRobot(type);
        }

        private static IConveyor CreateConveyor()
        {
            throw new NotImplementedException();
        }

        private static ClusterTemplate LoadClusterTemplate(XElement xElement)
        {
            string name = null;
            var xmlName = xElement.Attribute("Name");
            if (xmlName != null)
            {
                name = xmlName.Value;
            }

            var clusterTemplate = new ClusterTemplate(name);
            var hardwareTemplates = LoadHardwareTemplates(xElement.Elements("Hardware"));
            return clusterTemplate;
        }

        private static List<HardwareTemplate> LoadHardwareTemplates(IEnumerable<XElement> xHardwareElements)
        {
            var templates = new List<HardwareTemplate>();
            foreach (var xHardwareElement in xHardwareElements)
            {
                var type = HardwareMapping[xHardwareElement.Attribute("Type").Value.ToLower()];
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
