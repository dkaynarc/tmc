using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Scada.Core.Sequencing;
using Tmc.Scada.Core.Reporting;
using System.ServiceModel;

namespace Tmc.Scada.Core
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class ScadaEngine : IScada
    {
        public string Name { get; set; }
        public ClusterConfig ClusterConfig { get; set; }
        public OrderConsumer OrderConsumer { get; set; }
        internal TabletMagazine TabletMagazine { get; set; }
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
            //this._hardwareMonitor = new HardwareMonitor(this);
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
            this._sequencer.FireStartTrigger();
            Logger.Instance.Write(new LogEntry("Cluster operation started", LogType.Message));
        }

        public void Stop()
        {
            this._sequencer.FireStopTrigger();
            Logger.Instance.Write(new LogEntry("Cluster operation stopped", LogType.Message));
        }

        public void Resume()
        {
            this._sequencer.FireResumeTrigger();
            Logger.Instance.Write(new LogEntry("Cluster operation resumed", LogType.Message));
        }

        public void EmergencyStop()
        {
            Logger.Instance.Write(new LogEntry("Emergency stop command given", LogType.Warning));
            _sequencer.FireStopTrigger();
            _sequencer.StopSequencing();
        }
    }
}
