﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public interface IController
    {
        bool IsRunning { get; set; }
        event EventHandler<ControllerEventArgs> Completed;
        void Begin(ControllerParams parameters);
        void Cancel();
    }

    public abstract class ControllerBase : IController
    {
        public event EventHandler<ControllerEventArgs> Completed;
        public bool IsRunning { get; set; }
        private ClusterConfig _config;
        public ControllerBase(ClusterConfig _config)
        {
            this._config = _config;
            this.IsRunning = false;
        }

        public abstract void Begin(ControllerParams parameters);

        public abstract void Cancel();

        protected virtual void OnCompleted(ControllerEventArgs e)
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

    public enum ControllerOperationStatus
    {
        Succeeded,
        Cancelled,
        Failed
    }

    public class ControllerEventArgs : EventArgs
    {
        public ControllerOperationStatus OperationStatus;
    }
}
