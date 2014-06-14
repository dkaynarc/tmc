using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tmc.Common;
using Tmc.Sensors;
using TmcData;

namespace Tmc.Scada.Core
{
    public class EmergencyStopMonitor
    {
        public bool IsEnabled { get; private set; }
        private Timer _timer;
        private IPlc _plc;
        private List<IHardware> _allHardware;
        private ScadaEngine _engine;

        public EmergencyStopMonitor(ScadaEngine engine, long updateTimeMsec = 2000)
        {
            this._engine = engine;
            _timer = new Timer(updateTimeMsec);
            _timer.Elapsed += Update;
            _plc = _engine.ClusterConfig.Plcs["MainPlc"] as IPlc;
            this.IsEnabled = false;

            if (_plc == null)
            {
                throw new ArgumentException("Could not retrieve a Plc from ClusterConfig");
            }

            _allHardware = _engine.ClusterConfig.GetAllHardware();
        }

        public void Start()
        {
            if (!this.IsEnabled)
            {
                _timer.Start();
                this.IsEnabled = true;
            }
        }
        
        public void Stop()
        {
            if (this.IsEnabled)
            {
                _timer.Stop();
                this.IsEnabled = false;
            }
        }

        private void Update(object sender, ElapsedEventArgs e)
        {
            var plcSwitches = _plc.GetSwitchStates();
            var scadaEStopBtn = !plcSwitches[PlcAttachedSwitch.ScadaEmergencyStop];
            var plcEStopBtn = !plcSwitches[PlcAttachedSwitch.PlcEmergencyStop];

            if (scadaEStopBtn || plcEStopBtn)
            {
                Logger.Instance.Write("[EmergencyStopMonitor] Emergency stop command received from PLC", LogType.Warning);
                _engine.EmergencyStop();
                this.Stop();
            }
        }
    }
}
