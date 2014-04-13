using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using Tmc.Common;
using Tmc.Robotics;
using Tmc.Vision;
using Tmc.Sensors;

namespace Tmc.Scada.Core
{
    internal static class ClusterFactory
    {
        private static readonly Dictionary<string, Type> HardwareMapping;
        private static readonly Dictionary<string, ClusterTemplate> ClusterTemplates;
        
        static ClusterFactory()
        {
            HardwareMapping = new Dictionary<string, Type>();
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

        public static ClusterConfig CreateCluster(string fileName)
        {
            var doc = XDocument.Load(fileName);
            var root = doc.Element("Plant");
            var templateName = root.Attribute("Name").Value;
            var clusterElement = root.Element("Cluster");
            var clusterTemplate = LoadClusterTemplate(clusterElement);
            return CreateClusterConfig(clusterTemplate);
        }

        private static ClusterConfig CreateClusterConfig(ClusterTemplate template)
        {
            ClusterConfig config = new ClusterConfig();

            foreach (var hwTemplate in template.Hardware)
            {
                
            }

            return config;
        }

        private static T CreateHardware<T>() where T : class, IHardware
        {
            var typeSwitch = new Dictionary<Type, IHardware>
            {
                { typeof(ICamera),      CreateCamera() },
                { typeof(ISensor),      CreateSensor() },
                { typeof(IRobot),       CreateRobot(typeof(T)) },
                { typeof(IConveyor),    CreateConveyor(typeof(T))}
            };

            return typeSwitch[typeof(T)] as T;
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

        private static IRobot CreateRobot(Type t)
        {
            throw new NotImplementedException();
        }

        private static IConveyor CreateConveyor(Type t)
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
