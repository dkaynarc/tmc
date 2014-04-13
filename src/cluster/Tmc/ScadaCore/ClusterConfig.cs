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
    public class ClusterConfig
    {
        public string Name;
        public Dictionary<Type, IRobot> Robots;
        public Dictionary<string, IConveyor> Conveyors;
        public Dictionary<string, ICamera> Cameras;
        public Dictionary<Type, ISensor> Sensors;
        public Dictionary<Type, IActivityController> ActivityController;
        
        ClusterConfig()
        {
            Name = "";
            Robots = new Dictionary<Type, IRobot>();
            Conveyors = new Dictionary<string, IConveyor>();
            Cameras = new Dictionary<string, ICamera>();
            Sensors = new Dictionary<Type, ISensor>();
            ActivityController = new Dictionary<Type, IActivityController>();
        }
    }
}
