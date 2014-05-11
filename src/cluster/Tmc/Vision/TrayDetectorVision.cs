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


namespace Tmc.Vision
{
    public class TrayDetectorVision : VisionBase
    {
        private int minCircle, maxCircle;
        private int cannyThresh, cannyAccumThresh;
        double par3, par4;
        private Camera camera;
        private Point[] trayPoints = new Point[4];
        private Image<Bgr, Byte> img;
        private Image<Bgr, Byte> imgTray;
        private CircleF[] tablets;
        Tray<Tablet> trayList = new Tray<Tablet>();
        
        private Hsv[,] HSVTabletColoursRanges = new Hsv[5,2];//{{76,54}};

        public enum Side { Left = 0, Right };


        Form1 f;
        public TrayDetectorVision(Camera camera)
        {
            f = new Form1();
            f.Show();
            this.camera = camera;
            
            HSVTabletColoursRanges[(int)TabletColors.Green,(int)HSVRange.Low].Hue         = 76;
            HSVTabletColoursRanges[(int)TabletColors.Green,(int)HSVRange.Low].Satuation   = 24;
            HSVTabletColoursRanges[(int)TabletColors.Green,(int)HSVRange.Low].Value       = 139;
            HSVTabletColoursRanges[(int)TabletColors.Green,(int)HSVRange.High].Hue        = 87;
            HSVTabletColoursRanges[(int)TabletColors.Green,(int)HSVRange.High].Satuation  = 96;
            HSVTabletColoursRanges[(int)TabletColors.Green,(int)HSVRange.High].Value      = 226;

            HSVTabletColoursRanges[(int)TabletColors.Red,(int)HSVRange.Low].Hue           = 149;
            HSVTabletColoursRanges[(int)TabletColors.Red,(int)HSVRange.Low].Satuation     = 93;
            HSVTabletColoursRanges[(int)TabletColors.Red,(int)HSVRange.Low].Value         = 198;
            HSVTabletColoursRanges[(int)TabletColors.Red,(int)HSVRange.High].Hue          = 171;
            HSVTabletColoursRanges[(int)TabletColors.Red,(int)HSVRange.High].Satuation    = 128;
            HSVTabletColoursRanges[(int)TabletColors.Red,(int)HSVRange.High].Value        = 250;

            HSVTabletColoursRanges[(int)TabletColors.White,(int)HSVRange.Low].Hue         = 51;
            HSVTabletColoursRanges[(int)TabletColors.White,(int)HSVRange.Low].Satuation   = 6;
            HSVTabletColoursRanges[(int)TabletColors.White,(int)HSVRange.Low].Value       = 244;
            HSVTabletColoursRanges[(int)TabletColors.White,(int)HSVRange.High].Hue        = 91;
            HSVTabletColoursRanges[(int)TabletColors.White,(int)HSVRange.High].Satuation  = 12;
            HSVTabletColoursRanges[(int)TabletColors.White,(int)HSVRange.High].Value      = 250;

            HSVTabletColoursRanges[(int)TabletColors.Blue,(int)HSVRange.Low].Hue          = 111;
            HSVTabletColoursRanges[(int)TabletColors.Blue,(int)HSVRange.Low].Satuation    = 76;
            HSVTabletColoursRanges[(int)TabletColors.Blue,(int)HSVRange.Low].Value        = 157;
            HSVTabletColoursRanges[(int)TabletColors.Blue,(int)HSVRange.High].Hue         = 120;
            HSVTabletColoursRanges[(int)TabletColors.Blue,(int)HSVRange.High].Satuation   = 123;
            HSVTabletColoursRanges[(int)TabletColors.Blue,(int)HSVRange.High].Value       = 242;

            HSVTabletColoursRanges[(int)TabletColors.Black,(int)HSVRange.Low].Hue         = 102;
            HSVTabletColoursRanges[(int)TabletColors.Black,(int)HSVRange.Low].Satuation   = 15;
            HSVTabletColoursRanges[(int)TabletColors.Black,(int)HSVRange.Low].Value       = 90;
            HSVTabletColoursRanges[(int)TabletColors.Black,(int)HSVRange.High].Hue        = 145;
            HSVTabletColoursRanges[(int)TabletColors.Black,(int)HSVRange.High].Satuation  = 39;
            HSVTabletColoursRanges[(int)TabletColors.Black,(int)HSVRange.High].Value      = 167;

            //this.camera.ConnectionString = new Uri(@"http://192.168.0.190:8080/photoaf.jpg");
            //this.camera.ConnectionString = new Uri(@"http://192.168.0.243/ci-bin/video.jpg?size=2");
        }

