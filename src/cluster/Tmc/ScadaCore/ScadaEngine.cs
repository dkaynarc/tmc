using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Scada.Core.Sequencing;
using Tmc.Common;
using Tmc.Scada.Core.Reporting;

namespace Tmc.Scada.Core
{
    public class ScadaEngine : IScada
    {
        public ClusterConfig ClusterConfig { get; set; }
        internal TabletMagazine TabletMagazine { get; set; }
        //private HardwareMonitor _hardwareMonitor;
        private EnvironmentMonitor _environmentMonitor;

        public ScadaEngine()
        {
            //this._hardwareMonitor = new HardwareMonitor(this);
            this._environmentMonitor = EnvironmentMonitor.Instance;
            this.ClusterConfig = ClusterFactory.Instance.CreateCluster("./Configuration/ClusterConfig.xml");
            this.TabletMagazine = new TabletMagazine(); 
        }

        public void Initialise()
        {
            _environmentMonitor.Initialise(this);
        }

        public void Start()
        {
            _environmentMonitor.Log(); // Should be run on a separate thread
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
