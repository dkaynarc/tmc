using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tmc.Robotics;
using System.Collections.Generic;

namespace TmcTest.Robots
{
    [TestClass]
    public class SorterRobotTest
    {
        SorterRobot _sorter;

        public SorterRobotTest()
        {
            _sorter = RobotFactory.CreateRobot<SorterRobot>();

            var dict = new Dictionary<string, string>();
            dict.Add("IPAddress", "192.168.1.201");

            _sorter.SetParameters(dict);
            _sorter.Initialise();
        }

        [TestMethod]
        public void GetMagazine()
        {
            try
            {
                _sorter.GetMagazine();
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
                _sorter.ReturnMagazine();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

       [TestMethod]
        public void GetTablet()
        {
            try
            {
                _sorter.GetTablet(12, 12, -1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Shake()
        {
            try
            {
                _sorter.Shake();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
}
