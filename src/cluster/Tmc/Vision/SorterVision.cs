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
using Emgu.CV.Features2D;
using Emgu.CV.UI;
//using Emgu.CV.CameraCalibration;

namespace Tmc.Vision
{

    //[Calib3D.SupportedPattern(typeof(CheckerBoardPattern))]

    public class SorterVision : VisionBase
    {
        private int minRadius, maxRadius, cannyThresh, cannyAccumThresh;
        private double dp, minDist;
        private Hsv[,] HSVTabletColoursRanges = new Hsv[5, 2];
        
        PointF[] ChessboardPoints = new PointF[107];
        private Camera camera;
        private Image<Bgr, Byte> img;
        Form1 f;
        List<Tablet> TabletList = new List<Tablet>();

        /// <summary>
        /// constructor for sorter vision, do all initilasation here
        /// </summary>
        /// <param name="camera">
        /// camera object we use to get images off
        /// </param>
        public SorterVision(Camera camera)
        {
            f = new Form1();
            f.Show();
            this.camera = camera;
            //do calibration
            //this.camera.ConnectionString = new Uri(@"http://192.168.0.190:8080/photoaf.jpg");
            //this.camera.ConnectionString = new Uri(@"https://fbcdn-sphotos-b-a.akamaihd.net/hphotos-ak-frc3/t1.0-9/10270404_10202712962477674_682458036245271256_n.jpg");

            HSVTabletColoursRanges[(int)TabletColors.Green, (int)HSVRange.Low].Hue = 76;
            HSVTabletColoursRanges[(int)TabletColors.Green, (int)HSVRange.Low].Satuation = 24;
            HSVTabletColoursRanges[(int)TabletColors.Green, (int)HSVRange.Low].Value = 139;
            HSVTabletColoursRanges[(int)TabletColors.Green, (int)HSVRange.High].Hue = 87;
            HSVTabletColoursRanges[(int)TabletColors.Green, (int)HSVRange.High].Satuation = 96;
            HSVTabletColoursRanges[(int)TabletColors.Green, (int)HSVRange.High].Value = 226;

            HSVTabletColoursRanges[(int)TabletColors.Red, (int)HSVRange.Low].Hue = 176;
            HSVTabletColoursRanges[(int)TabletColors.Red, (int)HSVRange.Low].Satuation = 119;//93;
            HSVTabletColoursRanges[(int)TabletColors.Red, (int)HSVRange.Low].Value = 200;//198;
            HSVTabletColoursRanges[(int)TabletColors.Red, (int)HSVRange.High].Hue = 5;//171;
            HSVTabletColoursRanges[(int)TabletColors.Red, (int)HSVRange.High].Satuation = 194;//128;
            HSVTabletColoursRanges[(int)TabletColors.Red, (int)HSVRange.High].Value = 360;//250;

            HSVTabletColoursRanges[(int)TabletColors.White, (int)HSVRange.Low].Hue = 12;
            HSVTabletColoursRanges[(int)TabletColors.White, (int)HSVRange.Low].Satuation = 45;
            HSVTabletColoursRanges[(int)TabletColors.White, (int)HSVRange.Low].Value = 238;
            HSVTabletColoursRanges[(int)TabletColors.White, (int)HSVRange.High].Hue = 17;
            HSVTabletColoursRanges[(int)TabletColors.White, (int)HSVRange.High].Satuation = 62;
            HSVTabletColoursRanges[(int)TabletColors.White, (int)HSVRange.High].Value = 255;

            HSVTabletColoursRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Hue = 115;
            HSVTabletColoursRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Satuation = 76;
            HSVTabletColoursRanges[(int)TabletColors.Blue, (int)HSVRange.Low].Value = 69;
            HSVTabletColoursRanges[(int)TabletColors.Blue, (int)HSVRange.High].Hue = 126;
            HSVTabletColoursRanges[(int)TabletColors.Blue, (int)HSVRange.High].Satuation = 125;
            HSVTabletColoursRanges[(int)TabletColors.Blue, (int)HSVRange.High].Value = 213;

            HSVTabletColoursRanges[(int)TabletColors.Black, (int)HSVRange.Low].Hue = 102;
            HSVTabletColoursRanges[(int)TabletColors.Black, (int)HSVRange.Low].Satuation = 15;
            HSVTabletColoursRanges[(int)TabletColors.Black, (int)HSVRange.Low].Value = 90;
            HSVTabletColoursRanges[(int)TabletColors.Black, (int)HSVRange.High].Hue = 145;
            HSVTabletColoursRanges[(int)TabletColors.Black, (int)HSVRange.High].Satuation = 39;
            HSVTabletColoursRanges[(int)TabletColors.Black, (int)HSVRange.High].Value = 167;
        }

