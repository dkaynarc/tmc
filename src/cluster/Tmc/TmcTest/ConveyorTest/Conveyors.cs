using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tmc.Robotics;
using System.Collections.Generic;

namespace TmcTest.ConveyorTest
{
    [TestClass]
    public class Conveyors
    {
        [TestMethod]
        public void BluetoothForward()
        {
            var conveyor = ConveyorFactory.CreateConveyor<BluetoothConveyor>();

            var dict = new Dictionary<string, string>();
            dict.Add("PortName", "COM8");
            dict.Add("WaitTime", "1000");

            conveyor.SetParameters(dict);
            conveyor.Initialise();

            conveyor.MoveForward();
        }

        [TestMethod]
        public void BluetoothBackward()
        {
            var conveyor = ConveyorFactory.CreateConveyor<BluetoothConveyor>();

            var dict = new Dictionary<string, string>();
            dict.Add("PortName", "COM8");
            dict.Add("WaitTime", "1000");

            conveyor.SetParameters(dict);
            conveyor.Initialise();

            conveyor.MoveBackward();
        }
    }
}
