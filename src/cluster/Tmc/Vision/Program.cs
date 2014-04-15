using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tmc.Vision
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        //private Camera SorterCamera;
        //private Camera TrayCamera;
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //SorterVision sort = new SorterVision();
            //sort.hi();
            Camera SorterCamera         = new Camera();
            Camera TrayDetectionCamera  = new Camera();


        }
    }
}
