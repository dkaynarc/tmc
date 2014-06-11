using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tmc.Common;
using TmcData;

namespace Tmc.Scada.Core
{
    public class HardwareMonitor
    {
        public bool LoggingEnabled { get; set; }
        private List<IHardware> _hardware;
        public Dictionary<string, HardwareStatus> PreviousHardwareStatuses { get; set; }
        public event EventHandler<HardwareEventArgs> StatusChanged;
        private Timer _updateTimer;

        public HardwareMonitor(ClusterConfig config)
        {
            _hardware = config.GetAllHardware();
            int updateTime = 1000;

            PreviousHardwareStatuses = new Dictionary<string, HardwareStatus>();

            if (!Int32.TryParse(ConfigurationManager.AppSettings["HardwareMonitorUpdateRateMsec"], out updateTime))
            {
                Logger.Instance.Write(new LogEntry("HardwareMonitorUpdateRateMsec is invalid, defaulting to 1000 msec",
                    LogType.Warning));
            }
            this._updateTimer = new Timer(updateTime);
            this._updateTimer.Elapsed += new ElapsedEventHandler((s, e) => this.Update());

            foreach (var hw in _hardware)
            {
                PreviousHardwareStatuses.Add(hw.Name, HardwareStatus.Offline);
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
            foreach (var hw in _hardware)
            {
                var status = hw.GetStatus();
                if ((status == HardwareStatus.Failed))
                {
                    Logger.Instance.Write(new LogEntry(hw.Name + " has failed",
                                            LogType.Error));
                }

                //if (status != PreviousHardwareStatuses[hw.Name])
                //{
                // Changed so that this event fires every update for every hardware item - DK
                OnStatusChanged(new HardwareEventArgs(hw));
                //}
                PreviousHardwareStatuses[hw.Name] = status;
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