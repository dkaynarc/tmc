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
    public class Camera : ICamera
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }

        public Capture CaptureDevice { get { return _capture; } private set { _capture = value; } }

        private Capture _capture;
        private HardwareStatus _hardwareStatus;

        public Camera()
        {
            _hardwareStatus = HardwareStatus.Offline;
        }

        public Image<Bgr, byte> GetImage()
        {
            var img = _capture.QueryFrame();
            if (img == null)
            {
                _hardwareStatus = HardwareStatus.Failed;
                throw new InvalidOperationException("Could not get image from capture device");
            }
            return img;
        }

        public void Shutdown()
        {
            _capture.Dispose();
        }

        public HardwareStatus GetStatus()
        {
            return _hardwareStatus;
        }

        public void Initialise()
        {
            try
            {
                _capture = new Capture(ConnectionString);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Unable to open camera using connection string " + ConnectionString);
            }
        }

        public void SetParameters(Dictionary<string, string> parameters)
        {
            string s = "";
            if (parameters.TryGetValue("Name", out s))
            {
                this.Name = s;
            }
            if (parameters.TryGetValue("ConnectionString", out s))
            {
                this.ConnectionString = s;
            }
            else
            {
                throw new InvalidOperationException("No connection string passed to camera");
            }
        }
    }
}
