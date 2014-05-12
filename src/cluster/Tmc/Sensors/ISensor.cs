
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;

namespace Tmc.Sensors
{
    public interface ISensor : IHardware
    {
        string Channel { get; set; }
        string IPAddress { get; set; }
        string PortName { get; set; }
        float GetData();
    }
}
