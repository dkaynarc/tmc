using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public interface IController
    {
    }

    public abstract class ControllerBase : IController
    {
        private ClusterConfig _config;
        public ControllerBase(ClusterConfig _config)
        {
            this._config = _config;
        }
    }
}
