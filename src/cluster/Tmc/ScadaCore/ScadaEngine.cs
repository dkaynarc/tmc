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
            this.ClusterConfig = ClusterFactory.Instance.CreateCluster(configFile);
            //this._environmentMonitor = new EnvironmentMonitor(this.ClusterConfig);
            //this._hardwareMonitor = new HardwareMonitor(this);
            this.TabletMagazine = new TabletMagazine();
            this._sequencer = new FSMSequencer(this);
            this.OrderConsumer = new OrderConsumer();
        }

        public void Initialise()
        {
        }

        public void Start()
        {
            //_environmentMonitor.Log(); // Should be run on a separate thread
        }

        public void Stop()
        {
        }

        public void Resume()
        {
        }

        public void EmergencyStop()
        {
        }
    }
}
