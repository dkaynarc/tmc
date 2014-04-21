using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.GPU;

namespace Tmc.Vision
{
    class TrayDetectorVision
    {
        private int[] Xarray;
        private int[] Yarray;
        private Camera camera;
        public Point p;
        Image<Bgr, Byte> img;
        Image<Bgr, Byte> imgTray;
        public TrayDetectorVision(Camera camera)
        {
            this.camera = camera;
        }

        public void RunTrayDetectionVision()
        {
            img = camera.GetImage();
            string win1 = "Test Window"; //The name of the window
            CvInvoke.cvNamedWindow(win1); //Create the window using the specific name
            //BitmapImage image = new Bi6tmapImage(new Uri("http://192.168.0.11:8080/photo.jpg"));
            img = img.Resize(1088, 816, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            CvInvoke.cvShowImage(win1, img); //Show the image
            CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            CvInvoke.cvDestroyWindow(win1); //Destory the window
            DetectTray();
            DetectTabletsInTray();
        }

        private bool DetectTray()
        {

            return true;
        }

        private void DetectTabletsInTray()
        {
            
        }

        /*private void DetectTrayType()
        {
            
        }*/

        /*private bool CheckTrayEmpty()
        {

            return true;
        }*/
    }
}
