using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using Tmc.Common;

namespace Tmc.Vision
{
    public abstract class VisionBase
    {
        /// <summary>
        /// This is used for lower and higher HSV values
        /// </summary>
        public enum HSVRange { Low = 0, High };

        /// <summary>
        ///
        /// </summary>
        public enum HSVdata { Hue = 0, Sat, Val };

        //public enum TC { Black = 0, White, Red, Blue, Green, Bad, Na };

        /// <summary>
        /// This Function lets us detect circles in an image
        /// </summary>
        /// <returns>
        /// it return an CircleF[] which is an array of circles which detected in the image
        /// </returns>
        /// <param name="src">
        /// THis is the input image in which we want to find the circles
        /// </param>
        /// <param name="minRadius">
        /// smallest circle diameter that will be detected, note this is in pixcels.
        /// Minimal radius of the circles to search for
        /// </param>
        /// <param name="maxRadius">
        /// biggest circle diameter that will be detected, note this is in pixcels.
        /// Maximal radius of the circles to search for
        /// </param>
        /// <param name="dp">
        /// Resolution of the accumulator used to detect centers of the circles. For
        /// example, if it is 1, the accumulator will have the same resolution as the
        /// input image, if it is 2 - accumulator will have twice smaller width and height,
        /// etc
        /// </param>
        /// <param name="minDist">
        /// Minimum distance between centers of the detected circles. If the parameter
        /// is too small, multiple neighbor circles may be falsely detected in addition
        /// to a true one. If it is too large, some circles may be missed
        /// </param>
        /// <param name="cannyThresh">
        /// The higher threshold of the two passed to Canny edge detector (the lower
        /// one will be twice smaller).
        /// </param>
        /// <param name="cannyAccumThresh">
        /// Accumulator threshold at the center detection stage. The smaller it is, the
        /// more false circles may be detected. Circles, corresponding to the larger
        /// accumulator values, will be returned first
        /// </param>
        public CircleF[] DetectTablets(Image<Bgr, Byte> src, int minRadius, int maxRadius, double dp, double minDist, int cannyThresh, int cannyAccumThresh)
        {
            Image<Gray, Byte> gray = src.Convert<Gray, Byte>(); //convert source image to grayscale

            //Set canny edge value we are going to use in houghtransform
            Gray cannyThreshold = new Gray(cannyThresh);
            Gray cannyThresholdLinking = new Gray(100);
            Gray circleAccumulatorThreshold = new Gray(cannyAccumThresh);

            CircleF[] circles = gray.HoughCircles(//detect the circles
                cannyThreshold,
                circleAccumulatorThreshold,
                dp,
                minDist,
                minRadius,
                maxRadius
                )[0]; //Get the circles from the first channel

            return circles;//return the circles found
        }

        /// <summary>
        /// This function tells us what color the source image is
        /// </summary>
        /// <param name="src">
        /// Input image of whichw e want to determin the color
        /// </param>
        /// <param name="HSVTabletcolorRange">
        /// 2d array containing all the color ranges of tablets also has min and max for each color, uses HSV color range
        /// </param>
        /// <returns>
        /// Returns the color of the tablet with enum TabletColors
        /// </returns>
        /// <todo>
        /// add the ol, oh into the function, this function will not be used in the future
        /// </todo>
        public TabletColors detectcolor(Image<Bgr, byte> src, Hsv[,] HSVTabletcolorRange)
        {
            int ol = 5;
            int oh = 5;
            MCvScalar srcScalar;

            Image<Hsv, byte> hsv = src.Convert<Hsv, byte>();
            Hsv abc;
            hsv.AvgSdv(out abc, out srcScalar);
            //TabletColors
            if (true == InHSVRange(abc, HSVTabletcolorRange, TabletColors.Green, ol, oh))
            {//green
                return TabletColors.Green;
            }
            else if (true == InHSVRange(abc, HSVTabletcolorRange, TabletColors.Red, ol, oh))
            {//red
                return TabletColors.Red;
            }
            else if (true == InHSVRange(abc, HSVTabletcolorRange, TabletColors.White, ol, oh))
            {//white
                return TabletColors.White;
            }
            else if (true == InHSVRange(abc, HSVTabletcolorRange, TabletColors.Blue, ol, oh))
            {//blue
                return TabletColors.Blue;
            }
            else if (true == InHSVRange(abc, HSVTabletcolorRange, TabletColors.Black, ol, oh))
            {//black
                return TabletColors.Black;
            }
            else
            {
                return TabletColors.Unknown;
            }
        }

