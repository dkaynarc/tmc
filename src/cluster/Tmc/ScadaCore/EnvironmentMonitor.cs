using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Sensors;
using System.Threading;

namespace Tmc.Scada.Core
{
    public enum Sensors
    {
        Humidity,
        Temperature,
        Light,
        Sound,
        Dust
    }

    public enum Units
    {
        Celsius,
        //TODO get all the units from the sensor team
    }

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

        // TODO Have to think where the sensors get created
        //public void startLogging()
        //{
        //    while (startLoggingEnabled)
        //    {
        //        Logger.Instance.Write(new EnvirontmentLogEntry(Sensors.Temperature + ": " + _sensors.value + Units.Celsius));

        //        Thread.Sleep(1000);
        //    }
        //}
    }
}
