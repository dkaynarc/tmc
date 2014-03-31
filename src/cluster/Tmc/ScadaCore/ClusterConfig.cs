using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    internal class ClusterConfig
    {
        public string Name;
        public Dictionary<string, IRobot> Robots;
        public Dictionary<string, ICamera> Cameras;
        
        ClusterConfig()
        {
            Name = "";
            Robots = new Dictionary<string, IRobot>();
            Cameras = new Dictionary<string, ICamera>();
        }
    }
}
