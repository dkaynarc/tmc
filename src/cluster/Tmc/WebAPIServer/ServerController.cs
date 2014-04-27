using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiServer.Authentication;

namespace WebApiServer
{
    public class ServerController : ApiController
    {


        public ServerController()
        {
            SCADAUser user = new SCADAUser();
            user.UserName = "vasia";
            var result = UserManager.Create(user, "bbbbekdkdkd");
        }

        public SCADAUserManager UserManager
        {
            get { return new SCADAUserManager(); }
        }


        public string Get()
        {
            return "response";
        }

        public string Authenticate(string userName, string password)
        {
            var user = UserManager.Find(userName, password);
            if (user != null) return "success";

            return "fail";
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
