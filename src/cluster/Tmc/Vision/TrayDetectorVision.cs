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
    public class TrayDetectorVision : VisionBase
    {
        private int minCircle, maxCircle;
        private int cannyThresh, cannyAccumThresh;
        double par3, par4;
        //private int[] Xarray;
        //private int[] Yarray;
        private Camera camera;
        private Point[] trayPoints;
        private Image<Bgr, Byte> img;
        private Image<Bgr, Byte> imgTray;
        Form1 f;
        public TrayDetectorVision(Camera camera)
        {
            f = new Form1();
            f.Show();
            this.camera = camera;
        }

        public void RunTrayDetectionVision()
        {
            img = camera.GetImage();
            string win1 = "Test Window"; //The name of the window
            CvInvoke.cvNamedWindow(win1); //Create the window using the specific name
            //BitmapImage image = new Bi6tmapImage(new Uri("http://192.168.0.11:8080/photo.jpg"));
            img = img.Resize(1088, 816, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);//divide by 3
            CvInvoke.cvShowImage(win1, img); //Show the image
            CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            
            DetectTray();
            CvInvoke.cvShowImage(win1, imgTray); //Show the image
            CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            DetectTabletsInTray();
            CvInvoke.cvDestroyWindow(win1); //Destory the window
        }

        private bool DetectTray()
        {
            Rectangle rect = new Rectangle();
            rect.X = 360;
            rect.Y = 220;
            rect.Width = 420+100;
            rect.Height= 400;
            imgTray = img.GetSubRect(rect);
            return true;
        }

        private void DetectTabletsInTray()
        {
            while (true)
            {
                f.getValue(ref minCircle, ref maxCircle, ref par3, ref par4, ref cannyThresh,ref cannyAccumThresh);
                DetectTablets(imgTray, minCircle, maxCircle, par3, par4, cannyThresh, cannyAccumThresh, f);
            }
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
