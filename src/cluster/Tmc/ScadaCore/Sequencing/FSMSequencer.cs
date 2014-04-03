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

        private FSMState _currState;
        private FSMState _nextState;
        private Dictionary<string, FSMState> _states;
        private FSMStatePhase _currFSMPhase;
        private static Dictionary<string, Type> _stateMapping = null;

        private struct FSMTemplate
        {
            public Type Type;
            public Dictionary<string, string> Parameters;

            public FSMTemplate(Type type)
            {
                Type = type;
                Parameters = new Dictionary<string, string>();
            }
        }

        public FSMSequencer()
        {
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

        public void Load(string filename)
        {
            
            if (File.Exists(filename))
            {
                this.LoadFromXMLFile(filename);
                _currFSMPhase = FSMStatePhase.Begin;
            }
            else
            {
                throw new ArgumentException("File not found: " + filename);
            }
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
                        SetNextState(_currState.Update());
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

        private void SetNextState(string nextState)
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

        #region Loading

        private void LoadFromXMLFile(string filename)
        {
            if (_stateMapping == null)
                BuildMappings();

            var doc = XDocument.Load(filename);
            var root = doc.Element("fsm");
            Name = root.Attribute("name").Value;
            var initialStateName = root.Attribute("initialState").Value;
            var xStates = root.Elements("state");

            foreach (var xState in xStates)
            {
                var state = BuildState(xState);
                state.FSMController = this;
                state.Initialise();
                _states.Add(state.Name, state);
            }

            if (_states.ContainsKey(initialStateName))
                _currState = _states[initialStateName];
            else
                throw new System.InvalidOperationException("No InitialState defined in file");
        }

        public static FSMState BuildState(XElement xml)
        {
            var name = xml.Attribute("name").Value;
            var sType = _stateMapping[name.ToLower()];
            var state = Activator.CreateInstance(sType) as FSMState;
            state.Name = name;
            foreach (var xTransition in xml.Elements("transition"))
            {
                var condition = xTransition.Attribute("condition").Value;
                var nextState = xTransition.Attribute("nextState").Value;
                state.Transitions.Add(condition, nextState);
            }

            var xParams = xml.Elements("params");
            if (xParams != null)
            {
                var fsmTemplate = new FSMTemplate(sType);
                foreach (var attribParam in xParams.Attributes())
                    fsmTemplate.Parameters.Add(attribParam.Name.LocalName, attribParam.Value);

                foreach (var bodyParam in xParams.Elements())
                    fsmTemplate.Parameters.Add(bodyParam.Name.LocalName, bodyParam.Value);
                state.SetParameters(fsmTemplate.Parameters);
            }
            return state;
        }


        private static void BuildMappings()
        {
            _stateMapping = new Dictionary<string, Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.FullName.Contains("Tmc"))
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if (typeof(FSMState).IsAssignableFrom(type))
                        {
                            var name = type.FullName;
                            var attribs = type.GetCustomAttributes(typeof(NameAttribute), false);
                            if (attribs.Length > 0)
                            {
                                var attrib = attribs[0] as NameAttribute;
                                if (attrib != null)
                                    name = attrib.Name.ToLower();
                            }
                            _stateMapping.Add(name, type);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
