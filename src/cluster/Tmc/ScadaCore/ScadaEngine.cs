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
        //private EnvironmentMonitor _environmentMonitor;

        public ScadaEngine()
        {
            //this._hardwareMonitor = new HardwareMonitor(this);
            //this._environmentMonitor = new EnvironmentMonitor(this);
            this.ClusterConfig = ClusterFactory.Instance.CreateCluster("./Configuration/ClusterConfig.xml");
            this.TabletMagazine = new TabletMagazine(); 
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

        public string GetOperationStatus()
        {
            return "";
        }

        public void SetOperatingMode(string mode)
        {
        }
    }
}
