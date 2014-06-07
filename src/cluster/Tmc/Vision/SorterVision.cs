using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using Tmc.Common;
using System.Diagnostics;

namespace Tmc.Vision
{
    [Serializable]
    public class SorterVisionCalibrationData : ICalibrationData
    {
        public PointF[] ChessboardPoints { get; set; }

        public Type ParentType { get; set; }
    }

    public class SorterVision : VisionBase, ICalibrateable
    {
        private int minRadius;
        private int maxRadius;
        private int cannyThresh;
        private int cannyAccumThresh;
        private double dp;
        private double minDist;//expand dp

        private Hsv[,] HSVTabletcolorsRanges = new Hsv[5, 2];

        private PointF[] ChessboardPoints = new PointF[107];
        private Camera camera;

        private Image<Bgr, Byte> img;

        private List<Tablet> TabletList = new List<Tablet>();

        //private Tray<Tablet> trayList = new Tray<Tablet>();
        private TraceListener listener = new DelimitedListTraceListener(@"debugfileSort.txt");

        /// <summary>
        /// constructor for sorter vision, do all initilasation here
        /// </summary>
        /// <param name="camera">
        /// camera object we use to get images off
        /// </param>
        public SorterVision(Camera camera)
        {
            this.camera = camera;
            Debug.Listeners.Add(listener);
            //do calibration

            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.Low].Hue = 46;//46;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.Low].Satuation = 55;//55;//75;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.Low].Value = 49;//50;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.High].Hue = 78;// 68;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.High].Satuation = 180;//170;//140;
            HSVTabletcolorsRanges[(int)TabletColors.Green, (int)HSVRange.High].Value = 125;//125;

            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.Low].Hue = 1;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.Low].Satuation = 100;//153;//93;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.Low].Value = 117;//198;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.High].Hue = 8;//171;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.High].Satuation = 219;//128;
            HSVTabletcolorsRanges[(int)TabletColors.Red, (int)HSVRange.High].Value = 196;//250;

            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.Low].Hue = 12;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.Low].Satuation = 52;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.Low].Value = 183;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.High].Hue = 18;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.High].Satuation = 110;
            HSVTabletcolorsRanges[(int)TabletColors.White, (int)HSVRange.High].Value = 239;

            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Hue = 107;//114;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Satuation = 32;//36;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Value = 39;//49;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.High].Hue = 137;//147;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.High].Satuation = 176;//127;
            HSVTabletcolorsRanges[(int)TabletColors.Blue, (int)HSVRange.High].Value = 103;//109;

            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.Low].Hue = 177;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.Low].Satuation = 51;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.Low].Value = 19;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.High].Hue = 16;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.High].Satuation = 135;
            HSVTabletcolorsRanges[(int)TabletColors.Black, (int)HSVRange.High].Value = 75;

            minRadius = 60;
            maxRadius = 63;

            dp = 2.3;
            minDist = 20;
            cannyThresh = 2;
            cannyAccumThresh = 83;

            this.Register();
        }

        ~SorterVision()
        {
            this.Unregister();
        }

        /// <summary>
        /// Get visable tablets in sorter tray both possition and stateaa
        /// </summary>
        /// <returns>return position of viable tablets and state</returns>
        public List<Tablet> GetVisibleTablets()
        {
            
            Debug.WriteLine("\n\n");
            TabletList.Clear();//clear tablets from last use
            img = camera.GetImage(1);
            saveImage(img, "sorter pic.jpg");
            //img = new Image<Bgr, byte>("C:/Users/Denis/Dropbox/ICT DESIGN/Assignment 3/vision/cal/sort32.jpg");

            CircleF[] circles = DetectTablets(img, minRadius, maxRadius, dp, minDist, cannyThresh, cannyAccumThresh);

            DetectGoodPickupTablets(img, circles);

            return TabletList;
        }

        /// <summary>
        /// This function curenlty on calbrate on the chessboard
        /// </summary>
        public ICalibrationData Calibrate()
        {
            Image<Bgr, Byte> chessB = camera.GetImage(1);
            ChessboardPoints = FindPattern(chessB.Convert<Gray, Byte>(), new Size(12, 9));

            var calData = new SorterVisionCalibrationData
            {
                ParentType = this.GetType(),
                ChessboardPoints = this.ChessboardPoints
            };

            return calData;
        }

        public void SetCalibrationData(ICalibrationData data)
        {
            var myCalData = data as SorterVisionCalibrationData;
            if (myCalData != null)
            {
                ChessboardPoints = myCalData.ChessboardPoints;
            }
        }

        /// <summary>
        /// finds the chessboard in the image
        /// </summary>
        /// <param name="src">
        /// source image that will contain the chessboard
        /// </param>
        /// <param name="dim">
        /// the dimesions of the chessboard
        /// </param>
        /// <returns>
        /// returns the location of al the chessboard point
        /// </returns>
        ///
        private PointF[] FindPattern(Emgu.CV.Image<Gray, byte> src, Size dim)
        {
            //string win1 = "Test Window"; //The name of the window
            //CvInvoke.cvNamedWindow(win1); //Create the window using the specific name
            Image<Bgr, Byte> colr = src.Convert<Bgr, Byte>();
            PointF[] checkerBoardPoints = Emgu.CV.CameraCalibration.FindChessboardCorners(src, dim, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH | Emgu.CV.CvEnum.CALIB_CB_TYPE.FILTER_QUADS);//CALIB_CB_TYPE.DEFAULT);

            //CameraCalibration.CalibrateCamera(

            int a = 0;
            foreach (PointF checkerBoardPoint in checkerBoardPoints)
            {//draw dots just for debuging atm
                Rectangle rect = new Rectangle();
                rect.X = (int)checkerBoardPoint.X;
                rect.Y = (int)checkerBoardPoint.Y;
                rect.Width = 2;
                rect.Height = 2;
                if (a == 0)
                {
                    colr.Draw(rect, new Bgr(Color.Blue), 1);
                    a++;
                }
                else
                {
                    colr.Draw(rect, new Bgr(Color.Red), 1);
                    a++;
                }
            }
            //CvInvoke.cvShowImage(win1, colr); //Show the image

            //CvInvoke.cvWaitKey(0);
            return checkerBoardPoints;
        }

        /// <summary>
        /// gets coordinates in mm of where the target is on the chess board
        /// </summary>
        /// <param name="chessboard">
        /// contains the points from the chessboard
        /// </param>
        /// <param name="targetPoint">
        /// the point we want the find the mm of
        /// </param>
        /// <returns>
        /// gives back mm cordinates
        /// </returns>
        /// <todo>
        /// make it so that board rotation dosent effect our mm return value
        /// </todo>
        /// <bug>
        /// if circle outside the chessboard then loc will be out of range so need to fix this
        /// </bug>
        private PointF CalculateTrueCordXYmm(PointF[] chessboard, PointF targetPoint)
        {
            Point ClosestPoint = getClosestPointToTargetOnChessboard(chessboard, targetPoint, new Size(12, 9));//get closest point to

            int loc = ClosestPoint.X + ClosestPoint.Y;
            PointF locationXYmm = new PointF();
            double pixcelTommY;
            double pixcelTommX;

            double MagY;
            double MagX;
            
            if (loc < 96)
            {
                MagY = Math.Sqrt(Math.Pow((chessboard[loc + 12].X - chessboard[loc].X), 2) + Math.Pow((chessboard[loc + 12].Y - chessboard[loc].Y), 2));
            }
            else
            {
                MagY = Math.Sqrt(Math.Pow((chessboard[loc - 12].X - chessboard[loc].X), 2) + Math.Pow((chessboard[loc - 12].Y - chessboard[loc].Y), 2));
            }
            if (((loc + 1) % 12) == 0)
            {
                MagX = Math.Sqrt(Math.Pow((chessboard[loc - 1].X - chessboard[loc].X), 2) + Math.Pow((chessboard[loc-1].Y - chessboard[loc].Y), 2));
            }
            else
            {
                MagX = Math.Sqrt(Math.Pow((chessboard[loc].X - chessboard[loc + 1].X), 2) + Math.Pow((chessboard[loc].Y - chessboard[loc + 1].Y), 2));
            }

            pixcelTommY = 20 / MagY;//work out how much a pixcel is in mm
            pixcelTommX = 20 / MagX;


            locationXYmm.X = (float)(pixcelTommX * ((targetPoint.X - chessboard[loc].X)) + ClosestPoint.X * 20);      //work out location from origon
            locationXYmm.Y = (float)(pixcelTommY * ((targetPoint.Y - chessboard[loc].Y)) + ((ClosestPoint.Y / 12) * 20));

            return locationXYmm;
        }

        /// <summary>
        /// Work out the closest chessboard intersection to the target position
        /// </summary>
        /// <param name="chessboard">
        /// the chessboard points
        /// </param>
        /// <param name="target">
        /// target location
        /// </param>
        /// <param name="board">
        /// the dimension of the chess board
        /// </param>
        /// <returns>
        /// return the poisition of the closest points
        /// </returns>
        private Point getClosestPointToTargetOnChessboard(PointF[] chessboard, PointF target, Size board)
        {
            Point closestPoint = new Point(0, 0);

            for (int i = 0; i < board.Width; i++)
            {
                if (target.X > chessboard[i].X)
                {
                    closestPoint.X = i;
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < board.Width * board.Height; i += board.Width)
            {
                if (target.Y > chessboard[i].Y)
                {
                    closestPoint.Y = i;
                }
                else
                {
                    break;
                }
            }

            return closestPoint;
        }

        /*private List<Tablet> FillListOfGoodTablets(PointF[] chessboard, CircleF[] tablets)
        {
            return TabletList;
        }*/

        /*/// <summary>
        /// Determins if tablet is damaged
        /// </summary>
        private void DetectDamagedTablet()
        {
        }*/

        /// <summary>
        ///
        /// </summary>
        /// <param name="src">
        /// Image where the image where tablets are
        /// </param>
        /// <param name="tablets">
        /// location of tabletsin the src image
        /// </param>
        private void DetectGoodPickupTablets(Image<Bgr, Byte> src, CircleF[] tablets)
        {
            //Tablet tab = new Tablet();
            //List<Tablet> TabletLists = new List<Tablet>();

            List<CircleF> TabletsInList = new List<CircleF>();

            Image<Bgr, Byte> drawTab = src.Clone();
            var tabletHSV = new List<Tuple<int[][], int[][], int[][]>>(tablets.Length);

            for (int i = 0; i < tablets.Length; i++)
            {
                var tablet = tablets[i];
                Tablet tab = new Tablet();

                var histo = ImagesToHisto(GetTablet(src, tablet));

                int[][] hue = getHighLowHSV(histo, 30, HSVdata.Hue);
                int[][] sat = getHighLowHSV(histo, 30, HSVdata.Sat);
                int[][] val = getHighLowHSV(histo, 30, HSVdata.Val);

#if DEBUG
                //Debug.WriteLine("Hue: " + hue[0][0] + " - " + hue[0][1] + ", Sat: " + sat[0][0] + " - " + sat[0][1] + ", Val: " + val[0][0] + " - " + val[0][1]);
                Debug.WriteLine("" + hue[0][0] + "\t" + hue[0][1] + "\t" + sat[0][0] + "\t" + sat[0][1] + "\t" + val[0][0] + "\t" + val[0][1]);
                int hueM = hue.GetLength(0) - 1;
                int satM = sat.GetLength(0) - 1;
                int valM = val.GetLength(0) - 1;
                //Debug.WriteLine("Hue: " + hue[hueM][0] + " - " + hue[hueM][1] + ", Sat: " + sat[satM][0] + " - " + sat[satM][1] + ", Val: " + val[valM][0] + " - " + val[valM][1]);
                Debug.WriteLine("" + hue[hueM][0] + "\t" + hue[hueM][1] + "\t" + sat[satM][0] + "\t" + sat[satM][1] + "\t" + val[valM][0] + "\t" + val[valM][1]);
                Debug.Flush();
#endif

                tabletHSV.Add(new Tuple<int[][], int[][], int[][]>(hue, sat, val));

                if (FirstPass(hue, sat, val, tablet, tablets, HSVTabletcolorsRanges) == true)
                {
                    drawTab.Draw(tablet, new Bgr(Color.Green), 6);
                    TabletsInList.Add(tablet);
                }
                else
                {
                    drawTab.Draw(tablet, new Bgr(Color.White), 2);
                }
            }

            for (int i = 0; i < tablets.Length; i++)
            {
                var tablet = tablets[i];
                int[][] hue = tabletHSV[i].Item1;
                int[][] sat = tabletHSV[i].Item2;
                int[][] val = tabletHSV[i].Item3;

                int a = 0;
                if (IsVisableTablet(src, tablet, 5) == true)
                {
                    foreach (CircleF tabl in TabletsInList)
                    {
                        if (checkCircles(tabl, tablet))
                        {
                            a++;
                        }
                    }
                    if (a > 0)
                    {
                        continue;
                    }
                    if (FirstPass(hue, sat, val, tablet, tablets, HSVTabletcolorsRanges) == true)
                    {
                        drawTab.Draw(tablet, new Bgr(Color.Blue), 6);
                        TabletsInList.Add(tablet);
                    }
                    else
                    {
                        drawTab.Draw(tablet, new Bgr(Color.Red), 4);
                        TabletsInList.Add(tablet);
                    }
                }
                //CvInvoke.cvWaitKey(0);
            }

            foreach (CircleF tablet in TabletsInList)
            {
                Tablet tab = new Tablet();
                if ((IsVisableTablet(src, tablet, 5) == true) || (OtherTabletsNear(TabletsInList.ToArray(), tablet) == true))
                {
                    float[][] abca = ImagesToHisto(GetTablet(src, tablet));
                    //b++;
                    int[][] hue = getHighLowHSV(abca, 50, HSVdata.Hue);
                    int[][] sat = getHighLowHSV(abca, 50, HSVdata.Sat);
                    int[][] val = getHighLowHSV(abca, 50, HSVdata.Val);

                    if (FirstPass(hue, sat, val, tablet, tablets, HSVTabletcolorsRanges) == true)
                    {
                        tab.LocationPoint = CalculateTrueCordXYmm(ChessboardPoints, new PointF(tablet.Center.X, tablet.Center.Y));
                        tab.Color = detectcolor(new Hsv((hue[0][0] + hue[0][1]) / 2, (sat[0][0] + sat[0][1]) / 2, (val[0][0] + val[0][1]) / 2), HSVTabletcolorsRanges);
                        if ((IsVisableTablet(src, tablet, 8) == true))
                        {//if it's right color and we can detect it's on top pop to start of the list
                            TabletList.Insert(0, tab);
                        }
                        else
                        {//otherwise add to the end
                            TabletList.Add(tab);
                        }
                        drawTab.Draw(tablet, new Bgr(Color.Red), 2);
                    }
                    else
                    {//unknown tablet
                        tab.LocationPoint = CalculateTrueCordXYmm(ChessboardPoints, new PointF(tablet.Center.X, tablet.Center.Y));
                        tab.Color = TabletColors.Unknown;
                        TabletList.Add(tab);
                        drawTab.Draw(tablet, new Bgr(Color.Red), 5);
                        drawTab.Draw(tablet, new Bgr(Color.Blue), 2);
                    }
                }
            }

            saveImage(drawTab, "sorter tablets.jpg");
        }

        /// <summary>
        /// lets as darken image apart from one corner
        /// </summary>
        /// <param name="src">
        /// image containing circle
        /// </param>
        /// <param name="part">
        /// which part we want left alone
        /// </param>
        /// <returns>
        /// darkened image
        /// </returns>
        private Image<Bgr, Byte> darkenCircle(Image<Bgr, Byte> src, int part)
        {
            Image<Bgr, Byte> darken = src.Clone();
            int halfx = src.Cols / 2;
            int halfy = src.Rows / 2;
            //if ((part == 3) || (part == 4))
            if ((part == 3) || (part == 4) || (part == 2))
            {
                for (int i = 0; i < halfx; i++)
                {//tl
                    for (int j = 0; j < halfy; j++)
                    {
                        darken[j, i] = new Bgr(0, 0, 0);
                    }
                }
            }
            //if ((part == 3) || (part == 2))
            if ((part == 4) || (part == 1) || (part == 2))
            {
                for (int i = 0; i < halfx; i++)
                {//bl
                    for (int j = halfy; j < halfy * 2; j++)
                    {
                        darken[j, i] = new Bgr(0, 0, 0);
                    }
                }
            }
            //if ((part == 4) || (part == 1))
            if ((part == 3) || (part == 4) || (part == 1))
            {
                for (int i = halfx; i < halfx * 2; i++)
                {//tr
                    for (int j = 0; j < halfy; j++)
                    {
                        darken[j, i] = new Bgr(0, 0, 0);
                    }
                }
            }
            //if ((part == 2)|| (part == 1))
            if ((part == 3) || (part == 1) || (part == 2))
            {
                for (int i = halfx; i < halfx * 2; i++)
                {//br
                    for (int j = halfy; j < halfy * 2; j++)
                    {
                        darken[j, i] = new Bgr(0, 0, 0);
                    }
                }
            }

            return darken;
            //saveImage(darken, "derp.jpg");
        }

        /// <summary>
        /// Works out if the tablet is on top by checking if we can recognise the tablet we look at the 4 corners
        /// </summary>
        /// <param name="src">
        /// source image which contains the tablets
        /// </param>
        /// <param name="tablet">
        /// the tablet we want check
        /// </param>
        /// <param name="padding">
        /// Our tolirance to tablet location compared to original(tablet)
        /// </param>
        /// <returns>
        /// returns true if the circle is thought to be seen from all four sides
        /// </returns>
        private bool IsVisableTablet(Image<Bgr, Byte> src, CircleF tablet, int padding)
        {
            int expand = 4;
            int countTL = 0;
            int countTR = 0;
            int countBL = 0;
            int countBR = 0;

            Image<Bgr, Byte> tabletImage = CropImage(src, ((int)tablet.Center.X - (int)tablet.Radius) - expand, ((int)tablet.Center.Y - (int)tablet.Radius) - expand, ((int)tablet.Radius * 2) + expand * 2, ((int)tablet.Radius * 2) + expand * 2);
            Image<Bgr, byte> TL = darkenCircle(tabletImage, 1);//CropImage(src, ((int)tablet.Center.X - (int)tablet.Radius) - expand, ((int)tablet.Center.Y - (int)tablet.Radius) - expand, ((int)tablet.Radius) + expand, ((int)tablet.Radius) + expand);
            Image<Bgr, byte> TR = darkenCircle(tabletImage, 2);//CropImage(src, ((int)tablet.Center.X), ((int)tablet.Center.Y - (int)tablet.Radius) - expand, ((int)tablet.Radius) + expand, ((int)tablet.Radius) + expand);
            Image<Bgr, byte> BL = darkenCircle(tabletImage, 3);//CropImage(src, ((int)tablet.Center.X - (int)tablet.Radius) - expand, ((int)tablet.Center.Y), ((int)tablet.Radius) + expand, ((int)tablet.Radius * 2) + expand * 2);
            Image<Bgr, byte> BR = darkenCircle(tabletImage, 4);//CropImage(src, ((int)tablet.Center.X ) , ((int)tablet.Center.Y ) , ((int)tablet.Radius) + expand , ((int)tablet.Radius) + expand );
            //minDist, cannyThresh
            CircleF[] CTL = DetectTablets(TL, minRadius - 2, maxRadius + 2, dp, 2.6, 2, 40);
            CircleF[] CTR = DetectTablets(TR, minRadius - 2, maxRadius + 2, dp, 2.6, 2, 40);
            CircleF[] CBL = DetectTablets(BL, minRadius - 2, maxRadius + 2, dp, 2.6, 2, 40);
            CircleF[] CBR = DetectTablets(BR, minRadius - 2, maxRadius + 2, dp, 2.6, 2, 40);

            //darkenCircle(tabletImage, 3);

            //CvInvoke.cvShowImage("TL", TL);
            //CvInvoke.cvShowImage("TR", TR);
            //CvInvoke.cvShowImage("BL", BL);
            //CvInvoke.cvShowImage("BR", BR);

            int cX = (int)(tablet.Radius + expand);
            int cY = (int)(tablet.Radius + expand);

            foreach (CircleF qwe in CTR)
            {
                if ((qwe.Center.X > (cX - padding)) && (qwe.Center.X < (cX + padding))
                    && (qwe.Center.Y > (cY - padding)) && (qwe.Center.Y < (cY + padding)))
                    countTR++;
                tabletImage.Draw(qwe, new Bgr(Color.Red), 1);
            }
            foreach (CircleF qwe in CTL)
            {
                if ((qwe.Center.X > (cX - padding)) && (qwe.Center.X < (cX + padding))
                    && (qwe.Center.Y > (cY - padding)) && (qwe.Center.Y < (cY + padding)))
                    countTL++;
                tabletImage.Draw(qwe, new Bgr(Color.Blue), 1);
            }
            foreach (CircleF qwe in CBL)
            {
                if ((qwe.Center.X > (cX - padding)) && (qwe.Center.X < (cX + padding))
                    && (qwe.Center.Y > (cY - padding)) && (qwe.Center.Y < (cY + padding)))
                    countBL++;
                tabletImage.Draw(qwe, new Bgr(Color.Green), 1);
            }
            foreach (CircleF qwe in CBR)
            {
                if ((qwe.Center.X > (cX - padding)) && (qwe.Center.X < (cX + padding))
                    && (qwe.Center.Y > (cY - padding)) && (qwe.Center.Y < (cY + padding)))
                    countBR++;
                tabletImage.Draw(qwe, new Bgr(Color.Purple), 1);
            }
            //CvInvoke.cvShowImage("TLL", tabletImage);
            //CvInvoke.cvWaitKey(0);
            if ((countTL >= 1) && (countTR >= 1) && (countBL >= 1) && (countBR >= 1)) saveImage(tabletImage, tablet.Center.X + "good.jpg");
            return ((countTL >= 1) && (countTR >= 1) && (countBL >= 1) && (countBR >= 1));
        }

        public void Register()
        {
            CalibrationManager.Instance.Register(this);
        }

        public void Unregister()
        {
            CalibrationManager.Instance.Unregister(this);
        }
    }
}