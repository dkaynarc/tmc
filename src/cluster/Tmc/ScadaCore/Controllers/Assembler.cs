using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public class Assembler : ControllerBase
    {
        public Assembler(ClusterConfig config) : base(config)
        {
        }    

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as AssemblerParams;
            if (p != null)
            {
                OnCompleted(new EventArgs());
            }
        }
    }

    public class AssemblerParams : ControllerParams
    {
        public OrderConfiguration OrderConfig;
    }
}
