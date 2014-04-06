using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Robotics;

namespace Tmc.Scada.Core
{
    public class RobotManager
    {
        private static RobotManager _instance;
        public RobotManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RobotManager();
                }
                return _instance;
            }
        }

        private Dictionary<Type, IRobot> _robots;

        public void Add(IRobot robot)
        {
            var type = robot.GetType();
            if (!_robots.ContainsKey(type))
            {
                _robots.Add(type, robot);
            }
        }

        public void Remove(IRobot robot)
        {
            var type = robot.GetType();
            if (_robots.ContainsKey(type))
            {
                _robots.Remove(type);
            }
        }

        // TODO: Add proxies for each method exposed in IRobot
        // where there is a need to invoke on all robots. 
    }
}
