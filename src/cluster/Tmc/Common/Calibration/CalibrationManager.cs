#region Header

/// FileName: CalibrationManager.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)

#endregion Header


using System;
using System.Collections.Generic;
using System.IO;

namespace Tmc.Common
{
    /// <summary>
    /// Provides calibration, saving and loading access to ICalibrateable types.
    /// Only one instance of each ICalibrateable type can be stored.
    /// This is a Singleton class.
    /// </summary>
    public sealed class CalibrationManager
    {
        private static CalibrationManager _instance;

        public static CalibrationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CalibrationManager();
                }
                return _instance;
            }
        }

        private Dictionary<Type, ICalibrateable> _calibrateables;

        /// <summary>
        /// Base directory where Calibration Data Files are located.
        /// CalibrationManager will not attempt to load or save if this is not set.
        /// </summary>
        public string DataFilesDirectory { get; set; }

        /// <summary>
        /// Default extension for Calibration Data Files.
        /// </summary>
        public string DataFileExtension { get; set; }

        private CalibrationManager()
        {
            _calibrateables = new Dictionary<Type, ICalibrateable>();
            this.DataFilesDirectory = "";
            this.DataFileExtension = ".cal";
        }

        /// <summary>
        /// Adds an ICalibrateable to the manager.
        /// </summary>
        /// <param name="c">The instance to add</param>
        public void Register(ICalibrateable c)
        {
            var type = c.GetType();
            if (!_calibrateables.ContainsKey(type))
            {
                _calibrateables.Add(type, c);
            }
        }
        
        /// <summary>
        /// Removes an ICalibrateable from the manager.
        /// </summary>
        /// <param name="c">The instance to remove</param>
        public void Unregister(ICalibrateable c)
        {
            var type = c.GetType();
            if (_calibrateables.ContainsKey(type))
            {
                _calibrateables.Remove(type);
            }
        }

        /// <summary>
        /// Calibrates all ICalibrateables held by the manager.
        /// </summary>
        public void CalibrateAll()
        {
            foreach (var c in _calibrateables.Values)
            {
                this.Calibrate(c);
            }
        }

        /// <summary>
        /// Calibrates a specific ICalibrateable.
        /// </summary>
        /// <typeparam name="T">Type of the ICalibrateable to calibrate.</typeparam>
        public void CalibrateSpecific<T>()
        {
            var type = typeof(T);
            ICalibrateable c = null;
            if (!_calibrateables.TryGetValue(type, out c))
            {
                throw new ArgumentException("Requested ICalibrateable is not managed by CalibrationManager");
            }
            this.Calibrate(c);
        }

        /// <summary>
        /// Loads all calibration data from the specified DataFilesDirectory.
        /// </summary>
        public void LoadAllDataFiles()
        {
            if (!String.IsNullOrEmpty(this.DataFilesDirectory))
            {
                var dataFiles = Directory.GetFiles(this.DataFilesDirectory, "*" + this.DataFileExtension);
                foreach (var file in dataFiles)
                {
                    var calData = LoadDataFile(file);
                    this.SetCalibrationData(calData);
                }
            }
        }

        private void Calibrate(ICalibrateable c)
        {
            var calData = c.Calibrate();
            TrySaveCalibrationData(calData);
        }

        private ICalibrationData LoadDataFile(string fileName)
        {
            ICalibrationData calData = null;
            try
            {
                calData = CalibrationDataSerializer.Deserialize(fileName);
            }
            catch (Exception)
            {
                // swallow - we just skip the file if it cannot be loaded.
            }

            return calData;
        }

        private void SetCalibrationData(ICalibrationData calData)
        {
            if (calData != null)
            {
                ICalibrateable c = null;
                if (_calibrateables.TryGetValue(calData.ParentType, out c))
                {
                    c.SetCalibrationData(calData);
                }
            }
        }

        private void TrySaveCalibrationData(ICalibrationData calData)
        {
            if (!String.IsNullOrEmpty(this.DataFilesDirectory))
            {
                var fullPath = Path.Combine(this.DataFilesDirectory, calData.ParentType.Name + this.DataFileExtension);
                CalibrationDataSerializer.Serialize(calData, fullPath);
            }
        }
    }
}