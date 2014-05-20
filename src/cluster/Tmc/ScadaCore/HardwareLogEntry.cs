using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;

namespace Tmc.Scada.Core
{
    class HardwareLogEntry
    {
         public String Name { get; set; }
        public HardwareStatus Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public LogType Level { get; set; }

        public HardwareLogEntry(string name, HardwareStatus status, LogType level = LogType.Message)
        {
            this.Name = name;
            this.Status = status;
            this.Level = level;
            this.TimeStamp = DateTime.Now;
        }
        public override string ToString()
        {
            //eg: 10/9/2009 9:45:06 PM Temperature: 40°C
            return string.Format("[{0} {1}] {2} {3}", 
                this.TimeStamp.ToShortDateString(), this.TimeStamp.ToShortTimeString(),
                this.Name, this.Status);
        }
    }
    }
}
