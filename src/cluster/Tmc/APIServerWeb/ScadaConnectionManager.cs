using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;

namespace APIServerWeb
{
    public class ScadaConnectionManager
    {
        private static IScada _scadaClient;
        public static IScada ScadaClient
        {
            get
            {
                var pipeFactory = new ChannelFactory<IScada>(
                    new NetNamedPipeBinding(),
                    new EndpointAddress(ConfigurationManager.AppSettings["ScadaWcfPipe"]));
                _scadaClient = pipeFactory.CreateChannel();

                return _scadaClient;
            }
        }
    }
}
