using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tmc.Scada.Core;

namespace TmcTest
{
    [TestClass]
    public class ScadaEngineInteg
    {
        [TestMethod]
        public void AssembleCycle()
        {
            var engine = new ScadaEngine(@"./ScadaCoreTest/Configuration/IntegTestConfig.xml");
        }
    }
}
