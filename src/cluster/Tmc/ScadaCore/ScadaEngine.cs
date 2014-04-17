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
        private ISequencer _sequencer;
        private HardwareMonitor _hardwareMonitor;
        private EnvironmentMonitor _environmentMonitor;

        public ScadaEngine()
        {
            this._sequencer = new FSMSequencer(this);
            this._hardwareMonitor = new HardwareMonitor(this);
            this._environmentMonitor = new EnvironmentMonitor(this);
        }
    }
}
