using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Robotics
{
    public static class ConveyorFactory
    {
        public static T CreateConveyor<T>() where T : class, IRobot
        {
            var caseSwitch = new Dictionary<Type, Func<T>>
            {
                {typeof(BluetoothConveyor), () => { return BuildBluetoothConveyor() as T; }}
            };

            return caseSwitch[typeof(T)]();
        }

        public static IConveyor CreateConveyor(Type type)
        {
            var caseSwitch = new Dictionary<Type, Func<IConveyor>>
            {
                {typeof(BluetoothConveyor), () => { return BuildBluetoothConveyor(); }}
            };

            return caseSwitch[type]();
        }

        private static BluetoothConveyor BuildBluetoothConveyor()
        {
            return new BluetoothConveyor();
        }
    }
}
