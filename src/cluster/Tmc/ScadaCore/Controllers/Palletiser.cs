using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public class Palletiser : ControllerBase
    {
        public Palletiser(ClusterConfig config) : base(config)
        {
        }

        public override void Begin(ControllerParams parameters)
        {
        }
    }
}
