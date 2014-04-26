using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Step 1: Create an instance of the WCF proxy.
            WebService webService = new WebService();

            // Step 2: Call the service operations.
            // Call the Add service operation.
            double value1 = 100.00D;
            double value2 = 10.00D;
            double result = webService.Add(value1, value2);
            Console.WriteLine("Add({0},{1}) = {2}", value1, value2, result);

            // Step 1 Create a URI to serve as the base address.
            Uri baseAddress = new Uri("http://localhost:8001/TMC/");

            // Step 2 Create a ServiceHost instance
            ServiceHost selfHost = new ServiceHost(webService, baseAddress);

            try
            {
                // Step 3 Add a service endpoint.
                selfHost.AddServiceEndpoint(typeof(IWebService), new WSHttpBinding(), "WebService");
                // Step 4 Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Step 5 Start the service.
                selfHost.Open();
                //mainForm.textBox1.Text = "The service is ready.";
                // Close the ServiceHostBase to shutdown the service.
                //selfHost.Close();
                for(;;)
                { }
            }
            catch (CommunicationException ce)
            {
                selfHost.Abort();
                Console.WriteLine(ce.Message);
            }
        }
    }
}
