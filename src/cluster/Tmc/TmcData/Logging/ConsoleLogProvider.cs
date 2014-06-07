using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmcData
{
    public class ConsoleLogProvider : ILogProvider
    {
        public LogType DefaultLogLevel { get; set; }

        public LogStrategy ProvidedStrategy { get; set; }

        public ConsoleLogProvider(LogType defaultLogLevel = LogType.Message)
        {
            this.ProvidedStrategy = LogStrategy.Console;
            this.DefaultLogLevel = defaultLogLevel;
        }

        public void Write(string message)
        {
            this.Write(message, DefaultLogLevel);
        }

        public void Write(LogEntry message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message, LogType level)
        {
            var entry = new LogEntry(message, level);
            this.Write(entry);
        }
    }
}
