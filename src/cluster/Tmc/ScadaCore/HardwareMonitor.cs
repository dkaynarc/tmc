using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tmc.Common;

namespace Tmc.Scada.Core
{
    public class HardwareMonitor
    {
        public bool LoggingEnabled { get; set; }
        private ScadaEngine _engine;
        private List<IHardware> _hardware;
        private List<HardwareStatus> _previoushardwareStatus;
        public event EventHandler<HardwareEventArgs> StatusChanged;
        private Timer _updateTimer;

        public HardwareMonitor(ClusterConfig config)
        {
            _hardware = config.GetAllHardware();
            int updateTime = 1000;

            if (!Int32.TryParse(ConfigurationManager.AppSettings["HardwareMonitorUpdateRateMsec"], out updateTime))
            {
                Logger.Instance.Write(new LogEntry("HardwareMonitorUpdateRateMsec is invalid, defaulting to 1000 msec",
                    LogType.Warning));
            }
            this._updateTimer = new Timer(updateTime);
            this._updateTimer.Elapsed += new ElapsedEventHandler((s, e) => this.Update());

            for (int i = 0; i < _hardware.Count; i++)
            {
                _previoushardwareStatus[i] = _hardware[i].GetStatus();
            }
        }

        public void Start()
        {
            this._updateTimer.Start();
        }

        public void Stop()
        {
            this._updateTimer.Stop();
        }

        private void Update()
        {
            for (int i = 0; i < _hardware.Count; i++)
            {
                if ((_hardware[i].GetStatus() == HardwareStatus.Failed))
                {
                    Logger.Instance.Write(new LogEntry(_hardware[i].Name + " has failed",
               LogType.Error));
                }

                if (_hardware[i].GetStatus() != _previoushardwareStatus[i])
                {
                    OnStatusChanged(new HardwareEventArgs(_hardware[i]));
                }
                _previoushardwareStatus[i] = _hardware[i].GetStatus();
            }
        }

        protected virtual void OnStatusChanged(HardwareEventArgs e)
        {
            var handler = StatusChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public class HardwareEventArgs : EventArgs
    {
        private IHardware _hardware;

        public HardwareEventArgs(IHardware hardware)
        {
            _hardware = hardware;
        }

        public IHardware hardware
        {
            get { return _hardware; }
        }
    }
}