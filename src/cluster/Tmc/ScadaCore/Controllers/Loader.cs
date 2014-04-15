using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public class Loader : ControllerBase
    {
        public Loader(ClusterConfig config) : base(config)
        {
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as LoaderParams;
            if (p != null)
            {

            }
        }
    }

    public enum LoaderAction
    {
        LoadToConveyor,
        LoadToAcceptedBuffer,
        LoadToRejectedBuffer
    }

    public class LoaderParams : ControllerParams
    {
        public LoaderAction Action { get; set; }
    }
}
