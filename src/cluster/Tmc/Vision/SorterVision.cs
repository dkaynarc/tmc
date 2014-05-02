using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Tmc.Common;

namespace Tmc.Vision
{
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
            this.camera = camera;
        }

        public List<Tablet> GetVisibleTablets()
        {
            List<Tablet> tablet = new List<Tablet>();           
            img = camera.GetImage();
            CircleF[] circles = DetectTablets(img, minCircle, maxCircle, par3, par4, cannyThresh, cannyAccumThresh, f);
            DetectOverLap();
            DetectDamagedTablet();
            GetXYZForTablets();
             
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
  
    }
}
