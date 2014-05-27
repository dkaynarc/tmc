using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tmc.Robotics;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Tmc.Common;

namespace TmcTest.Robots
{
    [TestClass]
    public class BaseRobotTest
    {
        BaseRobot _base;

        public BaseRobotTest()
        {
            _base = RobotFactory.CreateRobot<BaseRobot>();

            var dict = new Dictionary<string, string>();
            dict.Add("IPAddress", "192.168.1.201"); //SorterRobot

            _base.SetParameters(dict);
            _base.Initialise();
        }

        public void GetStatus()
        {
            try
            {
                _base.GetStatus();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SetSpeed()
        {
            try
            {
                _base.SetSpeed(1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetSpeed()
        {
            _base.SetSpeed(101);
        }

        [TestMethod]
        public void Initialise()
        {
            try
            {
                _base.Initialise();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Initialise()
        {
            _base.Initialise();
        }

        [TestMethod]
        public void Shutdown()
        {
            try
            {
                _base.Shutdown();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ReturnToHomePosition()
        {
            try
            {
                _base.ReturnToHomePosition();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void SetParameters()
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("IPAddress", "192.168.1.201");

                _base.SetParameters(dict);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetParametersWithNullArgument()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("IPAddress", "");

            _base.SetParameters(dict);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetParametersWithInvalidArgument()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("IPAddress", "192.168.1.153");

            _base.SetParameters(dict);
        }
    }
}
