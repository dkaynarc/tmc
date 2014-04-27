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
            scadaEngine.Name = "Initial";
            scadaEngine.Add(12, 10);
            MainForm mainForm = new MainForm(scadaEngine);
            var wcfHost = new WcfHost(scadaEngine);
            wcfHost.Open();

            Application.Run(mainForm);

            wcfHost.Close();
        }
    }
}
