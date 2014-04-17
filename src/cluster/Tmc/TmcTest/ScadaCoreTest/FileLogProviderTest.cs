using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tmc.Scada.Core;
using System.IO;

namespace TmcTest.ScadaCore
{
    [TestClass]
    public class FileLogProviderTest
    {

        //A test method must meet the following requirements:
        //  The method must be decorated with the [TestMethod] attribute.
        //  The method must return void.
        //  The method cannot have parameters.

        /// <summary>
        /// Sample Test: Using FileLogProvider, a log file is created and written to. 
        /// This is then read back using the StreamReader to ensure the log was written correctly
        /// </summary>
        [TestMethod]
        public void FileLogProviderTest_Write()
        {
            // arrange
            const string filePath = "TestFile.txt";
            File.Delete(filePath);
            LogEntry logMessage = new LogEntry("Test123");

            // act
            using (var fileLogProvider = new FileLogProvider(filePath))
            {
                fileLogProvider.Write(logMessage);
            }

            var reader = new StreamReader(filePath);
                
            // assert
            string writtenMessage = reader.ReadLine();
            reader.Close();
            Assert.AreEqual(logMessage.ToString(), writtenMessage);

            File.Delete(filePath);
        }
    }
}
