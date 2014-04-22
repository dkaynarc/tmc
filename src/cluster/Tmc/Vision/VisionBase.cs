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
    abstract class VisionBase: Form1
    {

       // VisionBase()
        //{ 
            
        //}

        public CircleF[] DetectTablets(Image<Bgr, Byte> src, int max, int min,double par1,double par2,Form1 f)
        {
            //CircleF[] circle  = ;
            string win1 = "Test Window"; //The name of the window
            CvInvoke.cvNamedWindow(win1); //Create the window using the specific name

            Image<Gray, Byte> graySoft = src.Convert<Gray, Byte>().PyrDown().PyrUp();
            Image<Gray, Byte> gray = graySoft.SmoothGaussian(3);
            gray = gray.AddWeighted(graySoft, 1.5, -0.5, 0);

            //Image<Gray, Byte> bin = gray.ThresholdBinary(new Gray(0), new Gray(255));

            Gray cannyThreshold = new Gray(100);
            Gray cannyThresholdLinking = new Gray(100);
            Gray circleAccumulatorThreshold = new Gray(100);

            Image<Gray, Byte> cannyEdges = gray.Canny(cannyThreshold.Intensity, cannyThresholdLinking.Intensity);

            //CvInvoke.cvShowImage(win1, gray); //Show the image
            //CvInvoke.cvWaitKey(0);  //Wait for the key pressing event

            //CvInvoke.cvShowImage(win1, cannyEdges); //Show the image
            //CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            //pictureBox1.Image = cannyEdges.ToBitmap();
            //Form1 f = new Form1();
            //f.Show();
            f.pictureBox1_draw(cannyEdges);
            //pictureBox1.Image = cannyEdges.ToBitmap();
            //Circles
            //cannyEdges.Height = par2;
            CircleF[] circles = gray.HoughCircles(
                cannyThreshold,
                circleAccumulatorThreshold,
                par1,//1.0, //Resolution of the accumulator used to detect centers of the circles
                par2 / 8, //min distance  cannyEdges.Height
                min, //min radius
                max //max radius
                )[0]; //Get the circles from the first channel

            //draw circles (on original image)
            foreach (CircleF circle in circles)
                src.Draw(circle, new Bgr(Color.Brown), 2);
            //CvInvoke.cvShowImage(win1, src); //Show the image
            //CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            f.pictureBox2_draw(src);
            CvInvoke.cvWaitKey(40);
            return circles;
        }

        public void DetectTabletType()
        {
            
        }
    }
}
