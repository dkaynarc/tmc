using System;
using System.Collections.Generic;

namespace Tmc.Scada.Core.Sequencing
{
    public class FSMState
    {
        public string Name { get; set; }
        public Dictionary<StateTransition, FSMState> Transitions { get; set; }
        public FSMSequencer FSMController { get; internal set; }

        public Action UpdateAction;

        public FSMState(Action updateAction)
        {
            Transitions = new Dictionary<StateTransition, FSMState>();
            this.UpdateAction = updateAction;
        }

        public virtual void Initialise() { }

        public virtual void Begin() { }

        public virtual void Update() 
        {
            if (UpdateAction == null)
            {
                throw new InvalidOperationException();
            }
        }

        public virtual FSMState GetNext(string command)
        {
            var transition = new StateTransition(this, command);
            FSMState nextState;
            if (!Transitions.TryGetValue(transition, out nextState))
            {
                throw new InvalidOperationException("Invalid transition: " + this + " -> " + command);
            }
            return nextState;
        }

        public virtual void End() { }

        public virtual void Destroy() { }

        public virtual void SetParameters(Dictionary<string, string> parameters) { }
    }
}
