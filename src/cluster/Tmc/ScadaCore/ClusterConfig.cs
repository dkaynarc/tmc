#region Header
/// FileName: ClusterConfig.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)
#endregion

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
        public Dictionary<string, ISensor> Sensors;
        public Dictionary<string, IPlc> Plcs;
        public Dictionary<Type, IController> Controllers;
        
        public ClusterConfig()
        {
            Name = "";
            Robots = new Dictionary<Type, IRobot>();
            Conveyors = new Dictionary<Type, IConveyor>();
            Cameras = new Dictionary<string, ICamera>();
            Sensors = new Dictionary<string, ISensor>();
            Plcs = new Dictionary<string, IPlc>();
            Controllers = new Dictionary<Type, IController>();
        }

        public List<IHardware> GetAllHardware()
        {
            var hardware = new List<IHardware>();
            hardware.AddRange(Robots.Values.ToList());
            hardware.AddRange(Conveyors.Values.ToList());
            hardware.AddRange(Cameras.Values.ToList());
            hardware.AddRange(Sensors.Values.ToList());
            hardware.AddRange(Plcs.Values.ToList());

            return hardware;
        }
    }
}
