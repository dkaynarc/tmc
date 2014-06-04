using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Common
{
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

        private CalibrationManager()
        {
            _calibrateables = new Dictionary<Type, ICalibrateable>();
        }

        public void CalibrateAll()
        {
            foreach (var c in _calibrateables.Values)
            {
                c.Calibrate();
            }
        }

        public void Register(ICalibrateable c)
        {
            var type = c.GetType();
            if (!_calibrateables.ContainsKey(type))
            {
                _calibrateables.Add(type, c);
            }
        }

        public void Unregister(ICalibrateable c)
        {
            var type = c.GetType();
            if (_calibrateables.ContainsKey(type))
            {
                _calibrateables.Remove(type);
            }
        }

        public void CalibrateSpecific<T>()
        {
            var type = typeof(T);
            ICalibrateable c = null;
            if (!_calibrateables.TryGetValue(type, out c))
            {
                throw new ArgumentException("Requested ICalibrateable is not managed by CalibrationManager");   
            }
            c.Calibrate();
        }
    }
}
