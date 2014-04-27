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
        private HardwareStatus _hardwareStatus;

        public ScadaEngine()
        {
            this._hardwareMonitor = new HardwareMonitor(this);
            this._environmentMonitor = new EnvironmentMonitor(this);
        }

        public void Initialise()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void EmergencyStop()
        {
            throw new NotImplementedException();
        }

        public HardwareStatus GetStatus()
        {
            return _hardwareStatus;
        }
    }
}
