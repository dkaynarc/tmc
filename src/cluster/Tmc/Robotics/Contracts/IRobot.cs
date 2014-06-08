using Tmc.Common;

namespace Tmc.Robotics
{
    public interface IRobot : IHardware
    {
        void SetSpeed(int speed);
    }
}
