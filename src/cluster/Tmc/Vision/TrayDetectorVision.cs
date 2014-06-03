using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Diagnostics;
using System.Drawing;
using Tmc.Common;

namespace Tmc.Vision
{
    public class TrayDetectorVision : VisionBase
    {
        private int minRadius, maxRadius, cannyThresh, cannyAccumThresh;
        private double dp, minDist;
        private Hsv[,] HSVTabletcolorsRanges = new Hsv[5, 2];

        private Camera camera;
        private Point[] trayPoints = new Point[4];
        private Image<Bgr, Byte> img;
        private Image<Bgr, Byte> imgTray;
        private CircleF[] tablets;
        private double Angle;
        private Tray<Tablet> trayList = new Tray<Tablet>();
        private TraceListener listener = new DelimitedListTraceListener(@"debugfileTray.txt");

        /// <summary>
        ///
        /// </summary>
        public enum Side { Left = 0, Right };

        /// <summary>
        ///
        /// </summary>
        public enum pMinMax { Max = 0, Min };

        /// <summary>
        /// Constructor for this class
        /// </summary>
        /// <param name="camera">
        /// this gives us the camera object which we use to get images from
        /// </param>
        public TrayDetectorVision(Camera camera)
        {
            this.camera = camera;
            Debug.Listeners.Add(listener);

            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.Low].Hue = 50;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.Low].Satuation = 34;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.Low].Value = 167;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.High].Hue = 94;//87;//74;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.High].Satuation = 103;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.High].Value = 255;//229;//222;

            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.Low].Hue = 0;//176;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.Low].Satuation = 90;//93;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.Low].Value = 224;//198;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.High].Hue = 8;//171;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.High].Satuation = 155;//128;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.High].Value = 255;//250;

            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.Low].Hue = 0;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.Low].Satuation = 0;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.Low].Value = 252;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.High].Hue = 166;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.High].Satuation = 31;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.High].Value = 255;

            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Hue = 102;//112;//115;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Satuation = 34;//76;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Value = 132;//69;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.High].Hue = 137;//126;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.High].Satuation = 140;//124;//125;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.High].Value = 255;// 235;//226;// 214;//213;

            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.Low].Hue = 164;//102;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.Low].Satuation = 23;//15;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.Low].Value = 69;//90;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.High].Hue = 12;//145;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.High].Satuation = 77;//39;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.High].Value = 201;//167;

            minRadius = 54;
            maxRadius = 61;

            dp = 2.0;
            minDist = 20;
            cannyThresh = 2;
            cannyAccumThresh = 80;

            //this.camera.ConnectionString = new Uri(@"http://192.168.0.190:8080/photoaf.jpg");
            //this.camera.ConnectionString = new Uri(@"https://fbcdn-sphotos-c-a.akamaihd.net/hphotos-ak-ash3/t1.0-9/10247212_10202692742692192_8562559696417032763_n.jpg");
        }

        /// <summary>
        /// This function is resoncible for working out the state of trays
        /// </summary>
        /// <returns>
        /// it returns the state of the tray
        /// </returns>
        /// <todo>use the new function in vision base to detect tablet colour</todo>
        public Tray<Tablet> GetTabletsInTray()
        {
            img = camera.GetImage();//C:/Users/leonid/Dropbox/ICT DESIGN/Assignment 3/vision/cal
            //img = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICT DESIGN/Assignment 3/vision/cal/tray100.jpg");
            //img = camera.GetImageHttp(new Uri(@"http://www.wwrd.com.au/images/P/2260248_Fable%20s-4%2016cm%20Accent%20Plates-652383734586-co.jpg"));

            Image<Bgr, Byte> src = CropImage(img, 0, 777, img.Cols, 902);//reduce the image so we only see the coveour and tray

            saveImage(src, "croped Image.jpg");

            DetectTray(src);//make a ref angle

            trayList.Angle = Angle;

            foreach (Point traypoint in trayPoints)
            {//draw dots just for debuging atm
                Rectangle rect = new Rectangle();
                rect.X = traypoint.X;
                rect.Y = traypoint.Y;
                rect.Width = 2;
                rect.Height = 2;
                src.Draw(rect, new Bgr(Color.Red), 6);
            }

            saveImage(src, "Image with points.jpg");

            DetectTabletsInTray();
            DetectTabletType();

            return trayList;
        }

        /// <summary>
        /// Detects if the tray is there, and works out it's location
        /// </summary>
        /// <param name="src">
        /// image that contains the tray
        /// </param>
        /// <returns>true if tray found, false if not</returns>
        /// <todo>
        /// do the returns
        /// </todo>
        private bool DetectTray(Image<Bgr, Byte> src)
        {
            Point[] line = new Point[2];    //holds the location of the start and end yellow line in the picture
            double angle;                   //angle of the tray on the conveyor

            Hsv[] HSVT = new Hsv[2];         //hold the color of the yellow line
            HSVT[(int)HSVRange.Low].Hue = 19;
            HSVT[(int)HSVRange.Low].Satuation = 80;//108.24;
            HSVT[(int)HSVRange.Low].Value = 240;//183.68;
            HSVT[(int)HSVRange.High].Hue = 29;
            HSVT[(int)HSVRange.High].Satuation = 331.92;
            HSVT[(int)HSVRange.High].Value = 360;

            Image<Bgr, Byte> col = RemoveEverythingButRange(src, HSVT); //we want to remove everything that is not yellow

            int rows = col.Rows;
            int cols = col.Cols;

            col = col.Resize((int)(cols / 2.5), (int)(rows / 2.5), INTER.CV_INTER_AREA);
            col = col.Resize(cols, rows, INTER.CV_INTER_AREA);

            Image<Gray, Byte> Gsrc = col.Convert<Gray, Byte>();

            line = scanImgForLine(Gsrc);        //get location of the yellow line

            angle = AngleOfTray(line);          //get the angle of the tray
            angle -= 90;                        //remove 90 so the angle is starting from zero

            Angle = angle;//make ref

            //double Mag = Math.Sqrt(Math.Pow((line[0].X - line[1].X),2) + Math.Pow((line[0].Y - line[1].Y),2) );

            saveImage(col, "only yellow.jpg");

            int x = line[0].Y - line[1].Y;
            int y = line[0].X - line[1].X;

            //work out the points
            trayPoints[1].X = line[1].X;
            trayPoints[1].Y = line[1].Y;

            trayPoints[3].X = line[0].X;
            trayPoints[3].Y = line[0].Y;

            trayPoints[0].X = (line[1].X - x);
            trayPoints[0].Y = (line[1].Y + y);

            trayPoints[2].X = (line[0].X - x);
            trayPoints[2].Y = (line[0].Y + y);

            //work out the value we need to crop the image
            int xOrig = Math.Min(Math.Min(trayPoints[0].X, trayPoints[2].X), Math.Min(trayPoints[1].X, trayPoints[3].X));
            int yOrig = Math.Min(Math.Min(trayPoints[0].Y, trayPoints[2].Y), Math.Min(trayPoints[1].Y, trayPoints[3].Y));
            int xWidth = Math.Max(Math.Max(trayPoints[0].X, trayPoints[2].X), Math.Max(trayPoints[1].X, trayPoints[3].X)) - xOrig;
            int yHeight = Math.Max(Math.Max(trayPoints[0].Y, trayPoints[2].Y), Math.Max(trayPoints[1].Y, trayPoints[3].Y)) - yOrig;

            imgTray = CropImage(src, xOrig, yOrig, xWidth, yHeight);//crop the image

            saveImage(imgTray, "only tray.jpg");

            return true;
        }

        /// <summary>
        /// Ussed to detect tablets in the tray
        /// </summary>
        private void DetectTabletsInTray()
        {
            tablets = DetectTablets(imgTray, minRadius, maxRadius, dp, minDist, cannyThresh, cannyAccumThresh);
        }

        /// <summary>
        /// Used to detect the color of the tablet, only recognises good tablets and assemble the tray list
        /// </summary>
        /// <todo>
        /// move to vision base and make it more advance with the color detect
        /// </todo>
        private void DetectTabletType()
        {
            TabletColors tabletcolor;
            int cellInTray;

            foreach (CircleF tablet in tablets)
            {
                float[][] abca = ImagesToHisto(GetTablet(imgTray, tablet));

                int[][] hue = getHighLowHSV(abca, 50, HSVdata.Hue);
                int[][] sat = getHighLowHSV(abca, 50, HSVdata.Sat);
                int[][] val = getHighLowHSV(abca, 50, HSVdata.Val);

                if (FirstPass(hue, sat, val, tablet, tablets, HSVTabletcolorsRanges) == true)
                {
                    imgTray.Draw(tablet, new Bgr(Color.Green), 1);
                    tabletcolor = detectcolor(new Hsv((hue[0][0] + hue[0][1]) / 2, (sat[0][0] + sat[0][1]) / 2, (val[0][0] + val[0][1]) / 2), HSVTabletcolorsRanges);
                    //imgTray = CoverTablet(imgTray, tablet, 3);
                }
                else
                {
                    tabletcolor = TabletColors.Unknown;
                    imgTray.Draw(tablet, new Bgr(Color.Red), 1);
                    //imgTray = CoverTablet(imgTray, tablet, 3);
                }
                cellInTray = FindCellInTrayForTablet(imgTray.Cols, imgTray.Rows, tablet);

                ///cellInTray      = FindCellInTrayForTablet(imgTray.Cols, imgTray.Rows, tablet);
                trayList.Cells[cellInTray] = new Tablet { Color = tabletcolor };

                Debug.WriteLine(DateTime.Now.ToString("h:mm:ss tt>>  ") + "cell: " + cellInTray + " has tablet:" + tabletcolor.ToString());
                Debug.Flush();
            }
            for (int i = 0; i < 9; i++)
            {//check we only have one tablet ineach cell
                int count;
                count = DetectIfMoreThenOneTableInCell(imgTray, tablets, i);
                if (count > 1)
                {
                    trayList.Cells[i] = new Tablet { Color = TabletColors.Unknown };//TabletColors.Unknown;

                    Debug.WriteLine(DateTime.Now.ToString("h:mm:ss tt>>  ") + "cell: " + i + " has to many tablets:");
                    Debug.Flush();
                }
            }

            saveImage(imgTray, "tray with circles.jpg");
        }

        /// <summary>
        /// lets us cover up a tablet with black
        /// </summary>
        /// <param name="src">
        /// source image we use to cover up tablet
        /// </param>
        /// <param name="tablet">
        /// lication of tablet in src
        /// </param>
        /// <param name="extra">
        /// how much bigger to we want the cover up t be
        /// </param>
        /// <returns>
        /// the image with covered up tablet
        /// </returns>
        private Image<Bgr, Byte> CoverTablet(Image<Bgr, Byte> src, CircleF tablet, int extra)
        {
            Image<Bgr, Byte> ret = src.Clone();

            int blue = 0;
            int green = 0;
            int red = 0;

            Bgr coverColor = new Bgr(blue, green, red);

            int x = (int)(tablet.Center.X - tablet.Radius) - extra;
            int y = (int)(tablet.Center.Y - tablet.Radius) - extra;

            int wide = (int)(tablet.Radius * 2) + extra * 2;

            for (int i = y; i < wide + y; i++)
            {
                for (int j = x; j < wide + x; j++)
                {
                    ret[i, j] = coverColor;
                }
            }
            return ret;
        }

        /// <summary>
        /// This function tells us how many tablets we have detected in each cell
        /// </summary>
        /// <param name="src">
        /// Image of the tray
        /// </param>
        /// <param name="tablets">
        /// list of tablets
        /// </param>
        /// <param name="cell">
        /// The cell we are testing
        /// </param>
        /// <returns>
        /// Number of tablets on the cell
        /// </returns>
        private int DetectIfMoreThenOneTableInCell(Image<Bgr, Byte> src, CircleF[] tablets, int cell)
        {
            int count = 0;
            int cellInTray;
            foreach (CircleF tablet in tablets)
            {
                cellInTray = FindCellInTrayForTablet(imgTray.Cols, imgTray.Rows, tablet);
                if (cellInTray == cell)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// This Function gives us the cell a tablet is in givent it's location in the tray
        /// </summary>
        /// <param name="cols">
        /// the width
        /// </param>
        /// <param name="rows">
        /// the height
        /// </param>
        /// <param name="circle">
        /// contains the location if the circles
        /// </param>
        /// <todo>
        /// //change this so it can have angled lines, not sure if needed but
        /// </todo>
        private int FindCellInTrayForTablet(int cols, int rows, CircleF circle)
        {
            int[] lineX = { 0, (int)(cols / 3), (int)((cols / 3) * 2), cols };
            int[] lineY = { 0, (int)(rows / 3), (int)((rows / 3) * 2), rows };

            if ((circle.Center.X < lineX[1]) && (circle.Center.Y < lineY[3]) &&
                (circle.Center.X > lineX[0]) && (circle.Center.Y > lineY[2]))
            {//cell 0
                return 0;
            }
            else if ((circle.Center.X < lineX[1]) && (circle.Center.Y < lineY[2]) &&
                (circle.Center.X > lineX[0]) && (circle.Center.Y > lineY[1]))
            {//cell 1
                return 1;
            }
            else if ((circle.Center.X < lineX[1]) && (circle.Center.Y < lineY[1]) &&
                (circle.Center.X > lineX[0]) && (circle.Center.Y > lineY[0]))
            {//cell 2
                return 2;
            }
            else if ((circle.Center.X < lineX[2]) && (circle.Center.Y < lineY[3]) &&
                (circle.Center.X > lineX[1]) && (circle.Center.Y > lineY[2]))
            {//cell 3
                return 3;
            }
            else if ((circle.Center.X < lineX[2]) && (circle.Center.Y < lineY[2]) &&
                (circle.Center.X > lineX[1]) && (circle.Center.Y > lineY[1]))
            {//cell 4
                return 4;
            }
            else if ((circle.Center.X < lineX[2]) && (circle.Center.Y < lineY[1]) &&
                (circle.Center.X > lineX[1]) && (circle.Center.Y > lineY[0]))
            {//cell 5
                return 5;
            }
            else if ((circle.Center.X < lineX[3]) && (circle.Center.Y < lineY[3]) &&
                (circle.Center.X > lineX[2]) && (circle.Center.Y > lineY[2]))
            {//cell 6
                return 6;
            }
            else if ((circle.Center.X < lineX[3]) && (circle.Center.Y < lineY[2]) &&
                (circle.Center.X > lineX[2]) && (circle.Center.Y > lineY[1]))
            {//cell 7
                return 7;
            }
            else if ((circle.Center.X < lineX[3]) && (circle.Center.Y < lineY[1]) &&
                (circle.Center.X > lineX[2]) && (circle.Center.Y > lineY[0]))
            {//cell 8
                return 8;
            }
            else
            {
                return 9;
            }
        }

        /// <summary>
        /// allows us the get the location of the start and end of white in the picture
        /// </summary>
        /// <param name="src">
        /// source image we going to look at fro the line
        /// </param>
        /// <returns>
        /// return the points of the two points of the line
        /// </returns>
        public Point[] scanImgForLine(Image<Gray, Byte> src)
        {
            Gray gray;

            Point[] line = new Point[2];
            line[0].X = 0;
            line[0].Y = 0;

            line[1].X = src.Cols;
            line[1].Y = src.Rows;

            for (int row = 0; row < src.Rows; row++)
            {
                for (int col = 0; col < src.Cols; col++)
                {
                    gray = src[row, col];

                    if ((line[0].Y < row) && (gray.Intensity == 255))
                    {//get hghest point of the line, double check if this right way around
                        line[0].Y = row;
                        line[0].X = col;
                    }
                    if ((line[1].Y > row) && (gray.Intensity == 255))
                    {//get lowest point of the line
                        line[1].Y = row;
                        line[1].X = col;
                    }
                }
            }
            return line;
        }

        /// <summary>
        /// work out the angle of the line
        /// </summary>
        /// <param name="line">
        /// the two point on the line
        /// </param>
        /// <returns>
        /// angle is returned
        /// </returns>
        public double AngleOfTray(Point[] line)
        {
            double m = (line[0].Y - line[1].Y) / (line[0].X - line[1].X);//work out gradient

            double angle = Math.Atan((m - 0) / (1 + (0 * m))) * 180 / Math.PI;//get angle

            return angle;
        }
    }
}