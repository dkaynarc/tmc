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
        internal TabletMagazine TabletMagazine { get; set; }
        //private HardwareMonitor _hardwareMonitor;
        //private EnvironmentMonitor _environmentMonitor;

        public ScadaEngine()
        {
            //this._hardwareMonitor = new HardwareMonitor(this);
            //this._environmentMonitor = new EnvironmentMonitor(this);
            this.ClusterConfig = ClusterFactory.Instance.CreateCluster("./Configuration/ClusterConfig.xml");
            this.TabletMagazine = new TabletMagazine(); 
            //this._sequencer = new FSMSequencer(this);
            //this._hardwareMonitor = new HardwareMonitor(this);
            //this._environmentMonitor = new EnvironmentMonitor(this);
        }

        public void Initialise()
        {
        }

        public void Start()
        {
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
