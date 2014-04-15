using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Tmc.Vision
{
    class TrayDetectorVision
    {
        private fixed int Xarray[4];
        private fixed int Yarray[4];
        private Camera camera;

        TrayDetectorVision(Camera camera)
        {
            this.camera = camera;
        }

        public void RunTrayDetectionVision()
        {
            DetectTray();
            DetectTabletsInTray();
        }

        private bool DetectTray()
        {

            return true;
        }

        private void DetectTabletsInTray()
        {
            
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
