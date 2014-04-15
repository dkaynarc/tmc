using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Tmc.Common;

namespace Tmc.Vision
{
    public class Camera : IHardware
    {
        public Camera()
        {
        }

        public void GetImage(ref IntPtr image)
        {
             
        }

        public bool GetStatues()
        {

            return true;
        }
    }
}
