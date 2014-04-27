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
        private HardwareMonitor _hardwareMonitor;
        private EnvironmentMonitor _environmentMonitor;
        private HardwareStatus _hardwareStatus;

        public ScadaEngine()
        {
            this._hardwareMonitor = new HardwareMonitor(this);
            this._environmentMonitor = new EnvironmentMonitor(this);
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

        public HardwareStatus GetStatus()
        {
            return _hardwareStatus;
        }
    }
}
