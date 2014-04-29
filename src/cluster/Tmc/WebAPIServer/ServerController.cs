using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiServer.Authentication;
using Tmc.Scada.Core;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Configuration;

namespace WebApiServer
{
    public class ServerController : ApiController
    {
        //private ScadaClient _scadaClient;
        private IScada _scadaClient;

        public ServerController()
        {
            SCADAUser user = new SCADAUser();
            user.UserName = "vasia";
            var result = UserManager.Create(user, "bbbbekdkdkd");

            // TODO: Handle the case where the ScadaClient service reference cannot be resolved.
            // This will support the use case of the API Server running when the SCADA is not.
            var pipeFactory = new ChannelFactory<IScada>(
                new NetNamedPipeBinding(),
                new EndpointAddress(ConfigurationManager.AppSettings["ScadaWcfPipe"]));
            _scadaClient = pipeFactory.CreateChannel();
        }

        public SCADAUserManager UserManager
        {
            get { return new SCADAUserManager(); }
        }

        public string Get(string request)
        {
            return request + "Acknowledged";
        }

        public string Authenticate(string userName, string password)
        {
            var user = UserManager.Find(userName, password);
            if (user != null)
            {
                return "success";
            }

            return "fail";
        }

        public string StartScada()
        {
            _scadaClient.Start();
            return "response";
        }

        public string StopScada()
        {
            _scadaClient.Stop();
            return "response";
        }

        public string ResumeScada()
        {
            _scadaClient.Resume();
            return "response";
        }

        public string EmergencyStopScada()
        {
            _scadaClient.EmergencyStop();
            return "response";
        }

        public string PlaceOrder(string orderData)
        {
            return "response";
        }

        public string StartMachine()
        {
            return "response";
        }

        public string StopMachine()
        {
            return "response";
        }

        public string RemoveOrder()
        {
            return "response";
        }


        public string GetMachineStatus(string machineId)
        {
            return "response";
        }

        public string GetOrderStatus(string orderId)
        {
            return "response";
        }


        public string GetUserOrders()
        {
            return "response";
        }

    }
}
