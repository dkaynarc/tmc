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
    public class TrayDetectorVision : VisionBase
    {
        private int minCircle, maxCircle;
        private int cannyThresh, cannyAccumThresh;
        double par3, par4;
        //private int[] Xarray;
        //private int[] Yarray;
        private Camera camera;
        private Point[] trayPoints = new Point[4];
        private Image<Bgr, Byte> img;
        private Image<Bgr, Byte> imgTray;
        private CircleF[] tablets;
        //List<T> 
        Tray<ColourTablets> trayList = new Tray<ColourTablets>();

        private Hsv[,] HSVTabletColoursRanges = new Hsv[5,2];//{{76,54}};

        Form1 f;
        public TrayDetectorVision(Camera camera)
        {
            f = new Form1();
            f.Show();
            this.camera = camera;
            
            HSVTabletColoursRanges[(int)ColourTablets.Green,(int)HSVRange.Low].Hue         = 76;
            HSVTabletColoursRanges[(int)ColourTablets.Green,(int)HSVRange.Low].Satuation   = 24;
            HSVTabletColoursRanges[(int)ColourTablets.Green,(int)HSVRange.Low].Value       = 139;
            HSVTabletColoursRanges[(int)ColourTablets.Green,(int)HSVRange.High].Hue        = 87;
            HSVTabletColoursRanges[(int)ColourTablets.Green,(int)HSVRange.High].Satuation  = 96;
            HSVTabletColoursRanges[(int)ColourTablets.Green,(int)HSVRange.High].Value      = 226;

            HSVTabletColoursRanges[(int)ColourTablets.Red,(int)HSVRange.Low].Hue           = 149;
            HSVTabletColoursRanges[(int)ColourTablets.Red,(int)HSVRange.Low].Satuation     = 93;
            HSVTabletColoursRanges[(int)ColourTablets.Red,(int)HSVRange.Low].Value         = 198;
            HSVTabletColoursRanges[(int)ColourTablets.Red,(int)HSVRange.High].Hue          = 171;
            HSVTabletColoursRanges[(int)ColourTablets.Red,(int)HSVRange.High].Satuation    = 128;
            HSVTabletColoursRanges[(int)ColourTablets.Red,(int)HSVRange.High].Value        = 250;

            HSVTabletColoursRanges[(int)ColourTablets.White,(int)HSVRange.Low].Hue         = 51;
            HSVTabletColoursRanges[(int)ColourTablets.White,(int)HSVRange.Low].Satuation   = 6;
            HSVTabletColoursRanges[(int)ColourTablets.White,(int)HSVRange.Low].Value       = 244;
            HSVTabletColoursRanges[(int)ColourTablets.White,(int)HSVRange.High].Hue        = 91;
            HSVTabletColoursRanges[(int)ColourTablets.White,(int)HSVRange.High].Satuation  = 12;
            HSVTabletColoursRanges[(int)ColourTablets.White,(int)HSVRange.High].Value      = 250;

            HSVTabletColoursRanges[(int)ColourTablets.Blue,(int)HSVRange.Low].Hue          = 111;
            HSVTabletColoursRanges[(int)ColourTablets.Blue,(int)HSVRange.Low].Satuation    = 76;
            HSVTabletColoursRanges[(int)ColourTablets.Blue,(int)HSVRange.Low].Value        = 157;
            HSVTabletColoursRanges[(int)ColourTablets.Blue,(int)HSVRange.High].Hue         = 120;
            HSVTabletColoursRanges[(int)ColourTablets.Blue,(int)HSVRange.High].Satuation   = 123;
            HSVTabletColoursRanges[(int)ColourTablets.Blue,(int)HSVRange.High].Value       = 242;

            HSVTabletColoursRanges[(int)ColourTablets.Black,(int)HSVRange.Low].Hue         = 102;
            HSVTabletColoursRanges[(int)ColourTablets.Black,(int)HSVRange.Low].Satuation   = 15;
            HSVTabletColoursRanges[(int)ColourTablets.Black,(int)HSVRange.Low].Value       = 90;
            HSVTabletColoursRanges[(int)ColourTablets.Black,(int)HSVRange.High].Hue        = 145;
            HSVTabletColoursRanges[(int)ColourTablets.Black,(int)HSVRange.High].Satuation  = 39;
            HSVTabletColoursRanges[(int)ColourTablets.Black,(int)HSVRange.High].Value      = 167;

            //this.camera.ConnectionString = new Uri(@"http://192.168.0.190:8080/photoaf.jpg");
        }

        public void RunTrayDetectionVision()
        {
            //img = camera.GetImage();
            img = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/belt sort/belt mid.jpg");
            //img = camera.GetImageHttp(new Uri(@"http://www.wwrd.com.au/images/P/2260248_Fable%20s-4%2016cm%20Accent%20Plates-652383734586-co.jpg"));
            string win1 = "Test Window"; //The name of the window
            CvInvoke.cvNamedWindow(win1); //Create the window using the specific name
            //BitmapImage image = new Bi6tmapImage(new Uri("http://192.168.0.11:8080/photo.jpg"));
            //img = img.Resize(1088, 816, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);//divide by 3
            //CvInvoke.cvShowImage(win1, img); //Show the image
            CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            
            DetectTray();
            CvInvoke.cvShowImage(win1, imgTray); //Show the image
            CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            DetectTabletsInTray();
            DetectTabletType();
            CvInvoke.cvDestroyWindow(win1); //Destory the window
        }

        private bool DetectTray()                
        {
            Rectangle rect = new Rectangle();

            trayPoints[0].X = 92;//92/65, 271,227
            trayPoints[0].Y = 65;
            trayPoints[3].X = 271;
            trayPoints[3].Y = 227;

            rect.X = trayPoints[0].X;//Math.Min(trayPoints[0].X,trayPoints[2].X);//360;
            rect.Y = trayPoints[0].Y;//Math.Min(trayPoints[0].Y,trayPoints[1].Y);//220;
            rect.Width = trayPoints[3].X - trayPoints[0].X;//420+100;
            rect.Height = trayPoints[3].Y - trayPoints[0].Y;//400;
            imgTray = img.GetSubRect(rect);
            return true;
        }

        /// <summary>
        /// Ussed to detect tablets in the tray
        /// </summary>
        private void DetectTabletsInTray()
        {
            //Image<Bgr, byte> abc = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/belt sort/belt mid.jpg");
            //Rectangle rect = new Rectangle();
            //rect.X = 232;
            //rect.Y = 83;
            //rect.Width = 10;
            //rect.Height = 10;
            //Image<Bgr, byte> tab = abc.GetSubRect(rect);
            //CvInvoke.cvShowImage("Test Window", tab); //Show the image
            //detectColour(tab, HSVTabletColoursRanges);
             //while (true)
            //{
                //CvInvoke.cvWaitKey(10); 

                f.getValue(ref minCircle, ref maxCircle, ref par3, ref par4, ref cannyThresh,ref cannyAccumThresh);
                tablets = DetectTablets(imgTray, minCircle, maxCircle, par3, par4, cannyThresh, cannyAccumThresh, f);
            //}
        }

        /// <summary>
        /// Used to detect the colour of the tablet, only recognises good tablets and assemble the tray list
        /// </summary>
        private void DetectTabletType()
        {
            Rectangle rect = new Rectangle();
            Image<Bgr, byte> oneTablet;
            double rad = 0.607;
            ColourTablets tabletColour;
            int cellInTray;

            int[] cellsTablets = {0,0,0,0,0,0,0,0,0}; 

            foreach (CircleF tablet in tablets)
            {

                rect.X      = (int)(tablet.Center.X - (tablet.Radius * rad)); //Math.Min(trayPoints[0].X,trayPoints[2].X);//360;
                rect.Y      = (int)(tablet.Center.Y - (tablet.Radius * rad));//Math.Min(trayPoints[0].Y,trayPoints[1].Y);//220;
                rect.Width  = (int)((tablet.Radius * rad) * 2);//420+100;
                rect.Height = (int)((tablet.Radius * rad) * 2);//400;

                if (rect.X < 0) rect.X = 0;
                if (rect.Y < 0) rect.Y = 0;
                if ((rect.X + rect.Width) > imgTray.Cols)
                { 
                    rect.Width -= (rect.X + rect.Width) - imgTray.Cols;
                }
                if ((rect.Y + rect.Height) > imgTray.Rows)
                {
                    rect.Height -= (rect.Y + rect.Height) - imgTray.Rows;
                }

                oneTablet = imgTray.GetSubRect(rect);
                CvInvoke.cvShowImage("Test Window", oneTablet); //Show the image
                tabletColour    = detectColour(oneTablet, HSVTabletColoursRanges);
                cellInTray      = FindCellInTray(imgTray.Cols, imgTray.Rows, tablet);
                cellsTablets[cellInTray] = (int)tabletColour;
                CvInvoke.cvWaitKey(10); 
                //a.Draw(circle, new Bgr(Color.Red), 2);
            }
            f.trayFill(cellsTablets);
            CvInvoke.cvWaitKey(0);
        }
        private int FindCellInTray(int cols, int rows, CircleF circle)
        {
            int[] lineX = { 0, (int)(cols / 3), (int)((cols / 3) * 2), cols};//change this so it can have angled lines
            int[] lineY = { 0, (int)(rows / 3), (int)((rows / 3) * 2), rows};
            int a;
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
            

            //trayList.cells[0] = 1;
            //cell[0] = 1;
            //Cells[1] = 1;

        }
        /*private void DetectTrayType()
        {
            
        }*/

        /*private bool CheckTrayEmpty()
        {

            return true;
        }*/
    }
}
