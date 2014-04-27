using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using System.Net.Http;


namespace WebApiServer
{
    public class Program
    {

        public static void Main(string[] args)
        {
            new Starter().Start(); // call this to start the web service application
        }

    }
}
