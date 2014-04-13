using System.Collections.Generic;

namespace Tmc.Robotics
{
    public class LoaderRobot : IRobot
    {
        public string Name { get; set; }

        public Common.HardwareStatus GetStatus()
        {
            return Common.HardwareStatus.Offline;
        }

        public void SetParameters(Dictionary<string, string> parameters)
        {

        }
    }
}
