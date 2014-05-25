using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel;
using Tmc.Scada.Core;
using System.Configuration;

namespace Tmc.Scada.App
{
    public class WcfHost
    {
        private ServiceHost _serviceHost;

        public bool IsConnected { get; private set; }

        public WcfHost(IScada engine)
        {
            IsConnected = false;
            if (engine == null)
            {
                throw new ArgumentNullException("engine was null");
            }
            
            this._serviceHost = new ServiceHost(engine, new Uri(ConfigurationManager.AppSettings["ScadaWcfPipeBase"]));

            this._serviceHost.AddServiceEndpoint(typeof(IScada), new NetNamedPipeBinding(), ConfigurationManager.AppSettings["ScadaWcfPipeEndpoint"]);
        }

        public void Open()
        {
            if (!IsConnected)
            {
                try
                {
                    this._serviceHost.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to open service " + ex);
                }

                IsConnected = true;
            }
        }

        public void Close()
        {
            if (IsConnected)
            {
                this._serviceHost.Close();
                IsConnected = false;
            }
        }
    }
}
