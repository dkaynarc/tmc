using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;

namespace Mobile
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
            WebServiceClient webService = new WebServiceClient();
            double i = webService.Add(10, 10);
            //Debug.WriteLine(webService.Add(10,10));
            //MobileForm mobileForm = new MobileForm(null);
            Application.Run(new MobileForm());
        }
    }
}
