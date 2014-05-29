using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tmc.Common;

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
            //Form1();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            ////SorterVision sort = new SorterVision();
            ////sort.hi();
            //Camera SorterCamera         = new Camera();
            //Camera TrayDetectionCamera  = new Camera();
            TestCamera();
        }

        static void TestCamera()
        {
            Camera c = new Camera();
            //c.SetParameters(new Dictionary<string, string> { { "Name", "TestCam1" }, { "ConnectionString", "http://192.168.0.11:8080/photo.jpg" } });//"http://192.168.0.11:8080/photo.jpg"
            c.Initialise();
            //TrayDetectorV2ision tray = new TrayDetectorVision(c);
            //tray.GetTabletsInTray();
            SorterVision sorter = new SorterVision(c);
            sorter.Calibration();
            while (true)
            {
                List<Tablet> tabletList = sorter.GetVisibleTablets();
            }
            //CvInvoke.cvWaitKey(0);
        }
    }
}
