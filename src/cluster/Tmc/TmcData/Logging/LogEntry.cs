using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmcData
{
    public sealed class LogEntry
    {
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public LogType Level { get; set; }

        public LogEntry(string message = "", LogType level = LogType.Warning)
        {
            this.Message = message;
            this.Level = level;
            this.TimeStamp = DateTime.Now;
        }

        public LogEntry(Exception ex, LogType level = LogType.Error)
        {
            this.Message = ex.Message;
            this.Level = level;
            this.TimeStamp = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("[{0} {1}] - {2} - {3}", 
                this.TimeStamp.ToShortDateString(), this.TimeStamp.ToShortTimeString(),
                this.Level.ToString(), this.Message);
        }
    }
}
