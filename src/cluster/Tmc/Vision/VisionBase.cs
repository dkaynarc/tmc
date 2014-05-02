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
using Tmc.Common;


namespace Tmc.Vision
{
    public abstract class VisionBase
    {
        //public enum TabletColors { Green, Red, White, Blue, Black, Unknown, None };
        public enum HSVRange { Low = 0, High };

        public CircleF[] DetectTablets(Image<Bgr, Byte> src, int minCircle, int maxCircle, double par3, double par4, int cannyThresh, int cannyAccumThresh, Form1 f)
        {
            //CircleF[] circle  = ;
            string win1 = "Test Window"; //The name of the window
            CvInvoke.cvNamedWindow(win1); //Create the window using the specific name
            Image<Gray, Byte> graySoft = src.Convert<Gray, Byte>();
            Image<Gray, Byte> gray = graySoft;

            Gray cannyThreshold = new Gray(cannyThresh);
            Gray cannyThresholdLinking = new Gray(100);
            Gray circleAccumulatorThreshold = new Gray(cannyAccumThresh);

            Image<Gray, Byte> cannyEdges = gray.Canny(cannyThreshold.Intensity, cannyThresholdLinking.Intensity);

            f.pictureBox1_draw(gray);
            CircleF[] circles = gray.HoughCircles(
                cannyThreshold,
                circleAccumulatorThreshold,
                par3,
                par4,
                minCircle,
                maxCircle
                )[0]; //Get the circles from the first channel
           
            Image<Bgr,Byte> a= src.Clone();
            foreach (CircleF circle in circles)
            {
                a.Draw(circle, new Bgr(Color.Red), 2);
            }
            CvInvoke.cvShowImage(win1, a); //Show the image
            f.pictureBox2_draw(a);
            CvInvoke.cvWaitKey(40);//remove move later
            return circles;
        }

        public void calibration(Image<Bgr, byte> src)
        {
            const int width     = 9;
            const int height = 6;
            Size patternSize = new Size(width, height);
            Bgr[] line_colour_array = new Bgr[width * height]; // just for displaying coloured lines of detected chessboard
            Image<Gray, Byte>[] Frame_array_buffer = new Image<Gray,byte>[100];
            MCvPoint3D32f[][] corners_object_list = new MCvPoint3D32f[Frame_array_buffer.Length][];
            PointF[][] corners_points_list = new PointF[Frame_array_buffer.Length][];
        }

        public TabletColors detectColour(Image<Bgr, byte> src, Hsv[,] HSVTabletColourRange)
        {
            int ol = 5;
            int oh = 5;
            MCvScalar srcScalar;

            Image<Hsv, byte> hsv = src.Convert<Hsv, byte>();
            Hsv abc;
            hsv.AvgSdv(out abc,out srcScalar);
            //TabletColors
            if (true == InHSVRange(abc, HSVTabletColourRange, TabletColors.Green, ol, oh))
            {//green
                return TabletColors.Green;
            }
            else if (true == InHSVRange(abc, HSVTabletColourRange,TabletColors.Red, ol, oh))
            {//red
                return TabletColors.Red;
            }
            else if (true == InHSVRange(abc, HSVTabletColourRange,TabletColors.White, ol, oh))
            {//white
                return TabletColors.White;
            }
            else if (true == InHSVRange(abc, HSVTabletColourRange,TabletColors.Blue, ol, oh))
            {//blue
                return TabletColors.Blue;
            }
            else if (true == InHSVRange(abc, HSVTabletColourRange,TabletColors.Black, ol, oh))
            {//black
                return TabletColors.Black;
            }
            else 
            {
                return TabletColors.Unknown;
            }
        }

        public bool InHSVRange(Hsv srcHsv, Hsv[,] targetHsv, TabletColors colour, int lowerLimitExtra,int higherLimitExtra)
        {
            return ((srcHsv.Hue >= (targetHsv[(int)colour,(int)HSVRange.Low].Hue - lowerLimitExtra)) && (srcHsv.Satuation >= (targetHsv[(int)colour,(int)HSVRange.Low].Satuation - lowerLimitExtra)) && (srcHsv.Value >= (targetHsv[(int)colour,(int)HSVRange.Low].Value - lowerLimitExtra)) &&
                (srcHsv.Hue <= (targetHsv[(int)colour,(int)HSVRange.High].Hue + higherLimitExtra)) && (srcHsv.Satuation <= (targetHsv[(int)colour,(int)HSVRange.High].Satuation + higherLimitExtra)) && (srcHsv.Value <= (targetHsv[(int)colour,(int)HSVRange.High].Value + higherLimitExtra)));
        }
    }
}
