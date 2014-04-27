using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiServer
{
    class Starter
    {
        public void Start()
        {
            var hostname = System.Environment.MachineName;
            string rootAddress = hostname + "9000";  // change this to the current ip address of your machine - else - exception
            using (WebApp.Start<Startup>(rootAddress))
            {
                Console.WriteLine("Waiting for clients to connect...");
                Console.ReadLine();
            }
        }


    }
}
