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
            //string win1 = "Test Window"; //The name of the window
            //CvInvoke.cvNamedWindow(win1); //Create the window using the specific name
            Image<Gray, Byte> graySoft = src.Convert<Gray, Byte>();
            Image<Gray, Byte> gray = graySoft;

            Gray cannyThreshold = new Gray(cannyThresh);
            Gray cannyThresholdLinking = new Gray(100);
            Gray circleAccumulatorThreshold = new Gray(cannyAccumThresh);

            Image<Gray, Byte> cannyEdges = gray.Canny(cannyThreshold.Intensity, cannyThresholdLinking.Intensity);



            //HaarCascade My_Haar_Cascade = new HaarCascade("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/cascade classifier - Copy/tray5.xml");
            //Image<Gray, byte> img = new Image<Gray, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/cascade classifier/to be croped/samp4.jpg");//to be croped/tape9.jpg");
            //Image<Gray, Byte> cannyEdges2 = img.Canny(cannyThreshold.Intensity, cannyThresholdLinking.Intensity);
            //CvInvoke.cvSmooth(cannyEdges2, cannyEdges2, SMOOTH_TYPE.CV_GAUSSIAN, 13, 13, 1.5, 1);
            //cannyEdges2 = cannyEdges2.Resize(cannyEdges2.Cols / 3, cannyEdges2.Rows/3, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);//divide by 3
            //cannyEdges2 = cannyEdges2.Resize(cannyEdges2.Cols*3 , cannyEdges2.Rows*3 , Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);//divide by 3
            //cannyEdges2.
            //wide     187
            //hirght   167
            //Point[] left = new Point[180];//40 - 200
            //int t, b;
            //for (int i = 0; i < 180; i++)
            //{
            //    left[i].Y = 33 + i;
            //    //left[i].X = scanImg(cannyEdges2, 33 + i, Side.Left);
            //    if ((left[i].X > 200) && (left[i].Y < 130))
            //    {
            //        t = left[i].Y;
            //    }
            //    else if ((left[i].X > 200) && (left[i].Y > 130))
            //    {
            //        b = left[i].Y;
            //        break;
            //    }
            //    //imgC.Draw(qwe.rect, new Bgr(Color.Blue), 1);
            //}//cannyEdges2
            //MCvAvgComp[] trayDetect = My_Haar_Cascade.Detect(img, 1.1, 4, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.SCALE_IMAGE, new Size(30, 30), new Size(70, 70));
            // MCvAvgComp[][] trayDetect = img.HaarCascade.Detect(My_Haar_Cascade, 1.01, 5, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.SCALE_IMAGE, new Size(35, 32));

            //MCvAvgComp[][] trayDetect = img.DetectHaarCascade(My_Haar_Cascade, 1.01, 5, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.SCALE_IMAGE, new Size(35, 32));
            //Image<Bgr, byte> imgC = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/cascade classifier/to be croped/samp4.jpg");
            //foreach (MCvAvgComp qwe in trayDetect)
            //{
            //    //img.ROI = qwe.rect;
            //    imgC.Draw(qwe.rect, new Bgr(Color.Blue), 2);
            //}
            //img.ROI = trayDetect.rect;
            //RectanglesMarker marker = new RectanglesMarker(objects, Color.Fuchsia);
            /*foreach (MCvAvgComp trayDet in trayDetect[0])
            {
                img.Draw(traydet, new Bgr(Color.Red), 2);
            }*/

            //for (int i = 0; i < cannyEdges2.Cols; i++)
            //{
            //    for (int j = 0; j < cannyEdges2.Rows; j++)
            //    {
            //        Gray grayz = cannyEdges2[j, i];
            //        if (grayz.Intensity > 50)
            //        {
            //            grayz.Intensity = 255;
            //            cannyEdges2[j, i] = grayz;
            //        }
            //        else
            //        {
            //            grayz.Intensity = 0;
            //            cannyEdges2[j, i] = grayz;
            //        }
            //    }
            //}
            //Hsv[] HSVT = new Hsv[2];
            //HSVT[(int)HSVRange.Low].Hue           = 19.5;
            //HSVT[(int)HSVRange.Low].Satuation     = 145.24;
            //HSVT[(int)HSVRange.Low].Value         = 183.68;
            //HSVT[(int)HSVRange.High].Hue          = 29;
            //HSVT[(int)HSVRange.High].Satuation    = 331.92;
            //HSVT[(int)HSVRange.High].Value        = 360;

            //Image<Bgr, byte> imgC2 = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/trayY2.jpg");
            //imgC2 = imgC2.Resize(653, 490, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);//divide by 3

            //Image<Bgr, Byte> col = RemoveEverythingButRange(src, HSVT);
            //CvInvoke.cvShowImage(win1, col);
            //CvInvoke.cvWaitKey(0);
            f.pictureBox1_draw(gray);
            CircleF[] circles = gray.HoughCircles(
                cannyThreshold,
                circleAccumulatorThreshold,
                par3,
                par4,
                minCircle,
                maxCircle
                )[0]; //Get the circles from the first channel
            CvInvoke.cvWaitKey(0);
            Image<Bgr, Byte> a = src.Clone();
            foreach (CircleF circle in circles)
            {
                a.Draw(circle, new Bgr(Color.Red), 2);
            }
            //CvInvoke.cvShowImage(win1, a); //Show the image
            f.pictureBox2_draw(a);
            CvInvoke.cvWaitKey(40);//remove move later
            return circles;
        }

        public void calibration(Image<Bgr, byte> src)
        {
            const int width = 9;
            const int height = 6;
            Size patternSize = new Size(width, height);
            Bgr[] line_colour_array = new Bgr[width * height]; // just for displaying coloured lines of detected chessboard
            Image<Gray, Byte>[] Frame_array_buffer = new Image<Gray, byte>[100];
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
            hsv.AvgSdv(out abc, out srcScalar);
            //TabletColors
            if (true == InHSVRange(abc, HSVTabletColourRange, TabletColors.Green, ol, oh))
            {//green
                return TabletColors.Green;
            }
            else if (true == InHSVRange(abc, HSVTabletColourRange, TabletColors.Red, ol, oh))
            {//red
                return TabletColors.Red;
            }
            else if (true == InHSVRange(abc, HSVTabletColourRange, TabletColors.White, ol, oh))
            {//white
                return TabletColors.White;
            }
            else if (true == InHSVRange(abc, HSVTabletColourRange, TabletColors.Blue, ol, oh))
            {//blue
                return TabletColors.Blue;
            }
            else if (true == InHSVRange(abc, HSVTabletColourRange, TabletColors.Black, ol, oh))
            {//black
                return TabletColors.Black;
            }
            else
            {
                return TabletColors.Unknown;
            }
        }


        public bool InHSVRange(Hsv srcHsv, Hsv[,] targetHsv, TabletColors colour, int lowerLimitExtra, int higherLimitExtra)
        {
            return ((srcHsv.Hue >= (targetHsv[(int)colour, (int)HSVRange.Low].Hue - lowerLimitExtra)) &&             //lower values
                (srcHsv.Satuation >= (targetHsv[(int)colour, (int)HSVRange.Low].Satuation - lowerLimitExtra)) &&
                (srcHsv.Value >= (targetHsv[(int)colour, (int)HSVRange.Low].Value - lowerLimitExtra)) &&
                (srcHsv.Hue <= (targetHsv[(int)colour, (int)HSVRange.High].Hue + higherLimitExtra)) &&               //higher values
                (srcHsv.Satuation <= (targetHsv[(int)colour, (int)HSVRange.High].Satuation + higherLimitExtra)) &&
                (srcHsv.Value <= (targetHsv[(int)colour, (int)HSVRange.High].Value + higherLimitExtra)));
        }

        public bool InHSVRange(Hsv srcHsv, Hsv[] targetHsv, int lowerLimitExtra, int higherLimitExtra)
        {
            return ((srcHsv.Hue >= (targetHsv[(int)HSVRange.Low].Hue - lowerLimitExtra)) &&             //lower values
                (srcHsv.Satuation >= (targetHsv[(int)HSVRange.Low].Satuation - lowerLimitExtra)) &&
                (srcHsv.Value >= (targetHsv[(int)HSVRange.Low].Value - lowerLimitExtra)) &&
                (srcHsv.Hue <= (targetHsv[(int)HSVRange.High].Hue + higherLimitExtra)) &&               //higher values
                (srcHsv.Satuation <= (targetHsv[(int)HSVRange.High].Satuation + higherLimitExtra)) &&
                (srcHsv.Value <= (targetHsv[(int)HSVRange.High].Value + higherLimitExtra)));
        }


        public Image<Bgr, Byte> RemoveEverythingButRange(Image<Bgr, Byte> src, Hsv[] targetHsv)//, Hsv colorGood, Hsv colorBad)
        {
            Image<Hsv, Byte> temp = src.Convert<Hsv, Byte>();
            Image<Bgr, Byte> dst;
            Hsv currentPixcelHSV;

            Hsv colorGood = new Hsv();
            //colorGood = new Hsv;
            colorGood.Hue = 0;
            colorGood.Value = 255;
            colorGood.Satuation = 0;

            Hsv colorBad = new Hsv();
            colorBad.Hue = 0;
            colorBad.Value = 0;
            colorBad.Satuation = 0;

            for (int i = 0; i < src.Cols; i++)
            {
                for (int j = 0; j < src.Rows; j++)
                {
                    currentPixcelHSV = temp[j, i];
                    if (InHSVRange(currentPixcelHSV, targetHsv, 10, 20) == true)
                    {//make colour specified if all true
                        temp[j, i] = colorGood;
                    }
                    else
                    {//make black
                        //temp[j, i].Hue
                        temp[j, i] = colorBad;
                    }
                }
            }
            dst = temp.Convert<Bgr, Byte>();
            return dst;
        }

        public Image<Bgr, Byte> CropImage(Image<Bgr, Byte> src, int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle();

            rect.X = x;
            rect.Y = y;
            rect.Width = width;
            rect.Height = height;

            return src.GetSubRect(rect);
        }
    }
}
