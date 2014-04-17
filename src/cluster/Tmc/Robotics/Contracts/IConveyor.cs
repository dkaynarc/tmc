using Tmc.Common;

namespace Tmc.Robotics
{
    public interface IConveyor : IHardware
    {
        void MoveForward();
        void MoveBackward();
    }
}
