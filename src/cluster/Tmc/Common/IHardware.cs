using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Common
{
    public enum HardwareStatus
    {
        Offline,
        Operational,
        Failed
    }

    public interface IHardware
    {
        string Name { get; set; }
        HardwareStatus GetStatus();
        void SetParameters(Dictionary<string, string> parameters);
    }
}
