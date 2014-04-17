using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core.Sequencing
{
    public class StateTransition
    {
        readonly FSMState CurrentState;
        readonly string Command;

        public StateTransition(FSMState currentState, string command)
        {
            this.CurrentState = currentState;
            this.Command = command;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            StateTransition other = obj as StateTransition;
            return (other != null) && (this.CurrentState == other.CurrentState);
        }
    }
}
