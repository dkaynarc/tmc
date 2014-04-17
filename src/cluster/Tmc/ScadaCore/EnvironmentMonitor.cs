using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Sensors;

namespace Tmc.Scada.Core
{
    class EnvironmentMonitor
    {
        private ScadaEngine _engine;
        private List<ISensor> _sensors;

        public EnvironmentMonitor(ScadaEngine engine)
        {
            this._engine = engine;
            this._sensors = new List<ISensor>();
            var config = engine.ClusterConfig;
            //_sensors.AddRange();
        }
    }
}
