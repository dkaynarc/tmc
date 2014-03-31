using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Tmc.Scada.Core
{
    public static class XmlFactory
    {
        public static T LoadFromFile<T>(string filename) where T : class
        {
            T config = null;
            try
            {
                var reader = new StreamReader(filename);
                var serializer = new XmlSerializer(typeof(T));
                config = (T)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (XmlException ex)
            {
                throw new Exception("XML Parse Error: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("XML Deserialization Error: " + ex.Message);
            }
            return config;
        }

        public static void WriteToFile<T>(T config, string filename)
        {
            try
            {
                var serializer = new XmlSerializer(config.GetType());
                var writer = new StreamWriter(filename);
                serializer.Serialize(writer, config);
                writer.Close();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("XML Serialization Error: " + ex.Message);
            }
        }
    }
}