        /// <summary>
        /// detects the colo bassed on the HSV range given
        /// </summary>
        /// <param name="srcHSV">
        /// the Hsv value we want to check is a color we know or dont
        /// </param>
        /// <param name="HSVTabletcolorRange">
        /// the range of colors we know
        /// </param>
        /// <returns>
        /// Color of the tablet
        /// </returns>
        public TabletColors detectcolor(Hsv srcHSV, Hsv[,] HSVTabletcolorRange)
        {
            int ol = 0;
            int oh = 0;

            if (true == InHSVRange(srcHSV, HSVTabletcolorRange, TabletColors.Green, ol, oh))
            {//green
                return TabletColors.Green;
            }
            else if (true == InHSVRange(srcHSV, HSVTabletcolorRange, TabletColors.Red, ol, oh))
            {//red
                return TabletColors.Red;
            }
            else if (true == InHSVRange(srcHSV, HSVTabletcolorRange, TabletColors.White, ol, oh))
            {//white
                return TabletColors.White;
            }
            else if (true == InHSVRange(srcHSV, HSVTabletcolorRange, TabletColors.Blue, ol, oh))
            {//blue
                return TabletColors.Blue;
            }
            else if (true == InHSVRange(srcHSV, HSVTabletcolorRange, TabletColors.Black, ol, oh))
            {//black
                return TabletColors.Black;
            }
            else
            {
                return TabletColors.Unknown;
            }
        }

        /// <summary>
        /// Tells us if the the HSV color is in range
        /// </summary>
        /// <param name="srcHsv">
        /// The HSV values we want to check are in range
        /// </param>
        /// <param name="targetHsv">
        /// The range that we want the color to be in
        /// </param>
        /// <param name="color">
        /// THe color that we want it to be in range of since the 'targetHsv' can be indexed for colors
        /// </param>
        /// <param name="lowerLimitExtra">
        /// how much extra lower we want se the lower side of the range
        /// </param>
        /// <param name="higherLimitExtra">
        /// How much higher we want o set higer side of the range
        /// </param>
        /// <returns>
        /// returns true if it is in range, false if it is not
        /// </returns>
        /// <todo>
        /// make lowerLimitsExtra in to HSV or make overload for it
        /// </todo>
        public bool InHSVRange(Hsv srcHsv, Hsv[,] targetHsv, TabletColors color, int lowerLimitExtra, int higherLimitExtra)
        {
            if (targetHsv[(int)color, (int)HSVRange.Low].Hue < targetHsv[(int)color, (int)HSVRange.High].Hue)
            {
                return ((srcHsv.Hue >= (targetHsv[(int)color, (int)HSVRange.Low].Hue - lowerLimitExtra)) &&             //lower values
                (srcHsv.Satuation >= (targetHsv[(int)color, (int)HSVRange.Low].Satuation - lowerLimitExtra)) &&
                (srcHsv.Value >= (targetHsv[(int)color, (int)HSVRange.Low].Value - lowerLimitExtra)) &&
                (srcHsv.Hue <= (targetHsv[(int)color, (int)HSVRange.High].Hue + higherLimitExtra)) &&               //higher values
                (srcHsv.Satuation <= (targetHsv[(int)color, (int)HSVRange.High].Satuation + higherLimitExtra)) &&
                (srcHsv.Value <= (targetHsv[(int)color, (int)HSVRange.High].Value + higherLimitExtra)));
            }
            else
            {
                if (srcHsv.Hue >= (targetHsv[(int)color, (int)HSVRange.Low].Hue - lowerLimitExtra))
                {
                    return ((srcHsv.Hue >= (targetHsv[(int)color, (int)HSVRange.Low].Hue - lowerLimitExtra)) &&             //lower values
                    (srcHsv.Satuation >= (targetHsv[(int)color, (int)HSVRange.Low].Satuation - lowerLimitExtra)) &&
                    (srcHsv.Value >= (targetHsv[(int)color, (int)HSVRange.Low].Value - lowerLimitExtra)) &&
                    (srcHsv.Hue <= (179)) &&               //higher values
                    (srcHsv.Satuation <= (targetHsv[(int)color, (int)HSVRange.High].Satuation + higherLimitExtra)) &&
                    (srcHsv.Value <= (targetHsv[(int)color, (int)HSVRange.High].Value + higherLimitExtra)));
                }
                else if (srcHsv.Hue <= (targetHsv[(int)color, (int)HSVRange.High].Hue + higherLimitExtra))
                {
                    return ((srcHsv.Hue >= (0)) &&             //lower values
                    (srcHsv.Satuation >= (targetHsv[(int)color, (int)HSVRange.Low].Satuation - lowerLimitExtra)) &&
                    (srcHsv.Value >= (targetHsv[(int)color, (int)HSVRange.Low].Value - lowerLimitExtra)) &&
                    (srcHsv.Hue <= (targetHsv[(int)color, (int)HSVRange.High].Hue + higherLimitExtra)) &&               //higher values
                    (srcHsv.Satuation <= (targetHsv[(int)color, (int)HSVRange.High].Satuation + higherLimitExtra)) &&
                    (srcHsv.Value <= (targetHsv[(int)color, (int)HSVRange.High].Value + higherLimitExtra)));
                }
                else return false;
            }
        }

