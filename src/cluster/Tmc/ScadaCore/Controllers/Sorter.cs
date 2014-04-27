using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public sealed class Sorter : ControllerBase
    {
        public Sorter(ClusterConfig config) : base(config)
        {
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as SorterParams;
            if (p != null)
            {
                OnCompleted(new EventArgs()); 
            }
        }
    }

    public class SorterParams : ControllerParams
    {
        public TabletMagazine Magazine;
    }
}
