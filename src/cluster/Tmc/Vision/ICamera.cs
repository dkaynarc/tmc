using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tmc.Common;

namespace Tmc.Vision
{
    public interface ICamera : IHardware
    {
        string ConnectionString { get; set; }
    }
}