        /// <summary>
        /// This function is resoncible for working out the state of trays
        /// </summary>
        /// <returns>
        /// it returns the state of the tray
        /// </returns>
        public Tray<Tablet> GetTabletsInTray()
        {
            //img = camera.GetImage();
            img = new Image<Bgr, byte>("C:/Users/leonid/Dropbox/ICTD internal folder/Subsystem components/Visual Recognition/camera part/trayY4.jpg");
            //img = camera.GetImageHttp(new Uri(@"http://www.wwrd.com.au/images/P/2260248_Fable%20s-4%2016cm%20Accent%20Plates-652383734586-co.jpg"));
            string win1 = "Test Window"; //The name of the window
            CvInvoke.cvNamedWindow(win1); //Create the window using the specific name
            //BitmapImage image = new Bi6tmapImage(new Uri("http://192.168.0.11:8080/photo.jpg"));
            //img = img.Resize(1088, 816, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);//divide by 3
            //CvInvoke.cvShowImage(win1, img); //Show the image
            CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            
            DetectTray();

            Image<Bgr, Byte> src = CropImage(img, 0, 300, img.Cols, 380);

            foreach (Point traypoint in trayPoints)
            {
                Rectangle rect = new Rectangle();
                rect.X = traypoint.X;
                rect.Y = traypoint.Y;
                rect.Width = 2;
                rect.Height = 2;
                src.Draw(rect, new Bgr(Color.Red), 6);
            }

            CvInvoke.cvShowImage(win1, src); //Show the image

            CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            DetectTabletsInTray();
            DetectTabletType();
            CvInvoke.cvDestroyWindow(win1); //Destory the window

            return trayList;
        }

        public enum pMinMax { Max = 0, Min };

        private bool DetectTray()                
        {
            Rectangle rect = new Rectangle();
            Point[] line = new Point[2];

            Image<Bgr, Byte> src = CropImage(img,0, 300, img.Cols, 380);
            //
            Hsv[] HSVT = new Hsv[2];
            HSVT[(int)HSVRange.Low].Hue           = 19.5;
            HSVT[(int)HSVRange.Low].Satuation     = 145.24;
            HSVT[(int)HSVRange.Low].Value         = 183.68;
            HSVT[(int)HSVRange.High].Hue          = 29;
            HSVT[(int)HSVRange.High].Satuation    = 331.92;
            HSVT[(int)HSVRange.High].Value        = 360;
            
            Image<Bgr, Byte> col = RemoveEverythingButRange(src, HSVT);
            Image<Gray, Byte> Gsrc = col.Convert<Gray, Byte>();

            line = scanImg(Gsrc);

            double Mag = Math.Sqrt(Math.Pow((line[0].X - line[1].X),2) + Math.Pow((line[0].Y - line[1].Y),2) );
            CvInvoke.cvShowImage("Test Window", col); //Show the image
            CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
            //int a = 0;
            int x = line[0].Y - line[1].Y;
            int y = line[0].X - line[1].X;

            trayPoints[1].X = line[1].X;
            trayPoints[1].Y = line[1].Y;

            trayPoints[3].X = line[0].X;
            trayPoints[3].Y = line[0].Y;

            trayPoints[0].X = (line[1].X - x);
            trayPoints[0].Y = (line[1].Y + y);

            trayPoints[2].X = (line[0].X - x);
            trayPoints[2].Y = (line[0].Y + y);
                //350,670
            //trayPoints[0].X = 82;//92/65, 271,227
            //trayPoints[0].Y = 55;
            //trayPoints[3].X = 261;
            //trayPoints[3].Y = 217;

            //rect.X = trayPoints[0].X;//Math.Min(trayPoints[0].X,trayPoints[2].X);//360;
            //rect.Y = trayPoints[0].Y;//Math.Min(trayPoints[0].Y,trayPoints[1].Y);//220;
            //rect.Width = trayPoints[3].X - trayPoints[0].X;//420+100;
            //rect.Height = trayPoints[3].Y - trayPoints[0].Y;//400;



            imgTray = src;//img.GetSubRect(rect);
            return true;
        }

