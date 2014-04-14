using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tmc.Scada.Core
{
    public sealed class FileLogProvider : ILogProvider
    {
        private StreamWriter _streamWriter;

        public LogType DefaultLogLevel { get; set; }
        public LogStrategy ProvidedStrategy { get; set; }

        public FileLogProvider(string fileName, LogType defaultLogLevel = LogType.Warning)
        {
            this.ProvidedStrategy = LogStrategy.File;
            this.DefaultLogLevel = defaultLogLevel;
            OpenFile(fileName);
        }

        public void Write(string message)
        {
            Write(message, DefaultLogLevel);
        }

        public void Write(string message, LogType level)
        {
            var entry = new LogEntry(message, level);
            Write(entry);
        }

        public void Write(LogEntry message)
        {
            _streamWriter.WriteLine(message);
        }

        private void OpenFile(string fileName)
        {
            try
            {
                _streamWriter = new StreamWriter(fileName, true);
                _streamWriter.AutoFlush = true;
            }
            catch (FileNotFoundException)
            {
                throw new ArgumentException("Filename " + fileName + "does not exist");
            }
        }
    }
}
