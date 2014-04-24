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
    public abstract class VisionBase
    {

       // VisionBase()
        //{ 
            
        //}

        public CircleF[] DetectTablets(Image<Bgr, Byte> src, int minCircle, int maxCircle, double par3, double par4, int cannyThresh, int cannyAccumThresh, Form1 f)
        {
            //CircleF[] circle  = ;
            string win1 = "Test Window"; //The name of the window
            CvInvoke.cvNamedWindow(win1); //Create the window using the specific name

            Image<Gray, Byte> graySoft = src.Convert<Gray, Byte>();//.PyrDown().PyrUp().Clone();
            Image<Gray, Byte> gray = graySoft;//.SmoothGaussian(3);
            //gray = gray.AddWeighted(graySoft, 1.5, -0.5, 0);

            //Image<Gray, Byte> bin = gray.ThresholdBinary(new Gray(0), new Gray(255));

            Gray cannyThreshold = new Gray(cannyThresh);
            Gray cannyThresholdLinking = new Gray(100);
            Gray circleAccumulatorThreshold = new Gray(cannyAccumThresh);

            Image<Gray, Byte> cannyEdges = gray.Canny(cannyThreshold.Intensity, cannyThresholdLinking.Intensity);

            //CvInvoke.cvShowImage(win1, gray); //Show the image
            //CvInvoke.cvWaitKey(0);  //Wait for the key pressing event

            //CvInvoke.cvShowImage(win1, cannyEdges); //Show the image
            //CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            //pictureBox1.Image = cannyEdges.ToBitmap();
            //Form1 f = new Form1();
            //f.Show();
            f.pictureBox1_draw(gray);
            //pictureBox1.Image = cannyEdges.ToBitmap();
            //Circles
            //cannyEdges.Height = par2;
            CircleF[] circles = gray.HoughCircles(
                cannyThreshold,//new Gray(150),
                circleAccumulatorThreshold,//new Gray(75),
                par3,
                par4,
                minCircle,//5,
                maxCircle//60
                //cannyThreshold,
                //circleAccumulatorThreshold,
                //par1,//1.0, //Resolution of the accumulator used to detect centers of the circles
                //par2 / 8, //min distance  cannyEdges.Height
                //min, //min radius
                //max //max radius
                )[0]; //Get the circles from the first channel
           // if (max == 60)
            //{
              //  max = 60;
            //}
            //draw circles (on original image)
            Image<Bgr,Byte> a= src.Clone();
            foreach (CircleF circle in circles)
            {
                a.Draw(circle, new Bgr(Color.Red), 2);
            }
            CvInvoke.cvShowImage(win1, a); //Show the image
            //CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            f.pictureBox2_draw(a);
            CvInvoke.cvWaitKey(40);
            return circles;
        }

        public void DetectTabletType()
        {
            
        }
    }
}
