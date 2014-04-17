using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public enum LogType
    { 
        Message,
        Warning,
        Error
    }

    public interface ILogProvider
    {
        LogType DefaultLogLevel { get; set; }
        LogStrategy ProvidedStrategy { get; set; }

        void Write(string message);
        void Write(LogEntry message);
        void Write(string message, LogType level);
    }
}