        /// <summary>
        /// Tells us if the the HSV color is in range
        /// </summary>
        /// <param name="srcHsv">
        /// The HSV values we want to check are in range
        /// </param>
        /// <param name="targetHsv">
        /// The range that we want the color to be in
        /// </param>
        /// <param name="lowerLimitExtra">
        /// how much extra lower we want se the lower side of the range
        /// </param>
        /// <param name="higherLimitExtra">
        /// How much higher we want o set higer side of the range
        /// </param>
        /// <returns>
        /// returns true if it is in range, false if it is not
        /// </returns>
        public bool InHSVRange(Hsv srcHsv, Hsv[] targetHsv, int lowerLimitExtra, int higherLimitExtra)
        {
            if (targetHsv[(int)HSVRange.Low].Hue < targetHsv[(int)HSVRange.High].Hue)
            {
                return ((srcHsv.Hue >= (targetHsv[(int)HSVRange.Low].Hue - lowerLimitExtra)) &&             //lower values
                (srcHsv.Satuation >= (targetHsv[(int)HSVRange.Low].Satuation - lowerLimitExtra)) &&
                (srcHsv.Value >= (targetHsv[(int)HSVRange.Low].Value - lowerLimitExtra)) &&
                (srcHsv.Hue <= (targetHsv[(int)HSVRange.High].Hue + higherLimitExtra)) &&               //higher values
                (srcHsv.Satuation <= (targetHsv[(int)HSVRange.High].Satuation + higherLimitExtra)) &&
                (srcHsv.Value <= (targetHsv[(int)HSVRange.High].Value + higherLimitExtra)));
            }
            else
            {
                if (srcHsv.Hue >= (targetHsv[(int)HSVRange.Low].Hue - lowerLimitExtra))
                {
                    return ((srcHsv.Hue >= (targetHsv[(int)HSVRange.Low].Hue - lowerLimitExtra)) &&             //lower values
                    (srcHsv.Satuation >= (targetHsv[(int)HSVRange.Low].Satuation - lowerLimitExtra)) &&
                    (srcHsv.Value >= (targetHsv[(int)HSVRange.Low].Value - lowerLimitExtra)) &&
                    (srcHsv.Hue <= (179)) &&               //higher values
                    (srcHsv.Satuation <= (targetHsv[(int)HSVRange.High].Satuation + higherLimitExtra)) &&
                    (srcHsv.Value <= (targetHsv[(int)HSVRange.High].Value + higherLimitExtra)));
                }
                else if (srcHsv.Hue <= (targetHsv[(int)HSVRange.High].Hue + higherLimitExtra))
                {
                    return ((srcHsv.Hue >= (0)) &&             //lower values
                    (srcHsv.Satuation >= (targetHsv[(int)HSVRange.Low].Satuation - lowerLimitExtra)) &&
                    (srcHsv.Value >= (targetHsv[(int)HSVRange.Low].Value - lowerLimitExtra)) &&
                    (srcHsv.Hue <= (targetHsv[(int)HSVRange.High].Hue + higherLimitExtra)) &&               //higher values
                    (srcHsv.Satuation <= (targetHsv[(int)HSVRange.High].Satuation + higherLimitExtra)) &&
                    (srcHsv.Value <= (targetHsv[(int)HSVRange.High].Value + higherLimitExtra)));
                }
                else return false;
            }
        }

        /*public TabletColors InRange(int srcPeak, Hsv[,] allHsvs, int type)
        {
            if (type == 0)
            {
            }
            foreach(Hsv allHsv in allHsvs)
            {
                //if
            }
        }*/

