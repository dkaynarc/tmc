using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Tmc.Scada.Core.Sequencing
{
    public class FSMSequencer : ISequencer
    {
        private enum FSMStatePhase
        {
            Begin = 0,
            Update,
            End
        }

        public string Name { get; set; }

        public bool Enabled { get; private set; }
        public Dictionary<string, FSMState> States { get { return _states; } set { _states = value; } }

        private ScadaEngine _engine;
        private FSMState _currState;
        private FSMState _nextState;
        private Dictionary<string, FSMState> _states;
        private FSMStatePhase _currFSMPhase;
        private static Dictionary<string, Type> _stateMapping = null;


        public FSMSequencer(ScadaEngine engine)
        {
            _engine = engine;
            _currState = null;
            _nextState = _currState;
            _states = new Dictionary<string, FSMState>();
        }

        public void Start()
        {
            Enabled = true;
        }

        public void Stop()
        {
            Enabled = false;
        }

        public void Load(Dictionary<StateTransition, FSMState> transitionTable)
        {
            
        }

        public void Destroy()
        {
            foreach (var state in _states.Values)
            {
                state.Destroy();
            }
        }

        public void Update()
        {
            if (Enabled)
            {
                switch (_currFSMPhase)
                {
                    case FSMStatePhase.Begin:
                        _currState.Begin();
                        _currFSMPhase = FSMStatePhase.Update;
                        break;
                    case FSMStatePhase.Update:
                        //SetNextState(_currState.Update());
                        break;
                    case FSMStatePhase.End:
                        _currState.End();
                        _currFSMPhase = FSMStatePhase.Begin;
                        _currState = _nextState;
                        break;
                    default:
                        throw new System.InvalidOperationException("Unknown FSMStatePhase");
                }
            }
        }

        private void MoveNext(string nextState)
        {
            if (!string.IsNullOrEmpty(nextState) && nextState != _currState.Name)
            {
                if (_states.ContainsKey(nextState))
                {
                    _nextState = _states[nextState];
                    _currFSMPhase = FSMStatePhase.End;
                }
                else
                {
                    throw new System.ArgumentException("nextState does not exist in states collection");
                }
            }
        }
    }
}
