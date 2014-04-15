using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public class TrayVerifier : ControllerBase
    {
        public TrayVerifier(ClusterConfig config) : base(config)
        {
        }
    }

    public class TrayVerifierParams : ControllerParams
    {
        public Tray TraySpecification { get; set; }
    }
}
