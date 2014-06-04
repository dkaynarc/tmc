using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmcData;

namespace TmcData
{
    public sealed class DatabaseLogProvider : ILogProvider
    {
        public LogType DefaultLogLevel { get; set; }
        public LogStrategy ProvidedStrategy { get; set; }

        public DatabaseLogProvider(LogType defaultLogLevel = LogType.Warning)
        {
            this.ProvidedStrategy = LogStrategy.Database;
            this.DefaultLogLevel = defaultLogLevel;
        }

        public void Write(string message)
        {
            Write(message, DefaultLogLevel);
        }

        public void Write(string message, LogType level)
        {
            Write(message, level, DateTime.Now);
        }

        public void Write(string message, LogType level, DateTime date)
        {
            TmcRepository.AddNewEventLog(date, message, (int)level);
        }

        public void Write(LogEntry entry)
        {
            Write(entry.Message, entry.Level, entry.TimeStamp);
        }
    }
}

