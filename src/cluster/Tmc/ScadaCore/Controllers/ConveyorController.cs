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

    public enum ConveyorPosition
    {
        Left,
        Middle,
        Right
    }

    public class ConveyorController : ControllerBase
    {
        private Dictionary<ConveyorType, Type> _conveyorTypeMap;
        private Dictionary<ConveyorAction, Action<IConveyor>> _conveyorActionMap;
        private Dictionary<ConveyorType, ConveyorPosition> _conveyorPositionMap;
        //private Dictionary<ConveyorType, PassiveStateMachine<ConveyorPosition, ConveyorAction>> _fsmMap;

        public ConveyorController(ClusterConfig config) : base(config)
        {
            _conveyorTypeMap = new Dictionary<ConveyorType,Type> 
            {
                { ConveyorType.Assembly, typeof(BluetoothConveyor) }, // TODO: Replace with correct type
                { ConveyorType.Sorting, typeof(BluetoothConveyor) }
            };

            _conveyorActionMap = new Dictionary<ConveyorAction, Action<IConveyor>>
            {
                { ConveyorAction.MoveForward, x => x.MoveForward() },
                { ConveyorAction.MoveBackward, x => x.MoveBackward() }
            };

            _conveyorPositionMap = new Dictionary<ConveyorType, ConveyorPosition>
            {
                { ConveyorType.Assembly, ConveyorPosition.Right },
                { ConveyorType.Sorting, ConveyorPosition.Right}
            };
            
            //_fsmMap = new Dictionary<ConveyorType, PassiveStateMachine<ConveyorPosition, ConveyorAction>>();
            //_fsmMap.Add(ConveyorType.Assembly, CreateStateMachine());
            //_fsmMap.Add(ConveyorType.Sorting, CreateStateMachine());
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as ConveyorControllerParams;
            if (p != null)
            {
                if (!IsRunning)
                {
                    var task = new Task(() => MoveConveyor(p.ConveyorType, p.ConveyorAction));
                    task.Start();
                    IsRunning = true;
                }
            }
        }

        public ConveyorPosition GetCurrentPosition(ConveyorType type)
        {
            return _conveyorPositionMap[type];
        }

        private void UpdatePosition(ConveyorType type, ConveyorAction action)
        {
            if (type == ConveyorType.Assembly)
            {
                if (action == ConveyorAction.MoveForward)
                { 
                    if (GetCurrentPosition(type) == ConveyorPosition.Right)
                    {
                        _conveyorPositionMap[type] = ConveyorPosition.Middle;
                    }
                    else if (GetCurrentPosition(type) == ConveyorPosition.Middle)
                    {
                        _conveyorPositionMap[type] = ConveyorPosition.Left;
                    }
                }
                else if (action == ConveyorAction.MoveBackward)
                {
                    if (GetCurrentPosition(type) == ConveyorPosition.Left)
                    {
                        _conveyorPositionMap[type] = ConveyorPosition.Middle;
                    }
                    else if (GetCurrentPosition(type) == ConveyorPosition.Middle)
                    {
                        _conveyorPositionMap[type] = ConveyorPosition.Right;
                    }
                }
            }
            else if (type == ConveyorType.Sorting)
            {
                if (action == ConveyorAction.MoveForward)
                {
                    _conveyorPositionMap[type] = ConveyorPosition.Left;
                }
                else if (action == ConveyorAction.MoveBackward)
                {
                    _conveyorPositionMap[type] = ConveyorPosition.Right;
                }
            }
        }

        public bool CanMoveForward(ConveyorType type)
        {
            if (type == ConveyorType.Assembly)
            {
                return (_conveyorPositionMap[type] == ConveyorPosition.Right) ||
                    (_conveyorPositionMap[type] == ConveyorPosition.Middle);
            }
            else
            {
                return _conveyorPositionMap[type] == ConveyorPosition.Right;
            }
        }

        public bool CanMoveBackward(ConveyorType type)
        {
            if (type == ConveyorType.Assembly)
            {
                return (_conveyorPositionMap[type] == ConveyorPosition.Middle) ||
                    (_conveyorPositionMap[type] == ConveyorPosition.Left);
            }
            else
            {
                return _conveyorPositionMap[type] == ConveyorPosition.Left;
            }
        }

        //private PassiveStateMachine<ConveyorPosition, ConveyorAction> CreateStateMachine()
        //{
        //    var fsm = new PassiveStateMachine<ConveyorPosition, ConveyorAction>();

        //    fsm.In(ConveyorPosition.Right)
        //        .On(ConveyorAction.MoveForward)
        //            .If<ConveyorType>(type => type == ConveyorType.Assembly)
        //                .Goto(ConveyorPosition.Middle)
        //            .Otherwise()
        //                .Goto(ConveyorPosition.Left);

        //    fsm.In(ConveyorPosition.Middle)
        //        .On(ConveyorAction.MoveForward)
        //            .Goto(ConveyorPosition.Left)
        //        .On(ConveyorAction.MoveBackward)
        //            .Goto(ConveyorPosition.Right);

        //    fsm.In(ConveyorPosition.Left)
        //        .On(ConveyorAction.MoveBackward)
        //            .If<ConveyorType>(type => type == ConveyorType.Assembly)
        //                .Goto(ConveyorPosition.Middle)
        //            .Otherwise()
        //                .Goto(ConveyorPosition.Right);

        //    fsm.Initialize(ConveyorPosition.Right);

        //    return fsm;
        //}

        private void MoveConveyor(ConveyorType type, ConveyorAction action)
        {
            _conveyorActionMap[action](_conveyorTypeMap[type] as IConveyor);
            OnCompleted(new EventArgs());
            IsRunning = false;
        }
    }

    public class ConveyorControllerParams : ControllerParams
    {
        public ConveyorType ConveyorType { get; set; }
        public ConveyorAction ConveyorAction { get; set; }
    }
}
