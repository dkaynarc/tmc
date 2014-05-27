using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmcData;

namespace Tmc.Scada.Core.Logging
{
    public sealed class DatabaseLogProvider : ILogProvider
    {
        public LogType DefaultLogLevel { get; set; }
        public LogStrategy ProvidedStrategy { get; set; }

        public DatabaseLogProvider(string fileName, LogType defaultLogLevel = LogType.Warning)
        {
            this.ProvidedStrategy = LogStrategy.Database;
            this.DefaultLogLevel = defaultLogLevel;
        }

        public void Write(string message, int sourceID)
        {
            Write(message, DefaultLogLevel, sourceID);
        }

        public void Write(string message, LogType level, int sourceID)
        {
            TmcRepository.AddNewEventLog(DateTime.Now, message, sourceID, level.ToString());
        }
    }
}

