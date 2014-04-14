using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;

namespace Tmc.Robotics
{
    public class BluetoothConveyor : IConveyor
    {
        public string Name { get; set; }

        public HardwareStatus GetStatus()
        {
            throw new NotImplementedException();
        }

        public void Initialise()
        {
            throw new NotImplementedException();
        }

        public void SetParameters(Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
