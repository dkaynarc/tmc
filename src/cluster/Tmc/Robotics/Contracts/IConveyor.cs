using Tmc.Common;

namespace Tmc.Robotics
{
    public interface IConveyor : IHardware
    {
        ConveyorPosition Position { get; }
        void MoveForward();
        void MoveBackward();
    }
}