        /// <summary>
        /// This function allows you to remove everything(change to black) in a image apart from the color you want(change to white)
        /// </summary>
        /// <param name="src">
        /// Image that you want to remove every color apart from yours
        /// </param>
        /// <param name="targetHsv">
        /// THe color range you don't want to be removed, need specify high and low values, uses HSV color range
        /// </param>
        /// <returns>
        /// Returns the image with everything removed apart from what you wanted
        /// </returns>
        public Image<Bgr, Byte> RemoveEverythingButRange(Image<Bgr, Byte> src, Hsv[] targetHsv)//, Hsv colorGood, Hsv colorBad)
        {
            Image<Hsv, Byte> temp = src.Convert<Hsv, Byte>();
            Image<Bgr, Byte> dst;
            Hsv currentPixcelHSV;

            Hsv colorGood = new Hsv();//color we use to replace the correct data
            colorGood.Hue = 0;
            colorGood.Value = 255;
            colorGood.Satuation = 0;

            Hsv colorBad = new Hsv();//color we use to replace the not wanted colors
            colorBad.Hue = 0;
            colorBad.Value = 0;
            colorBad.Satuation = 0;

            for (int i = 0; i < src.Cols; i++)
            {
                for (int j = 0; j < src.Rows; j++)
                {
                    currentPixcelHSV = temp[j, i];
                    if (InHSVRange(currentPixcelHSV, targetHsv, 10, 20) == true)
                    {//make white if we are in range
                        temp[j, i] = colorGood;
                    }
                    else
                    {//make black otherwise
                        temp[j, i] = colorBad;
                    }
                }
            }
            dst = temp.Convert<Bgr, Byte>();//covert back to BGR
            return dst;
        }

        /// <summary>
        /// crops image to wanted size
        /// </summary>
        /// <param name="src">
        /// image that will be croped
        /// </param>
        /// <param name="x">
        /// The starting X location
        /// </param>
        /// <param name="y">
        /// Starting Y location
        /// </param>
        /// <param name="width">
        /// How wide we want to crop from origin that we set
        /// </param>
        /// <param name="height">
        /// How high we want to crop from origin that we set
        /// </param>
        /// <returns>
        /// returns croped image
        /// </returns>
        public Image<Bgr, Byte> CropImage(Image<Bgr, Byte> src, int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle();

            rect.X = x;
            rect.Y = y;
            rect.Width = width;
            rect.Height = height;
            if (x < 0)
            {
                rect.X = 0;
            }
            else if (x > src.Cols)
            {
                rect.X = src.Cols;
            }

            if (y < 0)
            {
                rect.Y = 0;
            }
            else if (y > src.Rows)
            {
                rect.Y = src.Rows;
            }

            if ((rect.X + width) > src.Cols)
            {
                rect.Width = src.Cols - rect.X;
            }

            if ((rect.Y + height) > src.Rows)
            {
                rect.Height = src.Rows - rect.Y;
            }

            return src.GetSubRect(rect).Clone();
        }

        /// <summary>
        /// workout where the peaks in
        /// </summary>
        /// <param name="srcHSV"></param>
        /// <param name="limit"></param>
        /// <param name="hsvPart"></param>
        /// <returns></returns>
        public int[][] getHighLowHSV(float[][] srcHSV, int limit, HSVdata hsvPart)//int hsvPart)
        {
            var HsvList = new List<int[]>();

            int[] hsvLH = new int[2];

            int toggle = 0;

            for (int j = 0; j < 256; j++)
            {
                if (srcHSV[(int)hsvPart][j] > limit)
                {
                    if (toggle == 0)
                    {
                        hsvLH[(int)HSVRange.Low] = j;
                        toggle = 1;
                    }
                }
                else if (srcHSV[(int)hsvPart][j] < limit)
                {
                    if (toggle == 1)
                    {
                        hsvLH[(int)HSVRange.High] = j;
                        HsvList.Add(hsvLH.Clone() as int[]);
                        toggle = 0;
                    }
                }
                if ((j == 255) && (toggle == 1))
                {
                    hsvLH[(int)HSVRange.High] = 255;
                    HsvList.Add(hsvLH.Clone() as int[]);
                    toggle = 0;
                }
            }
            //}
            return HsvList.ToArray();
        }

