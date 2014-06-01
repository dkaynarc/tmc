using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Robotics
{
    public static class ConveyorFactory
    {
        public static T CreateConveyor<T>() where T : class, IConveyor
        {
            var caseSwitch = new Dictionary<Type, Func<T>>
            {
                {typeof(BluetoothConveyor), () => { return BuildBluetoothConveyor() as T; }},
                {typeof(SerialConveyor), () => { return BuildSerialConveyor() as T; }}
            };

            return caseSwitch[typeof(T)]();
        }

        public static IConveyor CreateConveyor(Type type)
        {
            var caseSwitch = new Dictionary<Type, Func<IConveyor>>
            {
                {typeof(BluetoothConveyor), () => { return BuildBluetoothConveyor(); }},
                {typeof(SerialConveyor), () => { return BuildSerialConveyor(); }}
            };

            return caseSwitch[type]();
        }

        private static BluetoothConveyor BuildBluetoothConveyor()
        {
            return new BluetoothConveyor();
        }

        private static SerialConveyor BuildSerialConveyor()
        {
            return new SerialConveyor();
        }
    }
}
