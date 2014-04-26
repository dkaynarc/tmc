using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1() 
            };
            ServiceBase.Run(ServicesToRun);
            //Step 1: Create an instance of the WCF proxy.
            ScadaClient client = new ScadaClient();

            // Step 2: Call the service operations.
            // Call the Add service operation.
            double value1 = 100.00D;
            double value2 = 10.00D;
            double result = client.Add(value1, value2);
            Debug.WriteLine("Add({0},{1}) = {2}", value1, value2, result);

            // Call the Subtract service operation.
            //value1 = 145.00D;
            //value2 = 76.54D;
            //result = client.Subtract(value1, value2);
            //Console.WriteLine("Subtract({0},{1}) = {2}", value1, value2, result);

            //// Call the Multiply service operation.
            //value1 = 9.00D;
            //value2 = 81.25D;
            //result = client.Multiply(value1, value2);
            //Console.WriteLine("Multiply({0},{1}) = {2}", value1, value2, result);

            //// Call the Divide service operation.
            //value1 = 22.00D;
            //value2 = 7.00D;
            //result = client.Divide(value1, value2);
            //Console.WriteLine("Divide({0},{1}) = {2}", value1, value2, result);

            //Step 3: Closing the client gracefully closes the connection and cleans up resources.
            //client.Close();
        }
    }
}