        /// <summary>
        /// works out hsv histogram of image and give us back an arrays for Hue, saturation, and value
        /// </summary>
        /// <param name="src">
        /// source image we want to get the historgram of
        /// </param>
        /// <returns>
        /// returns 2d array for hue sat and val
        /// </returns>
        public float[][] HsvValueFloatArray(Image<Bgr, Byte> src)
        {
            var HsvList = new List<float[]>();

            float[] HueHist;
            float[] SatHist;
            float[] ValHist;

            HueHist = new float[256];
            SatHist = new float[256];
            ValHist = new float[256];

            DenseHistogram HistoHue = new DenseHistogram(256, new RangeF(0, 256));
            DenseHistogram HistoSat = new DenseHistogram(256, new RangeF(0, 256));
            DenseHistogram HistoVal = new DenseHistogram(256, new RangeF(0, 256));

            Image<Hsv, Byte> hsvColor = src.Convert<Hsv, Byte>();
            Image<Gray, Byte> Comparedimg2Hsv = hsvColor[0];
            Image<Gray, Byte> Comparedimg2Sat = hsvColor[1];
            Image<Gray, Byte> Comparedimg2Val = hsvColor[2];

            HistoHue.Calculate(new Image<Gray, Byte>[] { Comparedimg2Hsv }, true, null);
            HistoSat.Calculate(new Image<Gray, Byte>[] { Comparedimg2Sat }, true, null);
            HistoVal.Calculate(new Image<Gray, Byte>[] { Comparedimg2Val }, true, null);

            //HistoVal.Calculate(

            HistoHue.MatND.ManagedArray.CopyTo(HueHist, 0);
            HistoSat.MatND.ManagedArray.CopyTo(SatHist, 0);
            HistoVal.MatND.ManagedArray.CopyTo(ValHist, 0);

            HsvList.Add(HueHist);
            HsvList.Add(SatHist);
            HsvList.Add(ValHist);

            return HsvList.ToArray();
        }

        /// <summary>
        /// draws points on the image
        /// </summary>
        /// <param name="src">
        /// source image we want to draw on
        /// </param>
        /// <param name="points">
        /// The points we want to draw
        /// </param>
        /// <returns>
        /// Image with the points on them
        /// </returns>
        public Image<Bgr, Byte> DrawPoints(Image<Bgr, Byte> src, Point[] points)
        {
            foreach (Point point in points)
            {//draw dots just for debuging atm
                Rectangle rect = new Rectangle();
                rect.X = (int)point.X;
                rect.Y = (int)point.Y;
                rect.Width = 2;
                rect.Height = 2;
                //if (a == 0)
                //{
                //    src.Draw(rect, new Bgr(Color.Blue), 1);
                //    a++;
                //}
                src.Draw(rect, new Bgr(Color.Red), 1);
                //a++;
            }
            return src;
        }

