using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Robotics;

namespace Tmc.Scada.Core
{
    public enum ConveyorType
    {
        Assembly,
        Sorter
    }

    public enum ConveyorAction
    {
        MoveForward,
        MoveBackward
    }

    public class ConveyorController : ControllerBase
    {
        private Dictionary<ConveyorType, Type> _conveyorTypeMap;
        private Dictionary<ConveyorAction, Action<IConveyor>> _conveyorActionMap;

        public ConveyorController(ClusterConfig config) : base(config)
        {
            _conveyorTypeMap = new Dictionary<ConveyorType,Type> 
            {
                { ConveyorType.Assembly,    typeof(BluetoothConveyor) }, // TODO: Replace with correct type
                { ConveyorType.Sorter,      typeof(BluetoothConveyor) }
            };

            _conveyorActionMap = new Dictionary<ConveyorAction, Action<IConveyor>>
            {
                { ConveyorAction.MoveForward,   x => x.MoveForward() },
                { ConveyorAction.MoveBackward,  x => x.MoveBackward() }
            };
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as ConveyorControllerParams;
            if (p != null)
            {
                _conveyorActionMap[p.ConveyorAction](_conveyorTypeMap[p.ConveyorType] as IConveyor);
            }
        }

        public bool CanMoveForward(ConveyorType c)
        {
            return true;
        }

        public bool CanMoveBackward(ConveyorType c)
        {
            return true;
        }
    }

    public class ConveyorControllerParams : ControllerParams
    {
        public ConveyorType ConveyorType { get; set; }
        public ConveyorAction ConveyorAction { get; set; }
    }
}
