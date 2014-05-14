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
        int minCircle, maxCircle;
        int cannyThresh, cannyAccumThresh;
        double par3, par4;

        private Camera camera;
        private Image<Bgr, Byte> img;
        Form1 f;

        public SorterVision(Camera camera)
        {
            f = new Form1();
            f.Show();
            this.camera = camera;
            //do calibration
            //this.camera.ConnectionString = new Uri(@"https://fbcdn-sphotos-b-a.akamaihd.net/hphotos-ak-frc3/t1.0-9/10270404_10202712962477674_682458036245271256_n.jpg");
        }

        public List<Tablet> GetVisibleTablets()
        {
            List<Tablet> tablet = new List<Tablet>();           
            //img = camera.GetImage();
            img = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/sortC2.jpg");

            f.getValue(ref minCircle, ref maxCircle, ref par3, ref par4, ref cannyThresh, ref cannyAccumThresh);

            CircleF[] circles = DetectTablets(img, minCircle, maxCircle, par3, par4, cannyThresh, cannyAccumThresh, f);
            PointF[] points = FindPattern(img.Convert<Gray, Byte>(), new Size(12, 9));
            CalculateTrueCordXYmm(points, new PointF(433,474));//594, 750));//253, 525));
            DetectOverLap();
            DetectDamagedTablet();
            GetXYZForTablets();
            CvInvoke.cvWaitKey(0);

            return tablet;
        }
        
        private void DetectOverLap()
        {
              
        }
             
        private void GetXYZForTablets()
        {
            
        }

        private void DetectDamagedTablet()
        { 
            
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

        private PointF CalculateTrueCordXYmm(PointF[] refPoints, PointF targetPoint)
        { 
            //assume each black square is 2X2cm
            //refPoints[0].X
            //double Xang = (refPoints[11].Y - refPoints[0].Y);
            //double Yang = refPoints[96].X  - refPoints[0].X;
            //Point[] closest = new Point[2];

            //for (int i = 0; i < 12; i++)
            //{
            //    if ((refPoints[96 + i].X < targetPoint.X) && (i != 0))
            //    {
            //        closest[0].X = i;
            //        closest[1].X = 96 + (i );
            //    }
            //    else if(refPoints[96 + i].X < targetPoint.X)
            //    {
            //        closest[0].X = i;
            //        closest[1].X = 96;
            //    }

            //}

            //for (int i = 0; i < 9; i++)
            //{
            //    if ((refPoints[11 + i*12].X < targetPoint.X) && (i != 0))
            //    {
            //        closest[0].Y = i;
            //        closest[1].Y = 11 + (i*12 );
            //    }
            //    else if (refPoints[11 + i*12].X < targetPoint.X)
            //    {
            //        closest[0].Y = i;
            //        closest[1].Y = 11;
            //    }
            //}
            //double mX1 = (refPoints[0].Y - refPoints[11].Y) / (refPoints[0].X - refPoints[11].X);//work out gradient
            //double mY1 = (refPoints[0].Y - refPoints[96].Y) / (refPoints[0].X - refPoints[96].X);//work out gradient

            //double mX2 = (refPoints[106].Y - refPoints[107].Y) / (refPoints[106].X - refPoints[107].X);//work out gradient

            //double mX = (refPoints[closest[0].Y].Y - refPoints[closest[1].Y].Y) / (refPoints[closest[0].Y].X - refPoints[closest[1].Y].X);//work out gradient
            //double mY = (refPoints[closest[0].X].Y - refPoints[closest[1].X].Y) / (refPoints[closest[0].X].X - refPoints[closest[1].X].X);//work out gradient

            Point qwe = getClosestPointToTargetOnChessboard(refPoints, targetPoint, new Size(12,9));
            int loc = qwe.X + qwe.Y;
            PointF locationXYmm = new PointF();
            double pixcelTommY;
            double pixcelTommX;
            //Point dis;
            //double Mag = Math.Sqrt(Math.Pow((refPoints[0].X - refPoints[1].X), 2) + Math.Pow((refPoints[0].Y - refPoints[1].Y), 2));
            double MagY = Math.Sqrt(Math.Pow((refPoints[0].X - refPoints[1].X), 2) + Math.Pow((refPoints[0].Y - refPoints[1].Y), 2));
            double MagX = Math.Sqrt(Math.Pow((refPoints[0].X - refPoints[12].X), 2) + Math.Pow((refPoints[0].Y - refPoints[12].Y), 2));


            pixcelTommY = 200 / MagY;
            pixcelTommX = 200 / MagX;

            //locationXYmm.X = (float)(pixcelTommX * ((targetPoint.X - refPoints[107].X) + ((targetPoint.X - refPoints[107].X) * mX2)))+2200;
            //locationXYmm.Y = (float)(pixcelTommY * ((targetPoint.Y - refPoints[107].Y) + ((targetPoint.Y - refPoints[107].Y) / mY)))+1600;



            locationXYmm.X = (float)(pixcelTommX * ((targetPoint.X - refPoints[0].X)));
            locationXYmm.Y = (float)(pixcelTommY * ((targetPoint.Y - refPoints[0].Y)));

            return targetPoint;
        }

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

    }
}
