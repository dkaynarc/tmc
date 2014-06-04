using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmcData
{
    public sealed class EnvironmentLogEntry
    {
        public String Channel { get; set; }
        public String Unit { get; set; }
        public float Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public LogType Level { get; set; }

        public EnvironmentLogEntry(string channel, float value, string unit, LogType level = LogType.Message)
        {
            this.Channel = channel;
            this.Value = value;
            this.Unit = unit;
            this.Level = level;
            this.TimeStamp = DateTime.Now;
        }
        public override string ToString()
        {
            //eg: 10/9/2009 9:45:06 PM Temperature: 40°C
            return string.Format("[{0} {1}] {2}: {3}{4}", 
                this.TimeStamp.ToShortDateString(), this.TimeStamp.ToShortTimeString(),
                this.Channel, this.Value, this.Unit)
                    ;
        }
    }
}
