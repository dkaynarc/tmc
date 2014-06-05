using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tmc.Robotics;

namespace TmcTest.Robots
{
    /// <summary>
    /// Summary description for RobotUnitTests
    /// </summary>
    [TestClass]
    public class RobotUnitTests
    {
        LoaderRobot _loader;

        public RobotUnitTests()
        {
            _loader = RobotFactory.CreateRobot<LoaderRobot>();

            var dict = new Dictionary<string, string>();
            dict.Add("IPAddress", "192.168.1.202");

            _loader.SetParameters(dict);
            _loader.Initialise();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestLoader()
        {
            try
            {
                _loader.Shutdown();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            // Act

            //_loader.GetTray(1);     

            //Assert.Fail();                          

        }

        [TestMethod]
        public void ShutDown()
        {
            _loader.Shutdown();
        }
    }
}
