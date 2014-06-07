using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TmcData
{
    public sealed class FileLogProvider : ILogProvider, IDisposable
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

        public void Dispose()
        {
            _streamWriter.Dispose();
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
                _streamWriter = new StreamWriter(fileName, false);
                _streamWriter.AutoFlush = true;
            }
            catch (IOException)
            {
                throw new ArgumentException("Filename " + fileName + "is invalid");
            }
        }
    }
}
