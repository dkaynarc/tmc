using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Robotics;
using Tmc.Vision;
using Tmc.Sensors;

namespace Tmc.Scada.Core
{
    internal class ClusterConfig
    {
        public string Name;
        public Dictionary<string, IRobot> Robots;
        public Dictionary<string, IConveyor> Conveyors;
        public Dictionary<string, ICamera> Cameras;
        public Dictionary<string, ISensor> Sensors;
        
        ClusterConfig()
        {
            Name = "";
            Robots = new Dictionary<string, IRobot>();
            Conveyors = new Dictionary<string, IConveyor>();
            Cameras = new Dictionary<string, ICamera>();
            Sensors = new Dictionary<string, ISensor>();
        }
    }
}
