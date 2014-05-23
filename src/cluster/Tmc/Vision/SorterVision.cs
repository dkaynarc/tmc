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

            float[] HueHist;
            float[] SatHist;
            float[] ValHist;

            HueHist = new float[256];
            SatHist = new float[256];
            ValHist = new float[256];

            DenseHistogram HistoHue = new DenseHistogram(256, new RangeF(0, 256));
            DenseHistogram HistoSat = new DenseHistogram(256, new RangeF(0, 256));
            DenseHistogram HistoVal = new DenseHistogram(256, new RangeF(0, 256));

            Rectangle rect = new Rectangle();
            Image<Bgr, byte> oneTablet;
            double dotAngle = 0.607;//result of of cos(ang) which use to multiply radius to give us the dot product
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
                //cellInTray      = FindCellInTrayForTablet(imgTray.Cols, imgTray.Rows, tablet);
                //trayList.Cells[cellInTray] = new Tablet { Color = tabletColour };
                //HistogramViewer.Show(oneTablet);
                Image<Hsv, Byte> hsvColor = oneTablet.Convert<Hsv, Byte>();
                Image<Gray, Byte> Comparedimg2Blue = hsvColor[0];
                Image<Gray, Byte> Comparedimg2Green = hsvColor[1];
                Image<Gray, Byte> Comparedimg2Red = hsvColor[2];

                float[][] abc   = HsvValueFloatArray(oneTablet);
                int[][] hue     = getHighLowHSV(abc, 30, HSVdata.Hue);
                int[][] sat     = getHighLowHSV(abc, 30, HSVdata.Sat);
                int[][] val     = getHighLowHSV(abc, 30, HSVdata.Val);


                HistoHue.Calculate(new Image<Gray, Byte>[] { Comparedimg2Blue }, true, null);
                HistoSat.Calculate(new Image<Gray, Byte>[] { Comparedimg2Green }, true, null);
                 HistoVal.Calculate(new Image<Gray, Byte>[] { Comparedimg2Red }, true, null);

                //HistoVal.Calculate(

                HistoHue.MatND.ManagedArray.CopyTo(HueHist, 0);
                HistoSat.MatND.ManagedArray.CopyTo(SatHist, 0);
                HistoVal.MatND.ManagedArray.CopyTo(ValHist, 0);
                int lowh, lows, lowv;
                int highh, highs, highv;
                for(int i=0; i < 256;i++)
                {
                    //lowh 
                }


                

                HistogramViewer.Show(oneTablet.Convert<Hsv, Byte>());
                CvInvoke.cvWaitKey(0);
            }
            HistogramViewer.Show(src);

            CvInvoke.cvWaitKey(0); 
        }

        

    }
}
