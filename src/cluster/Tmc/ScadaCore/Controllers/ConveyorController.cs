using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Robotics;
using Appccelerate.StateMachine;

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

        private Dictionary<ConveyorType, PassiveStateMachine<ConveyorPosition, ConveyorAction>> _fsmMap;

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

            _fsmMap = new Dictionary<ConveyorType, PassiveStateMachine<ConveyorPosition, ConveyorAction>>();
            _fsmMap.Add(ConveyorType.Assembly, CreateStateMachine(ConveyorType.Assembly));
            _fsmMap.Add(ConveyorType.Sorting, CreateStateMachine(ConveyorType.Sorting));
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as ConveyorControllerParams;
            if (p != null)
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    //_fsmMap[p.ConveyorType].Fire(p.ConveyorAction, p.ConveyorType);
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

        private PassiveStateMachine<ConveyorPosition, ConveyorAction> CreateStateMachine(ConveyorType conveyorType)
        {
            var fsm = new PassiveStateMachine<ConveyorPosition, ConveyorAction>();

            fsm.In(ConveyorPosition.Right)
                .On(ConveyorAction.MoveForward)
                    .If<ConveyorType>(type => type == ConveyorType.Assembly)
                        .Goto(ConveyorPosition.Middle)
                        .Execute(() => MoveConveyorAsync(conveyorType, ConveyorAction.MoveForward))
                    .Otherwise()
                        .Goto(ConveyorPosition.Left)
                        .Execute(() => MoveConveyorAsync(conveyorType, ConveyorAction.MoveForward));

            fsm.In(ConveyorPosition.Middle)
                .On(ConveyorAction.MoveForward)
                    .Goto(ConveyorPosition.Left)
                    .Execute(() => MoveConveyorAsync(conveyorType, ConveyorAction.MoveForward))
                .On(ConveyorAction.MoveBackward)
                    .Goto(ConveyorPosition.Right)
                    .Execute(() => MoveConveyorAsync(conveyorType, ConveyorAction.MoveBackward));

            fsm.In(ConveyorPosition.Left)
                .On(ConveyorAction.MoveBackward)
                    .If<ConveyorType>(type => type == ConveyorType.Assembly)
                        .Goto(ConveyorPosition.Middle)
                        .Execute(() => MoveConveyorAsync(conveyorType, ConveyorAction.MoveBackward))
                    .Otherwise()
                        .Goto(ConveyorPosition.Right)
                        .Execute(() => MoveConveyorAsync(conveyorType, ConveyorAction.MoveBackward));

            fsm.Initialize(ConveyorPosition.Right);

            return fsm;
        }

        private void MoveConveyorAsync(ConveyorType type, ConveyorAction action)
        {
            Task.Run(() =>
                {
                    _actionMap[action](_conveyorTypeMap[type] as IConveyor);
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
