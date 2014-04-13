namespace Tmc.Robotics
{
    public class LoaderRobot : IRobot
    {
        public string Name { get; set; }

        public Common.HardwareStatus GetStatus()
        {
            return Common.HardwareStatus.Offline;
        }
    }
}
