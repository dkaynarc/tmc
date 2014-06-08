using System;
using System.Collections.Generic;

using Tmc.Common;

namespace Tmc.Vision
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        //private Camera SorterCamera;
        //private Camera TrayCamera;
        private static void Main()
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
        
        private static void TestCamera()
        {
            Camera trayC = new Camera();
            trayC.ConnectionString = new Uri(@"http://192.168.1.195:8080/photoaf.jpg");
            Camera sort = new Camera();
            sort.ConnectionString = new Uri(@"http://192.168.1.195:8080/photoaf.jpg");
            //c.SetParameters(new Dictionary<string, string> { { "Name", "TestCam1" }, { "ConnectionString", "http://192.168.0.11:8080/photo.jpg" } });//"http://192.168.0.11:8080/photo.jpg"
            //trayC.Initialise();
            //sort.Initialise();
            //TrayDetectorVision tray = new TrayDetectorVision(trayC);
            //Tray<Tablet> Tray = tray.GetTabletsInTray();
            SorterVision sorter = new SorterVision(sort);
            sorter.Calibrate();
            while (true)
            {
                List<Tablet> tabletList = sorter.GetVisibleTablets();
            }
            //CvInvoke.cvWaitKey(0);
        }
    }
}