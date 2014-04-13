using System;
using System.Collections.Generic;
using System.Linq;
using Tmc.Common;
using Tmc.Robotics;
using Tmc.Sensors;
using Tmc.Vision;

namespace Tmc.Scada.Core
{
    public class ClusterConfig
    {
        public string Name;
        public Dictionary<Type, IRobot> Robots;
        public Dictionary<Type, IConveyor> Conveyors;
        public Dictionary<string, ICamera> Cameras;
        public Dictionary<Type, ISensor> Sensors;
        public Dictionary<Type, IController> Controllers;
        
        public ClusterConfig()
        {
            Name = "";
            Robots = new Dictionary<Type, IRobot>();
            Conveyors = new Dictionary<Type, IConveyor>();
            Cameras = new Dictionary<string, ICamera>();
            Sensors = new Dictionary<Type, ISensor>();
            Controllers = new Dictionary<Type, IController>();
        }

        public List<IHardware> GetAllHardware()
        {
            var hardware = new List<IHardware>();
            hardware.AddRange(Robots.Values.ToList());
            hardware.AddRange(Conveyors.Values.ToList());
            hardware.AddRange(Cameras.Values.ToList());
            hardware.AddRange(Sensors.Values.ToList());
            return hardware;
        }
    }
}
