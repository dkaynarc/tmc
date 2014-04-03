using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tmc.Scada.Core
{
    internal static class ClusterFactory
    {
        private static ClusterConfig CreateClusterConfig(string filename)
        {
            ClusterConfig config = null;
            try
            {
                config = XmlFactory.LoadFromFile<ClusterConfig>(filename);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading cluster config" + ex.Message);
            }
            return config;
        }
    }
}
