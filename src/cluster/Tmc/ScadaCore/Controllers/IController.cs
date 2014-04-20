using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public interface IController
    {
        bool IsRunning { get; set; }
        event EventHandler<EventArgs> Completed;
        void Begin(ControllerParams parameters);
    }

    public abstract class ControllerBase : IController
    {
        public event EventHandler<EventArgs> Completed;
        public bool IsRunning { get; set; }
        private ClusterConfig _config;
        public ControllerBase(ClusterConfig _config)
        {
            this._config = _config;
            this.IsRunning = false;
        }

        public virtual void Begin(ControllerParams parameters)
        {

        }

        protected virtual void OnCompleted(EventArgs e)
        {
            var handler = Completed;
            if (handler != null)
            {
                handler(this, e);
            }
            IsRunning = false;
        }
    }

    public class ControllerParams
    {
        public object Sender { get; set; }
    }
}
