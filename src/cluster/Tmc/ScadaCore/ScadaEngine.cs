using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Scada.Core.Sequencing;
using Tmc.Scada.Core.Reporting;
using System.ServiceModel;
using TmcData;

namespace Tmc.Scada.Core
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class ScadaEngine : IScada
    {
        public string Name { get; set; }
        public ClusterConfig ClusterConfig { get; set; }
        public OrderConsumer OrderConsumer { get; set; }
        public TabletMagazine TabletMagazine { get; set; }
        //private HardwareMonitor _hardwareMonitor;
        //private EnvironmentMonitor _environmentMonitor;
        private ISequencer _sequencer;

        public ScadaEngine()
        {
            this.Create(@"./Configuration/ClusterConfig.xml");
        }

        public ScadaEngine(string configFile)
        {
            this.Create(configFile);
        }

        private void Create(string configFile)
        {
            try
            {
                this.ClusterConfig = ClusterFactory.Instance.CreateCluster(configFile);
            }
            catch (Exception ex)
            {
                var outer = new Exception("Unable to initialise the cluster configuration", ex);

                Logger.Instance.Write(new LogEntry(outer, LogType.Error));
                return;
            }
            //this._environmentMonitor = new EnvironmentMonitor(this.ClusterConfig);
            //this._hardwareMonitor = new HardwareMonitor(this.ClusterConfig);
            this.TabletMagazine = new TabletMagazine();
            this._sequencer = new FSMSequencer(this);
            this.OrderConsumer = new OrderConsumer();
        }

        public void Initialise()
        {
            this._sequencer.StartSequencing();
            Logger.Instance.Write(new LogEntry("TMC control system initialised", LogType.Message));
        }

        public void Start()
        {
            //_environmentMonitor.Log(); // Should be run on a separate thread
            if (_sequencer.TransitionLogger.CurrentState.Id == State.Shutdown)
            {
                this._sequencer.FireStartTrigger();
                Logger.Instance.Write(new LogEntry("Cluster operation started", LogType.Message));
            }
        }

        public void Stop()
        {
            if ((_sequencer.TransitionLogger.CurrentState.Id != State.Stopped) || (_sequencer.TransitionLogger.CurrentState.Id != State.Shutdown))
            {
                this._sequencer.FireStopTrigger();
                Logger.Instance.Write(new LogEntry("Cluster operation stopped", LogType.Message));
            }
        }

        public void Resume()
        {
            if (_sequencer.TransitionLogger.CurrentState.Id == State.Stopped)
            {
                this._sequencer.FireResumeTrigger();
                Logger.Instance.Write(new LogEntry("Cluster operation resumed", LogType.Message));
            }
        }

        public void Shutdown()
        {
            if (_sequencer.TransitionLogger.CurrentState.Id != State.Shutdown)
            {
                this._sequencer.FireShutdownTrigger();
                Logger.Instance.Write(new LogEntry("Cluster operation shutdown", LogType.Message));
            }
        }

        public void EmergencyStop()
        {
            foreach (var hardware in ClusterConfig.GetAllHardware())
            {
                hardware.EmergencyStop();
            }
            Logger.Instance.Write(new LogEntry("Emergency stop command given", LogType.Warning));
            _sequencer.FireStopTrigger();
            _sequencer.StopSequencing();
        }

        public string GetOperationStatus()
        {
            return "";
        }

        public void SetOperatingMode(string mode)
        {
        }
    }
}