        /// <summary>
        /// Adds two floats together of size [3][256], currently used to add histograms
        /// </summary>
        /// <param name="srcFloat">
        /// The source float we want to add to
        /// </param>
        /// <param name="srcFloatAdd">
        /// The flaot we want to add to srcFloat
        /// </param>
        /// <returns></returns>
        /// <todo>function name not very discriptive</todo>
        public float[][] addFloats(float[][] srcFloat, float[][] srcFloatAdd)
        {
            //float[,] values= new float[3,256];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    srcFloat[i][j] += srcFloatAdd[i][j];
                }
            }
            return srcFloat;
        }

        /// <summary>
        /// Get us an image of a full tablet, Since tablet round we need lots of smaller image to get all data from it
        /// </summary>
        /// <param name="src">
        /// src image where to tablet is in
        /// </param>
        /// <param name="tablet">
        /// the location of tablet in the source image
        /// </param>
        /// <returns>
        /// List of images of the tablet
        /// </returns>
        /// <todo>
        /// do bottom part of chip corectly
        /// </todo>
        public List<Image<Bgr, Byte>> GetTablet(Image<Bgr, Byte> src, CircleF tablet)
        {
            var TabletList = new List<Image<Bgr, Byte>>();

            double angle1 = Math.Cos(45 * (Math.PI / 180));
            double angle2 = Math.Cos(41 * (Math.PI / 180));
            double angle3 = Math.Cos(37 * (Math.PI / 180));
            double angle4 = Math.Cos(31.5 * (Math.PI / 180));
            double angle5 = Math.Cos(24 * (Math.PI / 180));
            double angle6 = Math.Cos(16 * (Math.PI / 180));

            Point[] points = new Point[11];

            float rad = tablet.Radius - 4;

            points[0].X = (int)(tablet.Center.X - (tablet.Radius * angle1));
            points[0].Y = (int)(tablet.Center.Y - (Math.Sin(45 * (Math.PI / 180)) * rad));

            points[1].X = (int)(tablet.Center.X - (tablet.Radius * angle2));
            points[1].Y = (int)(tablet.Center.Y - (Math.Sin(41 * (Math.PI / 180)) * rad));

            points[2].X = (int)(tablet.Center.X - (tablet.Radius * angle3));
            points[2].Y = (int)(tablet.Center.Y - (Math.Sin(37 * (Math.PI / 180)) * rad));

            points[3].X = (int)(tablet.Center.X - (tablet.Radius * angle4));
            points[3].Y = (int)(tablet.Center.Y - (Math.Sin(31.5 * (Math.PI / 180)) * rad));

            points[4].X = (int)(tablet.Center.X - (tablet.Radius * angle5));
            points[4].Y = (int)(tablet.Center.Y - (Math.Sin(24 * (Math.PI / 180)) * rad));

            points[5].X = (int)(tablet.Center.X - (tablet.Radius * angle6));
            points[5].Y = (int)(tablet.Center.Y - (Math.Sin(16 * (Math.PI / 180)) * rad));

            Image<Bgr, Byte> MID = CropImage(src, (int)(tablet.Center.X - (rad * angle1)), (int)(tablet.Center.Y - (rad * angle1)), (int)((rad * angle1) * 2), (int)((rad * angle1) * 2));

            Image<Bgr, Byte> LS1 = CropImage(src, points[1].X, points[1].Y, points[0].X - points[1].X, ((int)tablet.Center.Y - points[1].Y) * 2);
            Image<Bgr, Byte> LS2 = CropImage(src, points[2].X, points[2].Y, points[1].X - points[2].X, ((int)tablet.Center.Y - points[2].Y) * 2);
            Image<Bgr, Byte> LS3 = CropImage(src, points[3].X, points[3].Y, points[2].X - points[3].X, ((int)tablet.Center.Y - points[3].Y) * 2);
            Image<Bgr, Byte> LS4 = CropImage(src, points[4].X, points[4].Y, points[3].X - points[4].X, ((int)tablet.Center.Y - points[4].Y) * 2);
            Image<Bgr, Byte> LS5 = CropImage(src, points[5].X, points[5].Y, points[4].X - points[5].X, ((int)tablet.Center.Y - points[5].Y) * 2);
            //Image<Bgr, Byte> LS6 = CropImage(src, points[1].X, points[1].Y, points[0].X - points[1].X, ((int)tablet.Center.Y - points[1].Y) * 2);
            Image<Bgr, Byte> RS1 = CropImage(src, ((int)tablet.Center.X + ((int)tablet.Center.X - points[1].X)) - (points[0].X - points[1].X), points[1].Y, points[0].X - points[1].X, ((int)tablet.Center.Y - points[1].Y) * 2);
            Image<Bgr, Byte> RS2 = CropImage(src, ((int)tablet.Center.X + ((int)tablet.Center.X - points[2].X)) - (points[1].X - points[2].X), points[2].Y, points[1].X - points[2].X, ((int)tablet.Center.Y - points[2].Y) * 2);
            Image<Bgr, Byte> RS3 = CropImage(src, ((int)tablet.Center.X + ((int)tablet.Center.X - points[3].X)) - (points[2].X - points[3].X), points[3].Y, points[2].X - points[3].X, ((int)tablet.Center.Y - points[3].Y) * 2);
            Image<Bgr, Byte> RS4 = CropImage(src, ((int)tablet.Center.X + ((int)tablet.Center.X - points[4].X)) - (points[3].X - points[4].X), points[4].Y, points[3].X - points[4].X, ((int)tablet.Center.Y - points[4].Y) * 2);
            Image<Bgr, Byte> RS5 = CropImage(src, ((int)tablet.Center.X + ((int)tablet.Center.X - points[5].X)) - (points[4].X - points[5].X), points[5].Y, points[4].X - points[5].X, ((int)tablet.Center.Y - points[5].Y) * 2);

            Image<Bgr, Byte> TS1 = CropImage(src, points[0].X + (points[1].Y - points[0].Y), points[0].Y - ((points[0].X - points[1].X)), ((int)tablet.Center.Y - points[1].Y) * 2, points[0].X - points[1].X);
            Image<Bgr, Byte> TS2 = CropImage(src, points[0].X + (points[2].Y - points[0].Y), points[0].Y - ((points[0].X - points[2].X)), ((int)tablet.Center.Y - points[2].Y) * 2, points[1].X - points[2].X);
            Image<Bgr, Byte> TS3 = CropImage(src, points[0].X + (points[3].Y - points[0].Y), points[0].Y - ((points[0].X - points[3].X)), ((int)tablet.Center.Y - points[3].Y) * 2, points[2].X - points[3].X);
            Image<Bgr, Byte> TS4 = CropImage(src, points[0].X + (points[4].Y - points[0].Y), points[0].Y - ((points[0].X - points[4].X)), ((int)tablet.Center.Y - points[4].Y) * 2, points[3].X - points[4].X);
            Image<Bgr, Byte> TS5 = CropImage(src, points[0].X + (points[5].Y - points[0].Y), points[0].Y - ((points[0].X - points[5].X)), ((int)tablet.Center.Y - points[5].Y) * 2, points[4].X - points[5].X);

            Image<Bgr, Byte> BS1 = CropImage(src, points[0].X + (points[1].Y - points[0].Y), points[0].Y - ((points[0].X - points[1].X)), ((int)tablet.Center.Y - points[1].Y) * 2, points[0].X - points[1].X);
            Image<Bgr, Byte> BS2 = CropImage(src, points[0].X + (points[2].Y - points[0].Y), points[0].Y - ((points[0].X - points[2].X)), ((int)tablet.Center.Y - points[2].Y) * 2, points[1].X - points[2].X);
            Image<Bgr, Byte> BS3 = CropImage(src, points[0].X + (points[3].Y - points[0].Y), points[0].Y - ((points[0].X - points[3].X)), ((int)tablet.Center.Y - points[3].Y) * 2, points[2].X - points[3].X);
            Image<Bgr, Byte> BS4 = CropImage(src, points[0].X + (points[4].Y - points[0].Y), points[0].Y - ((points[0].X - points[4].X)), ((int)tablet.Center.Y - points[4].Y) * 2, points[3].X - points[4].X);
            Image<Bgr, Byte> BS5 = CropImage(src, points[0].X + (points[5].Y - points[0].Y), points[0].Y - ((points[0].X - points[5].X)), ((int)tablet.Center.Y - points[5].Y) * 2, points[4].X - points[5].X);

            TabletList.Add(MID);

            TabletList.Add(LS1);
            TabletList.Add(LS2);
            TabletList.Add(LS3);
            TabletList.Add(LS4);
            TabletList.Add(LS5);

            TabletList.Add(RS1);
            TabletList.Add(RS2);
            TabletList.Add(RS3);
            TabletList.Add(RS4);
            TabletList.Add(RS5);

            TabletList.Add(TS1);
            TabletList.Add(TS2);
            TabletList.Add(TS3);
            TabletList.Add(TS4);
            TabletList.Add(TS5);

            points[6].X = points[0].X + (points[1].Y - points[0].Y);
            points[6].Y = (points[0].Y - ((points[0].X - points[1].X)));

            points[7].X = points[0].X + (points[2].Y - points[0].Y);
            points[7].Y = points[0].Y - ((points[0].X - points[2].X));
            points[8].X = points[0].X + (points[3].Y - points[0].Y);
            points[8].Y = points[0].Y - ((points[0].X - points[3].X));
            points[9].X = points[0].X + (points[4].Y - points[0].Y);
            points[9].Y = points[0].Y - ((points[0].X - points[4].X));
            points[10].X = points[0].X + (points[5].Y - points[0].Y);
            points[10].Y = points[0].Y - ((points[0].X - points[5].X));
            return TabletList;
        }

        /// <summary>
        /// Convert image to a HSV histogram
        /// </summary>
        /// <param name="tabletList">
        /// List of image we want to get HSV histgram data from
        /// </param>
        /// <returns>
        /// Histogram for HSV of the imput images combined
        /// </returns>
        public float[][] ImagesToHisto(List<Image<Bgr, Byte>> tabletList)
        {
            //tabletList = Tabletcolor(src, tablet);
            float[][] abca = HsvValueFloatArray(tabletList[0]);
            for (int i = 1; i < tabletList.Capacity - 1; i++)
            {
                float[][] abc = HsvValueFloatArray(tabletList[i]);
                abca = addFloats(abca, abc);
            }
            return abca;
        }

        /// <summary>
        /// gives us distance between 2 points
        /// </summary>
        /// <param name="a">
        /// point a
        /// </param>
        /// <param name="b">
        /// point b
        /// </param>
        /// <returns>
        /// distance between point a and b
        /// </returns>
        public double Mag(PointF a, PointF b)
        {
            return Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }

        /// <summary>
        /// Gives us the identity of nearby tablets which appear to be inside the radius of our tablet
        /// </summary>
        /// <param name="knowTablets">
        /// the list of all the tablets in the picture
        /// </param>
        /// <param name="targetTablet">
        /// this is the tablet that we want to check if it is near other tablets which are witin its radius
        /// </param>
        /// <returns>
        /// the tablets which are with the radius of our targetTablet
        /// </returns>
        public bool OtherTabletsNear(CircleF[] knowTablets, CircleF targetTablet)
        {
            //var circleList = new List<CircleF>();
            int a = 0;
            foreach (CircleF knowTablet in knowTablets)
            {
                double Mag = Math.Sqrt(Math.Pow((targetTablet.Center.X - knowTablet.Center.X), 2) + Math.Pow((targetTablet.Center.Y - knowTablet.Center.Y), 2));

                bool b = checkCircles(knowTablet, targetTablet);
                if ((Mag <= targetTablet.Radius) && (checkCircles(knowTablet, targetTablet) == false))
                {//center is in the radius of the circle
                    a++;
                    //circleList.Add(knowTablet);
                    //int a = 0;
                    //targetTablet
                }
                if ((Mag < (targetTablet.Radius + knowTablet.Radius)) && (checkCircles(knowTablet, targetTablet) == false))
                {//if the circle crosses over
                    if (a < 1)
                    {
                        //circleList.Add(knowTablet);
                    }

                    a++;
                    //int a = 0;
                }
            }

            return (a == 0);// circleList.ToArray();

            //return knowTablets;
        }

        /// <summary>
        /// Checks if the 2 circles given are the same or not
        /// </summary>
        /// <param name="circ">
        /// first circle
        /// </param>
        /// <param name="targ">
        /// second circle
        /// </param>
        /// <returns>
        /// returns true if the circles the same, false if it is diffrent
        /// </returns>
        public bool checkCircles(CircleF circ, CircleF targ)
        {
            return ((circ.Radius == targ.Radius) && (circ.Center.X == targ.Center.X) &&
                    (circ.Center.Y == targ.Center.Y) && (circ.Area == targ.Area));
        }

        /// <summary>
        /// This function uses the histogram to see if there is more then one color on the tablet we are looking at
        /// </summary>
        /// <param name="hue">
        /// The hue spikes on the historgram
        /// </param>
        /// <param name="sat">
        /// The saturation spikes on the historgram
        /// </param>
        /// <param name="val">
        /// The value spikes on the historgram
        /// </param>
        /// <param name="circle">Not used</param>
        /// <param name="circles">Not used</param>
        /// <returns>
        /// True if tablet is ok and is a color we know, Flase if a unknown tablet or overlaped by other tablets
        /// </returns>
        public bool FirstPass(int[][] hue, int[][] sat, int[][] val, CircleF circle, CircleF[] circles, Hsv[,] HSVTabletcolorsRanges)
        {
            if ((hue.GetLength(0) == 1) && (sat.GetLength(0) == 1) && (val.GetLength(0) == 1))
            {
                TabletColors a = detectcolor(new Hsv((hue[0][0] + hue[0][1]) / 2, (sat[0][0] + sat[0][1]) / 2, (val[0][0] + val[0][1]) / 2), HSVTabletcolorsRanges);
                if (a != TabletColors.Unknown) return true;
                else return false;
            }
            else
            {
                TabletColors b = detectcolor(new Hsv((hue[0][0] + hue[0][1]) / 2, (sat[0][0] + sat[0][1]) / 2, (val[0][0] + val[0][1]) / 2), HSVTabletcolorsRanges);
                int hueM = hue.GetLength(0) - 1;
                int satM = sat.GetLength(0) - 1;
                int valM = val.GetLength(0) - 1;
                TabletColors c = detectcolor(new Hsv((hue[hueM][0] + hue[hueM][1]) / 2, (sat[satM][0] + sat[satM][1]) / 2, (val[valM][0] + val[valM][1]) / 2), HSVTabletcolorsRanges);
                if (hue.GetLength(0) > 2)
                {
                    hueM = ((hue.GetLength(0) - 1) / 2);
                }
                if (sat.GetLength(0) > 2)
                {
                    satM = ((sat.GetLength(0) - 1) / 2);
                }
                if (val.GetLength(0) > 2)
                {
                    valM = ((val.GetLength(0) - 1) / 2);
                }
                TabletColors d = detectcolor(new Hsv((hue[hueM][0] + hue[hueM][1]) / 2, (sat[satM][0] + sat[satM][1]) / 2, (val[valM][0] + val[valM][1]) / 2), HSVTabletcolorsRanges);

                if ((b != TabletColors.Unknown) && (b == c) && (b == d)) return true;
                else return false;
            }
        }

        /// <summary>
        /// saves image but only in debug
        /// </summary>
        /// <param name="src">
        /// image to save
        /// </param>
        /// <param name="filename">
        /// location to save and name
        /// </param>
        public void saveImage(Image<Bgr, Byte> src, string filename)
        {
#if DEBUG
            src.Save(filename);
#endif
        }
    }
}