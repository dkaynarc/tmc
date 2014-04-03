using System.Collections.Generic;

namespace Tmc.Scada.Core.Sequencing
{
    public abstract class FSMState
    {
        public string Name { get; set; }
        public Dictionary<string, string> Transitions { get; set; }
        public FSMSequencer FSMController { get; internal set; }

        public FSMState()
        {
            Transitions = new Dictionary<string, string>();
        }

        public virtual void Initialise() { }

        public virtual void Begin() { }

        public virtual string Update() { return null; }

        public virtual void End() { }

        public virtual void Destroy() { }

        public abstract void SetParameters(Dictionary<string, string> parameters);
    }
}
