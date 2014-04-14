using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Robotics
{
    public static class ConveyorFactory
    {
        public static T CreateFactory<T>() where T : class, IRobot
        {
            var caseSwitch = new Dictionary<Type, Func<T>>
            {
                {typeof(BluetoothConveyor)
            }
            return caseSwitch[typeof(T)]();
        }

        private void BluetoothConveyor BuildBluetoothConveyor()
        {
            return new BluetoothConveyor();
        }
    }
}
