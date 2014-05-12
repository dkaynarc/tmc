using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;

namespace Tmc.Scada.Core
{
    public class HardwareMonitor
    {
        private ScadaEngine _engine;
        private List<IHardware> _hardware;

        public HardwareMonitor(ScadaEngine engine)
        {
            this._engine = engine;
            _hardware = _engine.ClusterConfig.GetAllHardware();
        }
    }
}
