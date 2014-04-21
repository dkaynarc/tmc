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
using System.Drawing;

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
            Image<Bgr, Byte> img = new Image<Bgr, byte>("../../tray.jpg");//var img = _capture.QueryFrame();
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
                //_capture = new Capture("http://192.168.0.11:8080/photo.jpg");//ConnectionString);
                //string win1 = "Test Window"; //The name of the window
                //CvInvoke.cvNamedWindow(win1); //Create the window using the specific name
                //BitmapImage image = new BitmapImage(new Uri("http://192.168.0.11:8080/photo.jpg"));

                Image<Bgr, Byte> img = new Image<Bgr, byte>("../../tray.jpg"); //Create an image of 400x200 of Blue color
                //CvInvoke.cvShowImage(win1, img); //Show the image
                //CvInvoke.cvWaitKey(0);  //Wait for the key pressing event
                //CvInvoke.cvDestroyWindow(win1); //Destory the window
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
