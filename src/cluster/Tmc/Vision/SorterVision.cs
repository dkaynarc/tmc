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
    class SorterVision : VisionBase
    {
        int min, max;
        double par1;
        double par2;

        private Camera camera;
        private Image<Bgr, Byte> img;
        Form1 f;

        public SorterVision(Camera camera)
        {
            this.camera = camera;
        }

        public List<Tablet> GetVisibleTablets()
        {
            var tablets = new List<Tablet>();
            return tablets;
        }
        
        public void RunSorterCamera()
        {
            CircleF[] circles = DetectTablets(img, min, max, par1, par2, f);
            DetectOverLap();
            DetectDamagedTablet();
            GetXYZForTablets();
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
