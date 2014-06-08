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

            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.Low].Hue = 55;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.Low].Satuation = 50;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.Low].Value = 79;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.High].Hue = 70;//87;//74;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.High].Satuation = 118;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.High].Value = 216;//229;//222;

            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.Low].Hue = 0;//176;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.Low].Satuation = 113;//93;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.Low].Value = 142;//198;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.High].Hue = 7;//171;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.High].Satuation = 185;//128;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.High].Value = 255;//250;

            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.Low].Hue = 0;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.Low].Satuation = 0;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.Low].Value = 230;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.High].Hue = 97;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.High].Satuation = 47;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.High].Value = 255;

            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Hue = 115;//112;//115;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Satuation = 40;//76;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Value = 117;//69;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.High].Hue = 131;//126;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.High].Satuation = 119;//124;//125;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.High].Value = 197;// 235;//226;// 214;//213;

            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.Low].Hue = 177;//102;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.Low].Satuation = 14;//15;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.Low].Value = 61;//90;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.High].Hue = 14;//145;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.High].Satuation = 84;//39;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.High].Value = 161;//167;

            minRadius = 54;
            maxRadius = 62;

            dp = 1.8;
            minDist = 20;
            cannyThresh = 2;
            cannyAccumThresh = 70;

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
        public bool GetTabletsInTray(out Tray<Tablet> tray)
        {

            img = camera.GetImage(1);//C:/Users/leonid/Dropbox/ICT DESIGN/Assignment 3/vision/cal
            //img = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICT DESIGN/Assignment 3/vision/cal/trayGRE.jpg");
            //img = camera.GetImageHttp(new Uri(@"http://www.wwrd.com.au/images/P/2260248_Fable%20s-4%2016cm%20Accent%20Plates-652383734586-co.jpg"));

            Image<Bgr, Byte> src = CropImage(img, 0, 877, img.Cols, 902);//reduce the image so we only see the coveour and tray

            saveImage(src, "croped Image.jpg");

            if (DetectTray(src) == false)//make a ref angle
            {
                tray = trayList;
                return false;    
            }

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
            saveImage(img, "orig image.jpg");
            saveImage(src, "Image with points.jpg");

            DetectTabletsInTray();
            DetectTabletType();

            //saveImage(apply_Hough(img), "lines in tray.jpg");
            tray = trayList;
            return true;
        }

        private Image<Bgr, Byte> apply_Hough(Image<Bgr, Byte> Input_Image, out int countLines)
        {

            LineSegment2D[] lines = Input_Image.HoughLinesBinary(
              1, //Distance resolution in pixel-related units
              Math.PI / 90.0, //Angle resolution measured in radians.
              50, //threshold
              100, //min Line width
              1 //gap between lines
              )[0]; //Get the lines from the first channel
            Image<Bgr, Byte> lineImage = img.Copy();

            for (int i = 0; i < lines.Length; i++)
            {
                Input_Image.Draw(lines[i], new Bgr(Color.Yellow), 2);
            }

            countLines = lines.Length;
            return Input_Image;
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
            HSVT[(int)HSVRange.High].Hue = 39;
            HSVT[(int)HSVRange.High].Satuation = 255;//331.92;
            HSVT[(int)HSVRange.High].Value = 255;//360;

            Image<Bgr, Byte> col = RemoveEverythingButRange(src, HSVT); //we want to remove everything that is not yellow

            int rows = col.Rows;
            int cols = col.Cols;

            col = col.Resize((int)(cols / 2.5), (int)(rows / 2.5), INTER.CV_INTER_AREA);
            col = col.Resize(cols, rows, INTER.CV_INTER_AREA);

            Image<Gray, Byte> Gsrc = col.Convert<Gray, Byte>();

            int countLines;

            col = RemoveEverythingButRange(apply_Hough(col.Clone(),out countLines), HSVT); //we want to remove everything that is not yellow

            if (countLines < 3) return false;

            Gsrc = col.Convert<Gray, Byte>();
            line = scanImgForLine(Gsrc);        //get location of the yellow line

            angle = AngleOfTray(line);          //get the angle of the tray
            angle -= 90;                        //remove 90 so the angle is starting from zero

            Angle = angle;//make ref
            Debug.WriteLine(DateTime.Now.ToString("h:mm:ss tt>>  ") + "Angle: " + Angle);
            Debug.Flush();
            //double Mag = Math.Sqrt(Math.Pow((line[0].X - line[1].X),2) + Math.Pow((line[0].Y - line[1].Y),2) );

            saveImage(col, "only yellow.jpg");

            //saveImage(apply_Hough(col.Clone()), "lines in tray.jpg");

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

                #if DEBUG
                Debug.WriteLine("cell min: " + cellInTray + "Hue: " + hue[0][0] + " - " + hue[0][1] + ", Sat: " + sat[0][0] + " - " + sat[0][1] + ", Val: " + val[0][0] + " - " + val[0][1]);
                int hueM = hue.GetLength(0) - 1;
                int satM = sat.GetLength(0) - 1;
                int valM = val.GetLength(0) - 1;
                Debug.WriteLine("cell max: " + cellInTray + "Hue: " + hue[hueM][0] + " - " + hue[hueM][1] + ", Sat: " + sat[satM][0] + " - " + sat[satM][1] + ", Val: " + val[valM][0] + " - " + val[valM][1]);
                Debug.Flush();
                #endif

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
            double den = (line[0].X - line[1].X);
            double m;
            double angle;

            if (den != 0)
            {
                m = (line[0].Y - line[1].Y) / den;//work out gradient
                angle = Math.Atan((m - 0) / (1 + (0 * m))) * 180 / Math.PI;//get angle
            }
            else
            {
                angle = 0;
            }

            //double angle = Math.Atan((m - 0) / (1 + (0 * m))) * 180 / Math.PI;//get angle

            return angle;
        }
    }
}