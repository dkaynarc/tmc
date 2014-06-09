using Tmc.Common;

namespace Tmc.Robotics
{
    public interface IConveyor : IHardware
    {
        ConveyorPosition Position { get; set; }
        void MoveForward();
        void MoveBackward();
    }
}