        /// <summary>
        /// Get visable tablets in sorter tray both possition and state
        /// </summary>
        /// <returns>return position of viable tablets and state</returns>
        public List<Tablet> GetVisibleTablets()
        {
            List<Tablet> tablet = new List<Tablet>();           
            //img = camera.GetImage(1);
            img = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/cal/sort21.jpg");

            f.getValue(ref minRadius, ref maxRadius, ref dp, ref minDist, ref cannyThresh, ref cannyAccumThresh);

            CircleF[] circles = DetectTablets(img, minRadius, maxRadius, dp, minDist, cannyThresh, cannyAccumThresh, f);
            //PointF[] points = FindPattern(img.Convert<Gray, Byte>(), new Size(12, 9));
            //CircleF
            //CvInvoke.cvWaitKey(0);
            
            foreach (CircleF circle in circles)
            {
                CalculateTrueCordXYmm(ChessboardPoints, new PointF(circle.Center.X, circle.Center.Y));//424, 466));//454, 503));//423,464));//594, 750));//253, 525));
                CircleF[] abc = OtherTabletsNear(circles, circle);
                HistogramImage(img, circles);
            }
            DetectOverLap();
            DetectDamagedTablet();
            return FillListOfGoodTablets(ChessboardPoints, circles);
            //GetXYZForTablets();
            CvInvoke.cvWaitKey(0); 

            return tablet;
        }

