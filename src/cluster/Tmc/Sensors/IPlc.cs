#region Header
/// FileName: Plc.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;

namespace Tmc.Sensors
{
    public enum PlcAttachedSwitch
    {
        Tray4 = 0,
        Tray1,
        Tray2,
        Tray3,
        Tray5,
        Tray6,
        PlcEmergencyStop,
        ScadaEmergencyStop
    }

    public interface IPlc : IHardware
    {
        IDictionary<PlcAttachedSwitch, bool> GetSwitchStates();
    }
}
