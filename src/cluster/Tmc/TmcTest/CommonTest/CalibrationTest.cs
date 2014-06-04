#region Header

/// FileName: CalibrationTest.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)

#endregion Header

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tmc.Common;

namespace TmcTest.CommonTest
{
    [Serializable]
    public class CalibrationDataTestMock : ICalibrationData
    {
        public string StringData { get; set; }

        public Type ParentType { get; set; }
    }

    public class CalibrationTestMock : ICalibrateable
    {
        internal string _stringData;

        public CalibrationTestMock()
        {
            this.Register();
        }

        ~CalibrationTestMock()
        {
            this.Unregister();
        }

        public ICalibrationData Calibrate()
        {
            return new CalibrationDataTestMock
            {
                StringData = "CalibrationTestData",
                ParentType = this.GetType()
            };
        }

        public void SetCalibrationData(ICalibrationData data)
        {
            var myCalData = data as CalibrationDataTestMock;
            if (myCalData != null)
            {
                this._stringData = myCalData.StringData;
            }
        }

        public void Register()
        {
            CalibrationManager.Instance.Register(this);
        }

        public void Unregister()
        {
            CalibrationManager.Instance.Unregister(this);
        }
    }

    [TestClass]
    public class CalibrationTest
    {
        [TestMethod]
        public void TestLoad()
        {
            CalibrationManager.Instance.DataFilesDirectory = @".\";
            CalibrationDataTestMock calDataRef = null;
            var mock = new CalibrationTestMock();
            calDataRef = mock.Calibrate() as CalibrationDataTestMock;
            CalibrationManager.Instance.CalibrateSpecific<CalibrationTestMock>();
            mock._stringData = "Something different";
            Assert.AreNotEqual(calDataRef.StringData, mock._stringData);
            CalibrationManager.Instance.LoadAllDataFiles();
            Assert.AreEqual(calDataRef.StringData, mock._stringData);
        }
    }
}