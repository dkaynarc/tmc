using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel;
using Tmc.Scada.Core;

namespace Tmc.Scada.App
{
    public class WcfHost
    {
        private readonly Uri _baseAddress = new Uri("http://localhost:8000/TMC/");
        private ServiceHost _serviceHost;

        public bool IsConnected { get; private set; }

        public WcfHost(IScada engine)
        {
            IsConnected = false;
            if (engine == null)
            {
                throw new ArgumentNullException("engine was null");
            }
            
            this._serviceHost = new ServiceHost(engine, _baseAddress);
        }

        public void Open()
        {
            if (!IsConnected)
            {
                try
                {
                    this._serviceHost.AddServiceEndpoint(typeof(IScada), new WSHttpBinding(), "ScadaEngine");
                    var smb = new ServiceMetadataBehavior();
                    smb.HttpGetEnabled = true;
                    this._serviceHost.Description.Behaviors.Add(smb);
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
