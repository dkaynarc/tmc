namespace Tmc.Robotics
{
    public class SorterRobot : IRobot
    {
        public string Name { get; set; }

        public Common.HardwareStatus GetStatus()
        {
            return Common.HardwareStatus.Offline;
        }
    }
}
