#region Header

/// FileName: CalibrationDatSerializer.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)

#endregion Header

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tmc.Common
{
    public static class CalibrationDataSerializer
    {
        public static void Serialize(ICalibrationData template, string fileName)
        {
            try
            {
                var serializer = new BinaryFormatter();
                var stream = new FileStream(fileName, FileMode.Create);
                serializer.Serialize(stream, template);
                stream.Flush();
                stream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to serialize to file: {0}", ex.Message), ex);
            }
        }

        public static ICalibrationData Deserialize(string fileName)
        {
            ICalibrationData template = null;

            try
            {
                var serializer = new BinaryFormatter();
                var stream = new FileStream(fileName, FileMode.Open);
                template = serializer.Deserialize(stream) as ICalibrationData;
                stream.Close();
            }
            catch (SerializationException)
            {
                return template;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Unable to deserialize file: {0}", ex.Message), ex);
            }

            return template;
        }
    }
}