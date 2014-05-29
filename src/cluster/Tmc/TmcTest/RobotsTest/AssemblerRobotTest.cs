using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tmc.Robotics;
using System.Threading;

namespace TmcTest.Robots
{
    [TestClass]
    public class AssemblerRobotTest
    {
        AssemblerRobot _assembler;

        public AssemblerRobotTest()
        {
            _assembler = RobotFactory.CreateRobot<AssemblerRobot>();

            var dict = new Dictionary<string, string>();
            dict.Add("IPAddress", "192.168.1.200");

            _assembler.SetParameters(dict);
            _assembler.Initialise();
        }

        [TestMethod]
        public void GetMagazine()
        {
            try
            {
                _assembler.GetMagazine();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void ReturnMagazine()
        {
            try
            {
                _assembler.ReturnMagazine();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void PlaceTablet()
        {
            try
            {
                _assembler.PlaceTablet(0, 1, 1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlaceTabletWithInvalidArgument()
        {
            _assembler.PlaceTablet(0, 100, 1);
        }
    }
}
