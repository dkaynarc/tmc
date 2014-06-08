using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tmc.Robotics;
using System.Collections.Generic;

namespace TmcTest.ConveyorTest
{
    [TestClass]
    public class Conveyors
    {
        private SerialConveyor _serialConveyor;

        public Conveyors()
        {
            _serialConveyor = ConveyorFactory.CreateConveyor<SerialConveyor>();

            var dict = new Dictionary<string, string>();
            dict.Add("PortName", "COM3");

            _serialConveyor.SetParameters(dict);
            _serialConveyor.Initialise();
        }

        [TestMethod]
        public void BluetoothForward()
        {
            var conveyor = ConveyorFactory.CreateConveyor<BluetoothConveyor>();

            var dict = new Dictionary<string, string>();
            dict.Add("PortName", "COM7");
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
            dict.Add("PortName", "COM7");
            dict.Add("WaitTime", "1000");

            conveyor.SetParameters(dict);
            conveyor.Initialise();

            conveyor.MoveBackward();
        }
           
        [TestMethod]
        public void SerialForward()
        {
            //_serialConveyor.Position = ConveyorPosition.Middle;
            try
            {
                _serialConveyor.MoveForward();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SerialBackward()
        {
            _serialConveyor.Position = ConveyorPosition.Middle;
            try
            {
                _serialConveyor.MoveBackward();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    
    }
}
