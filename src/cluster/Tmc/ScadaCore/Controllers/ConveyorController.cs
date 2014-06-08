using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Robotics;
using Appccelerate.StateMachine;
using System.Threading;

namespace Tmc.Scada.Core
{
    public enum ConveyorType
    {
        Assembly,
        Sorting
    }

    public enum ConveyorAction
    {
        MoveForward,
        MoveBackward
    }

    public sealed class ConveyorController : ControllerBase
    {
        private SerialConveyor _serialConveyor;
        private BluetoothConveyor _bluetoothConveyor;

        private Dictionary<ConveyorAction, Action<IConveyor>> _actionMap;
        private Dictionary<ConveyorType, IConveyor> _conveyorTypeMap;

        public ConveyorController(ClusterConfig config) : base(config)
        {
            _serialConveyor = config.Conveyors[typeof(SerialConveyor)] as SerialConveyor;
            _bluetoothConveyor = config.Conveyors[typeof(BluetoothConveyor)] as BluetoothConveyor;

            _conveyorTypeMap = new Dictionary<ConveyorType, IConveyor>()
            {
                {ConveyorType.Sorting, _bluetoothConveyor},
                {ConveyorType.Assembly, _serialConveyor}
            };

            _actionMap = new Dictionary<ConveyorAction, Action<IConveyor>>()
            {
                {ConveyorAction.MoveForward, x => x.MoveForward() },
                {ConveyorAction.MoveBackward, x => x.MoveBackward() }
            };
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as ConveyorControllerParams;
            if (p != null)
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    MoveConveyorAsync(p.ConveyorType, p.ConveyorAction);
                }
            }
        }

        public override void Cancel()
        {
        }

        public ConveyorPosition GetCurrentPosition(ConveyorType type)
        {
            return _conveyorTypeMap[type].Position;
        }

        public bool CanMoveForward(ConveyorType type)
        {
            if (type == ConveyorType.Assembly)
            {
                return (_conveyorTypeMap[type].Position == ConveyorPosition.Right) ||
                    (_conveyorTypeMap[type].Position == ConveyorPosition.Middle);
            }
            else
            {
                return _conveyorTypeMap[type].Position == ConveyorPosition.Right;
            }
        }

        public bool CanMoveBackward(ConveyorType type)
        {
            if (type == ConveyorType.Assembly)
            {
                return (_conveyorTypeMap[type].Position == ConveyorPosition.Middle) ||
                    (_conveyorTypeMap[type].Position == ConveyorPosition.Left);
            }
            else
            {
                return _conveyorTypeMap[type].Position == ConveyorPosition.Left;
            }
        }

        private void MoveConveyorAsync(ConveyorType type, ConveyorAction action)
        {
            Task.Run(() =>
                {
                    _actionMap[action](_conveyorTypeMap[type] as IConveyor);
                    if (type == ConveyorType.Assembly)
                    {
                        Thread.Sleep(4000);
                    }
                    IsRunning = false;
                    OnCompleted(new ControllerEventArgs() { OperationStatus = ControllerOperationStatus.Succeeded });
                });
        }
    }

    public class ConveyorControllerParams : ControllerParams
    {
        public ConveyorType ConveyorType { get; set; }
        public ConveyorAction ConveyorAction { get; set; }
    }
}
