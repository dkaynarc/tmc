using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tmc.Robotics;
using System.Threading;

namespace TmcTest.Robots
{
    /// <summary>
    /// Summary description for RobotUnitTests
    /// </summary>
    [TestClass]
    public class LoaderRobotTest
    {
        LoaderRobot _loader;

        public LoaderRobotTest()
        {
            _loader = RobotFactory.CreateRobot<LoaderRobot>();

            var dict = new Dictionary<string, string>();
            dict.Add("IPAddress", "192.168.1.202");

            _loader.SetParameters(dict);
            _loader.Initialise();

            //var conveyor = ConveyorFactory.CreateConveyor<SerialConveyor>();
            //var dict3 = new Dictionary<string, string>();
            //dict3.Add("PortName", "COM1");

            //conveyor.SetParameters(dict3);
            //conveyor.Initialise();

            //conveyor.MoveForward();
        }

        /// <summary>
        /// Test passes when LoaderRobot can get trays 1 to 6. Otherwise an exception is caught.
        /// </summary>
        [TestMethod]
        public void GetTray()
        {
            try
            {
                _loader.GetTray(1);
                //conveyor.MoveForward();
                //_loader.GetTray(2);
                ////conveyor.MoveForward();
                //_loader.GetTray(3);
                ////conveyor.MoveForward();
                //_loader.GetTray(4);
                ////conveyor.MoveForward();
                //_loader.GetTray(5);
                ////conveyor.MoveForward();
                //_loader.GetTray(6);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Test passes when LoaderRobot throws an exception. Testing for an imaginary tray. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTrayWithOverBoundArgument()
        {
            _loader.GetTray(9);
        }

        /// <summary>
        /// Test passes when LoaderRobot throws an exception. Testing for an imaginary tray. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTrayWithUnderBoundArgument()
        {
            _loader.GetTray(0);
        }

        /// <summary>
        /// Test passes when LoaderRobot throws an exception. Testing for an imaginary tray. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTrayWithNegativeArgument()
        {
            _loader.GetTray(-2);
        }

        [TestMethod]
        public void Palletise()
        {
            try
            {
                _loader.Palletise();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ShutDown()
        {
            _loader.Shutdown();
        }
    }
}
