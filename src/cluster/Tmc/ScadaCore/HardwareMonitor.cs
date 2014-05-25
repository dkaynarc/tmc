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
        //private List<HardwareLogEntry> Log { get; set; }
        private Timer _updateTimer;

        public HardwareMonitor(ClusterConfig config)
        {
            _hardware = config.GetAllHardware();
            //Log = new List<HardwareLogEntry>();
            int updateTime = 1000;

            if (!Int32.TryParse(ConfigurationManager.AppSettings["HardwareMonitorUpdateRateMsec"], out updateTime))
            {
                Logger.Instance.Write(new LogEntry("HardwareMonitorUpdateRateMsec is invalid, defaulting to 1000 msec",
                    LogType.Warning));
            }
            this._updateTimer = new Timer(updateTime);
            this._updateTimer.Elapsed += new ElapsedEventHandler((s, e) => this.Update());
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
            while (LoggingEnabled)
            {
                foreach (var hardware in _hardware)
                {
                    LogType logType;
                    if ((hardware.GetStatus() == HardwareStatus.Failed)) 
                    {
                        logType = LogType.Error;
                    }
                    else
                    {
                        logType = LogType.Message;
                    }
                    //Log.Add(new HardwareLogEntry(hardware.Name, hardware.GetStatus(), logType));
                    // enter in the database
                }
            }
        }
    }
}