        /// <summary>
        /// Ussed to detect tablets in the tray
        /// </summary>
        private void DetectTabletsInTray()
        {         
             while (true)
            {
                CvInvoke.cvWaitKey(10); 

                f.getValue(ref minCircle, ref maxCircle, ref par3, ref par4, ref cannyThresh,ref cannyAccumThresh);
                tablets = DetectTablets(imgTray, minCircle, maxCircle, par3, par4, cannyThresh, cannyAccumThresh, f);
                 
            }
        }

        /// <summary>
        /// Used to detect the colour of the tablet, only recognises good tablets and assemble the tray list
        /// </summary>
        private void DetectTabletType()
        {
            Rectangle rect = new Rectangle();
            Image<Bgr, byte> oneTablet;
            double dotAngle = 0.607;//result of of cos(ang) which use to multiply radius to give us the dot product
            TabletColors tabletColour;
            int cellInTray;


            foreach (CircleF tablet in tablets)
            {

                rect.X      = (int)(tablet.Center.X - (tablet.Radius * dotAngle)); 
                rect.Y      = (int)(tablet.Center.Y - (tablet.Radius * dotAngle));
                rect.Width  = (int)((tablet.Radius * dotAngle) * 2);
                rect.Height = (int)((tablet.Radius * dotAngle) * 2);

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
                cellInTray      = FindCellInTrayForTablet(imgTray.Cols, imgTray.Rows, tablet);
                trayList.Cells[cellInTray] = new Tablet { Color = tabletColour };
                //cellsTablets[cellInTray] = (int)tabletColour;
                CvInvoke.cvWaitKey(10); 
                //a.Draw(circle, new Bgr(Color.Red), 2);
            }
            f.trayFill(trayList);
            CvInvoke.cvWaitKey(0);
        }

        /// <summary>
        /// This Function gives us the cell a tablet is in givent it's location in the tray
        /// </summary>
        private int FindCellInTrayForTablet(int cols, int rows, CircleF circle)
        {
            int[] lineX = { 0, (int)(cols / 3), (int)((cols / 3) * 2), cols};//change this so it can have angled lines
            int[] lineY = { 0, (int)(rows / 3), (int)((rows / 3) * 2), rows};
            //int a;
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

        

     //   public enum Side { Left = 0, Right };




        public Point[] scanImg(Image<Gray, Byte> src)
        {
            //int Col;
            Gray gray;

            Point[] minMax = new Point[2];
            minMax[0].X = 0;
            minMax[0].Y = 0;

            minMax[1].X = src.Cols;
            minMax[1].Y = src.Rows;

            for(int row = 0; row < src.Rows;row++)
            {
                for(int col = 0; col < src.Cols;col++)
                {
                    gray = src[row,col];
                    if((minMax[0].X < col) && (gray.Intensity == 255))
                    {
                        minMax[0].X = col;
                    }
                    if((minMax[0].Y < row) && (gray.Intensity == 255))
                    {
                        minMax[0].Y = row;
                    }
                    if((minMax[1].X > col) && (gray.Intensity == 255))
                    {
                        minMax[1].X = col;
                    }
                    if((minMax[1].Y > row) && (gray.Intensity == 255))
                    {
                        minMax[1].Y = row;
                    }

                }
             
            }
            return line;
            //if (side == Side.Left) 
            //{
               // Col = 0;
            //for(
            //    for (int i = 0; i < src.Cols; i++)
            //    {
            //        gray = src[row, i];
            //        if (gray.Intensity == 255)
            //        {
            //            return i;
            //        }

            //    }
            //    return src.Cols;
            //}

            //else if (side == Side.Right)
            //{
            //    Col = src.Cols - 1;
            //    for (int i = Col; i > 0; i--)
            //    {
            //        gray = src[row, i];
            //        if (gray.Intensity == 255)
            //        {
            //            return i;
            //        }

            //    }
            //    return 0;
            //}
            //else  return 0; //throw
        }
        //public Image<Gray, byte> ToCannyEdge(Image<Bgr, byte
    
        /*private void DetectTrayType()
        {
            
        }*/

        /*private bool CheckTrayEmpty()
        {

            return true;
        }*/
    }
}
