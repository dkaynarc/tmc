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
            this.camera.ConnectionString = new Uri(@"http://192.168.0.190:8080/photoaf.jpg");
            //this.camera.ConnectionString = new Uri(@"https://fbcdn-sphotos-b-a.akamaihd.net/hphotos-ak-frc3/t1.0-9/10270404_10202712962477674_682458036245271256_n.jpg");
        }

        /// <summary>
        /// Get visable tablets in sorter tray both possition and state
        /// </summary>
        /// <returns>return position of viable tablets and state</returns>
        public List<Tablet> GetVisibleTablets()
        {
            List<Tablet> tablet = new List<Tablet>();           
            img = camera.GetImage(1);
            //img = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/sortC2.jpg");

            f.getValue(ref minRadius, ref maxRadius, ref dp, ref minDist, ref cannyThresh, ref cannyAccumThresh);

            CircleF[] circles = DetectTablets(img, minRadius, maxRadius, dp, minDist, cannyThresh, cannyAccumThresh, f);
            //PointF[] points = FindPattern(img.Convert<Gray, Byte>(), new Size(12, 9));
            //CircleF
            //CvInvoke.cvWaitKey(0);
            foreach (CircleF circle in circles)
            {
                CalculateTrueCordXYmm(ChessboardPoints, new PointF(circle.Center.X, circle.Center.Y));//424, 466));//454, 503));//423,464));//594, 750));//253, 525));
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
            //Image<Bgr, Byte> img2 = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/sortC2.jpg");
            //ChessboardPoints = FindPattern(img2.Convert<Gray, Byte>(), new Size(12, 9));
            ChessboardPoints = FindPattern(camera.GetImage(1).Convert<Gray, Byte>(), new Size(12, 9));



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
                if(true)
                {//center is in the radius of the circle
                        
                }
                if(true)
                {//if the circle crosses over
                    
                }
            }
            return knowTablets;
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

    }
}
