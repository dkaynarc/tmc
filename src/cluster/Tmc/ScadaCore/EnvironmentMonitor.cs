using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Sensors;
using System.Threading;

namespace Tmc.Scada.Core
{
    //public enum Sensors
    //{
    //    Humidity,
    //    Temperature,
    //    Light,
    //    Sound,
    //    Dust
    //}

    //public enum Units
    //{
    //    Percentage = "%",
    //    Celsius = "°C",
    //    Candela = "cd",
    //    Decibel = "dB",
    //    ParticlesPerLitre = "pcs/L"
    //}

    public class EnvironmentMonitor
    {
        public bool LoggingEnabled { get; set; }
        private ScadaEngine _engine;
        private List<ISensor> _sensors;
        private List<EnvironmentLogEntry> _log;

        public EnvironmentMonitor(ScadaEngine engine)
        {
            _log = new List<EnvironmentLogEntry>();
            
            this._engine = engine;
            this._sensors = new List<ISensor>();
            var config = engine.ClusterConfig;
            LoggingEnabled = true;

            //_sensors.Add(config.Sensors[typeof(AssemblerRobot)] as AssemblerRobot; //need sensor objects
        }

        public void Initialise(ScadaEngine engine)
        {
        }

        public void Log()   
        {
            while (LoggingEnabled)
            {
                foreach (var sensor in _sensors)
                {
                    //_log.Add(new EnvironmentLogEntry(sensor.Name + ": " + sensor.Value + sensor.Unit));
                }
                Thread.Sleep(1000);
            }
        }
    }
}
