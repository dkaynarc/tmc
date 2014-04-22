using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel.Description;
using System.ServiceModel;
using Tmc.Scada.Core;

namespace Tmc.Scada.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ScadaEngine scadaEngine = new ScadaEngine();
            scadaEngine.Add(12, 10);
            MainForm mainForm = new MainForm(scadaEngine);
            
            // Step 1 Create a URI to serve as the base address.
            Uri baseAddress = new Uri("http://localhost:8000/TMC/");

            // Step 2 Create a ServiceHost instance
            ServiceHost selfHost = new ServiceHost(scadaEngine, baseAddress);

            try
            {
                // Step 3 Add a service endpoint.
                //selfHost.AddServiceEndpoint(typeof(IScada), new WSHttpBinding(), "ScadaEngine");

                // Step 4 Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Step 5 Start the service.
                selfHost.Open();
                mainForm.textBox1.Text = "The service is ready.";


                // Close the ServiceHostBase to shutdown the service.
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                mainForm.textBox1.Text = "An exception occurred: {0}" + ce.Message;
                selfHost.Abort();
            }

            Application.Run(mainForm);
        }
    }
}