        public void Calibration()
        {
            Image<Bgr, Byte> img2 = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/cal/chess2.jpg");
            ChessboardPoints = FindPattern(img2.Convert<Gray, Byte>(), new Size(12, 9));
            //ChessboardPoints = FindPattern(camera.GetImage(1).Convert<Gray, Byte>(), new Size(12, 9));



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
        private PointF[] FindPattern(Emgu.CV.Image<Gray, byte> src, Size dim)
        {
            string win1 = "Test Window"; //The name of the window
            CvInvoke.cvNamedWindow(win1); //Create the window using the specific name
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
             CvInvoke.cvShowImage(win1, colr); //Show the image
              
             CvInvoke.cvWaitKey(0);
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
            
            Point ClosestPoint = getClosestPointToTargetOnChessboard(chessboard, targetPoint, new Size(12,9));//get closest point to 
            
            int loc = ClosestPoint.X + ClosestPoint.Y;
            PointF locationXYmm = new PointF();
            double pixcelTommY;
            double pixcelTommX;

            double MagY;
            double MagX;
            if (ClosestPoint.Y > 0)
            {
                MagY = Math.Sqrt(Math.Pow((chessboard[loc - 1].X - chessboard[loc].X), 2) + Math.Pow((chessboard[loc - 1].Y - chessboard[loc].Y), 2));
            }
            else
            {
                MagY = Math.Sqrt(Math.Pow((chessboard[0].X - chessboard[12].X), 2) + Math.Pow((chessboard[0].Y - chessboard[12].Y), 2));
            }
            if (ClosestPoint.X > 11)
            {//out side bounds
                MagX = Math.Sqrt(Math.Pow((chessboard[loc - 12].X - chessboard[loc].X), 2) + Math.Pow((chessboard[loc - 12].Y - chessboard[loc].Y), 2));
            }
            else 
            {
                MagX = Math.Sqrt(Math.Pow((chessboard[0].X - chessboard[1].X), 2) + Math.Pow((chessboard[0].Y - chessboard[1].Y), 2));
            }


            pixcelTommY = 20 / MagY;//work out how much a pixcel is in mm
            pixcelTommX = 20 / MagX;

            locationXYmm.X = (float)(pixcelTommX * ((targetPoint.X - chessboard[loc].X)) + ClosestPoint.X*20);      //work out location from origon
            locationXYmm.Y = (float)(pixcelTommY * ((targetPoint.Y - chessboard[loc].Y)) + (ClosestPoint.Y/12)*20);

            return targetPoint;
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

            for (int i = 0; i < board.Width*board.Height; i += board.Width)
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

        private List<Tablet> FillListOfGoodTablets(PointF[] chessboard, CircleF[] tablets)
        {
            return TabletList;
        }

        private CircleF[] OtherTabletsNear(CircleF[] knowTablets, CircleF targetTablet)
        {
            var circleList = new List<CircleF>();

            foreach(CircleF knowTablet in knowTablets)
            {
                double Mag = Math.Sqrt(Math.Pow((targetTablet.Center.X - knowTablet.Center.X), 2) + Math.Pow((targetTablet.Center.Y - knowTablet.Center.Y), 2));
                int a = 0;
                bool b = checkCircles(knowTablet, targetTablet);
                if ((Mag <= targetTablet.Radius) && (checkCircles(knowTablet,targetTablet) == false))
                {//center is in the radius of the circle
                    a++;
                    circleList.Add(knowTablet);
                    //int a = 0;
                    //targetTablet
                }
                if ((Mag < (targetTablet.Radius + knowTablet.Radius)) && (checkCircles(knowTablet, targetTablet) == false))
                {//if the circle crosses over
                    
                    if (a < 1)
                    {
                        circleList.Add(knowTablet);
                    }
                    
                    a++;
                    //int a = 0;
                }
            }

            return circleList.ToArray();

            //return knowTablets;
        }

        private bool checkCircles(CircleF circ, CircleF targ)
        {
            return ((circ.Radius == targ.Radius) && (circ.Center.X == targ.Center.X) &&
                    (circ.Center.Y == targ.Center.Y) && (circ.Area == targ.Area));
        }

        /// <summary>
        /// will detect if tablet has been covered by another tablet
        /// </summary>
        private void DetectOverLap()
        {

        }


        //private void GetXYZForTablets()
        //{

        //}

        /// <summary>
        /// Determins if tablet is damaged
        /// </summary>
        private void DetectDamagedTablet()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="tablets"></param>
        private void HistogramImage(Image<Bgr, Byte> src, CircleF[] tablets)
        {
            Image<Hsv, Byte> srcHSV = src.Convert<Hsv, Byte>();

            Rectangle rect = new Rectangle();
            Image<Bgr, byte> oneTablet;
            double dotAngle = 0.707;//result of of cos(ang) which use to multiply radius to give us the dot product
            TabletColors tabletColour;
            int cellInTray;


            foreach (CircleF tablet in tablets)
            {

                rect.X = (int)(tablet.Center.X - (tablet.Radius * dotAngle));
                rect.Y = (int)(tablet.Center.Y - (tablet.Radius * dotAngle));
                rect.Width = (int)((tablet.Radius * dotAngle) * 2);
                rect.Height = (int)((tablet.Radius * dotAngle) * 2);

                if (rect.X < 0) rect.X = 0;
                if (rect.Y < 0) rect.Y = 0;
                if ((rect.X + rect.Width) > src.Cols)
                {
                    rect.Width -= (rect.X + rect.Width) - src.Cols;
                }
                if ((rect.Y + rect.Height) > src.Rows)
                {
                    rect.Height -= (rect.Y + rect.Height) - src.Rows;
                }

                oneTablet = src.GetSubRect(rect);
                CvInvoke.cvShowImage("Test Window2", oneTablet); //Show the image
                tabletColour = detectColour(oneTablet, HSVTabletColoursRanges);

                float[][] abca   = HsvValueFloatArray(oneTablet);
                

                 var tabletList = new List<Image<Bgr, Byte>>();
                tabletList = TabletColour(src, tablet);
                for (int i = 0; i < tabletList.Capacity - 1; i++)
                {
                    float[][] abc = HsvValueFloatArray(tabletList[i]);
                    int[][] hue = getHighLowHSV(abc, 30, HSVdata.Hue);
                    int[][] sat = getHighLowHSV(abc, 30, HSVdata.Sat);
                    int[][] val = getHighLowHSV(abc, 30, HSVdata.Val);
                    abca = addFloats(abca, abc);
                }
                //Image<Bgr, Byte> qwe[];// = new Image<Bgr, Byte>;
                //= TabletColour(src, tablet);

                int[][] huex = getHighLowHSV(abca, 50, HSVdata.Hue);
                int[][] satx = getHighLowHSV(abca, 50, HSVdata.Sat);
                int[][] valx = getHighLowHSV(abca, 50, HSVdata.Val);

                ChoseCheckType(huex, satx, valx);

                TabletColour(src, tablet);

                HistogramViewer.Show(oneTablet.Convert<Hsv, Byte>());
                HistogramViewer.Show(oneTablet.Convert<Hsv, Byte>());
                CvInvoke.cvWaitKey(0);
            }
            HistogramViewer.Show(src);

            CvInvoke.cvWaitKey(0);
        }

        private void ChoseCheckType(int[][] hue, int[][] sat, int[][] val)
        {
            if ((hue.GetLength(0) == 1) && (sat.GetLength(0) == 1) && (val.GetLength(0) == 1))
            {
                TabletColors a = detectColour(new Hsv((hue[0][0]+hue[0][1])/2,(sat[0][0]+sat[0][1])/2,(val[0][0]+val[0][1])/2),HSVTabletColoursRanges);
            }
            else
            {
 
            }
        }

        private float[][] addFloats(float[][] srcFloat, float[][] srcFloatAdd)
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

        private List<Image<Bgr, Byte>> TabletColour(Image<Bgr, Byte> src, CircleF tablet)
        {
            var TabletList = new List<Image<Bgr, Byte>>();

            double angle1 = Math.Cos(45 * (Math.PI / 180));
            double angle2 = Math.Cos(41 * (Math.PI / 180));
            double angle3 = Math.Cos(37 * (Math.PI / 180));
            double angle4 = Math.Cos(31.5 * (Math.PI / 180));
            double angle5 = Math.Cos(24 * (Math.PI / 180));
            double angle6 = Math.Cos(16 * (Math.PI / 180));

            Point[] points = new Point[11];

            float rad = tablet.Radius - 1;

            points[0].X = (int)(tablet.Center.X - (tablet.Radius * angle1));
            points[0].Y = (int)(tablet.Center.Y - (Math.Sin(45 * (Math.PI / 180)) * rad));

            points[1].X = (int)(tablet.Center.X - (tablet.Radius * angle2));
            points[1].Y = (int)(tablet.Center.Y - (Math.Sin(41 * (Math.PI / 180)) * rad));

            //Image<Bgr, Byte> LS1 = CropImage(src, points[1].X, points[1].Y, points[0].X - points[1].X, ((int)tablet.Center.Y - points[1].Y) * 2);

            

            points[2].X = (int)(tablet.Center.X - (tablet.Radius * angle3));
            points[2].Y = (int)(tablet.Center.Y - (Math.Sin(37 * (Math.PI / 180)) * rad));

            points[3].X = (int)(tablet.Center.X - (tablet.Radius * angle4));
            points[3].Y = (int)(tablet.Center.Y - (Math.Sin(31.5 * (Math.PI / 180)) * rad));

            points[4].X = (int)(tablet.Center.X - (tablet.Radius * angle5));
            points[4].Y = (int)(tablet.Center.Y - (Math.Sin(24 * (Math.PI / 180)) * rad));

            points[5].X = (int)(tablet.Center.X - (tablet.Radius * angle6));
            points[5].Y = (int)(tablet.Center.Y - (Math.Sin(16 * (Math.PI / 180)) * rad));

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
            points[10].Y = points[0].Y - ((points[0].X - points[5].X) );//points[0].Y - ((points[5].Y - points[0].Y) );

            //Image<Bgr, Byte> TS2 = CropImage(src, points[2].X, points[2].Y, ((int)tablet.Center.Y - points[2].Y) * 2, points[1].X - points[2].X);
            //Image<Bgr, Byte> TS3 = CropImage(src, points[3].X, points[3].Y, ((int)tablet.Center.Y - points[3].Y) * 2, points[2].X - points[3].X);
            //Image<Bgr, Byte> TS4 = CropImage(src, points[4].X, points[4].Y, ((int)tablet.Center.Y - points[4].Y) * 2, points[3].X - points[4].X);
            //Image<Bgr, Byte> TS5 = CropImage(src, points[5].X, points[5].Y, ((int)tablet.Center.Y - points[5].Y) * 2, points[4].X - points[5].X);



            CvInvoke.cvShowImage("first", LS1);
            CvInvoke.cvShowImage("first2", LS2);
            CvInvoke.cvShowImage("first3", LS3);
            CvInvoke.cvShowImage("first4", LS4);
            CvInvoke.cvShowImage("first5", LS5);

            CvInvoke.cvShowImage("firstr", RS1);
            CvInvoke.cvShowImage("first2r", RS2);
            CvInvoke.cvShowImage("first3r", RS3);
            CvInvoke.cvShowImage("first4r", RS4);
            CvInvoke.cvShowImage("first5r", RS5);

            CvInvoke.cvShowImage("firstrt", TS1);
            CvInvoke.cvShowImage("first2rt", TS2);
            CvInvoke.cvShowImage("first3rt", TS3);
            CvInvoke.cvShowImage("first4rt", TS4);
            CvInvoke.cvShowImage("first5rt", TS5);

            Image<Bgr, Byte> derp;

            derp = DrawPoints(src, points);

            CvInvoke.cvShowImage("Test Window3", derp);
            CvInvoke.cvWaitKey(0);

            return TabletList;
        }
        //private i

    }
}
