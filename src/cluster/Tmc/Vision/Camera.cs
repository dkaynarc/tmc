using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using Tmc.Common;

namespace Tmc.Vision
{
    public class Camera : ICamera
    {
        public string Name { get; set; }
        public Uri ConnectionString { get; set; }
        public Hsv[,] HSVColorRanges { get; set; }
        public int MinRadius { get; set; }
        public int MaxRadius { get; set; }
        public double Dp { get; set; }
        public double MinDist { get; set; }
        public int CannyThresh { get; set; }
        public int CannyAccumThresh { get; set; }
        private HardwareStatus _hardwareStatus;

        public Camera()
        {
            _hardwareStatus = HardwareStatus.Offline;
            HSVColorRanges = new Hsv[5, 2];
        }

        public Image<Bgr, byte> GetImage()
        {
            var image = GetImageHttp(ConnectionString);

            if (image == null)
            {
                _hardwareStatus = HardwareStatus.Failed;
                throw new InvalidOperationException("Could not get image from capture device");
            }

            return image;
        }

        public Image<Bgr, byte> GetImage(int rotation)
        {
            var image = GetImageHttp(ConnectionString, 1);

            if (image == null)
            {
                _hardwareStatus = HardwareStatus.Failed;
                throw new InvalidOperationException("Could not get image from capture device");
            }

            return image;
        }

        public Image<Bgr, byte> GetImageHttp(Uri uri)
        {
            Image<Bgr, byte> emguImg = null;
            var request = WebRequest.Create(uri);
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var img = Bitmap.FromStream(stream) as Bitmap;
                emguImg = new Image<Bgr, byte>(img);
            }
            return emguImg;
        }

        public Image<Bgr, byte> GetImageHttp(Uri uri, int rotation)
        {
            Image<Bgr, byte> emguImg = null;
            var request = WebRequest.Create(uri);
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var img = Bitmap.FromStream(stream) as Bitmap;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                emguImg = new Image<Bgr, byte>(img);
            }
            return emguImg;
        }

        public Bitmap GetImageFromUrl()
        {
            Bitmap b = null;

            var request = WebRequest.Create(ConnectionString);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                b = Bitmap.FromStream(stream) as Bitmap;
            }

            return b;
        }

        public void Shutdown()
        {
            _hardwareStatus = HardwareStatus.Offline;
        }

        public void EmergencyStop()
        {
        }

        public HardwareStatus GetStatus()
        {
            return _hardwareStatus;
        }

        public void Initialise()
        {
            try
            {
                _hardwareStatus = HardwareStatus.Operational;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to open camera using connection string " + ConnectionString, ex);
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
                this.ConnectionString = new Uri(s);
            }
            else
            {
                throw new InvalidOperationException("No connection string passed to camera");
            }

            ParseCalibrationData(parameters);            
        }

        private void ParseCalibrationData(Dictionary<string, string> parameters)
        {
            ParseHsvColorRanges(parameters);
            ParseOtherCalibrationData(parameters);
        }

        private void ParseOtherCalibrationData(Dictionary<string, string> parameters)
        {
            int minRadius = 0, maxRadius = 0, cannyThresh = 0, cannyAccumThresh = 0;
            double dp = 0, minDist = 0;
            string s = "";

            if (parameters.TryGetValue("MinRadius", out s))
            {
                int.TryParse(s, out minRadius);
                this.MinRadius = minRadius;
            }
            if (parameters.TryGetValue("MaxRadius", out s))
            {
                int.TryParse(s, out maxRadius);
                this.MaxRadius = maxRadius;
            }
            if (parameters.TryGetValue("CannyThresh", out s))
            {
                int.TryParse(s, out cannyThresh);
                this.CannyThresh = cannyThresh;
            }
            if (parameters.TryGetValue("CannyAccumThresh", out s))
            {
                int.TryParse(s, out cannyAccumThresh);
                this.CannyAccumThresh = CannyAccumThresh;
            }
            if (parameters.TryGetValue("Dp", out s))
            {
                double.TryParse(s, out dp);
                this.Dp = dp;
            }
            if (parameters.TryGetValue("minDist", out s))
            {
                double.TryParse(s, out minDist);
                this.MinDist = minDist;
            }
        }

        private void ParseHsvColorRanges(Dictionary<string, string> parameters)
        {
            string blackLow, blackHigh;
            string blueLow, blueHigh;
            string greenLow, greenHigh;
            string redLow, redHigh;
            string whiteLow, whiteHigh;

            parameters.TryGetValue("BlackLow", out blackLow);
            parameters.TryGetValue("BlackHigh", out blackHigh);

            parameters.TryGetValue("BlueLow", out blueLow);
            parameters.TryGetValue("BlueHigh", out blueHigh);

            parameters.TryGetValue("GreenLow", out greenLow);
            parameters.TryGetValue("GreenHigh", out greenHigh);

            parameters.TryGetValue("RedLow", out redLow);
            parameters.TryGetValue("RedHigh", out redHigh);

            parameters.TryGetValue("WhiteLow", out whiteLow);
            parameters.TryGetValue("WhiteHigh", out whiteHigh);

            this.HSVColorRanges = ParseHsvColorRanges(blackLow, blackHigh,
                                                        blueLow, blueHigh,
                                                        greenLow, greenHigh,
                                                        redLow, redHigh,
                                                        whiteLow, whiteHigh);
        }

        private static Hsv[,] ParseHsvColorRanges(string blackLow, string blackHigh,
                                                    string blueLow, string blueHigh,
                                                    string greenLow, string greenHigh,
                                                    string redLow, string redHigh,
                                                    string whiteLow, string whiteHigh)
        {
            var ranges = new Hsv[5, 2];

            ranges[(int)TabletColors.Black, (int)VisionBase.HSVRange.Low] = ParseHsv(blackLow);
            ranges[(int)TabletColors.Black, (int)VisionBase.HSVRange.High] = ParseHsv(blackHigh);
            ranges[(int)TabletColors.Blue, (int)VisionBase.HSVRange.Low] = ParseHsv(blueLow);
            ranges[(int)TabletColors.Blue, (int)VisionBase.HSVRange.High] = ParseHsv(blueHigh);
            ranges[(int)TabletColors.Green, (int)VisionBase.HSVRange.Low] = ParseHsv(greenLow);
            ranges[(int)TabletColors.Green, (int)VisionBase.HSVRange.High] = ParseHsv(greenHigh);
            ranges[(int)TabletColors.Red, (int)VisionBase.HSVRange.Low] = ParseHsv(redLow);
            ranges[(int)TabletColors.Red, (int)VisionBase.HSVRange.High] = ParseHsv(redHigh);
            ranges[(int)TabletColors.White, (int)VisionBase.HSVRange.Low] = ParseHsv(whiteLow);
            ranges[(int)TabletColors.White, (int)VisionBase.HSVRange.High] = ParseHsv(whiteHigh);

            return ranges;
        }

        private static Hsv ParseHsv(string s)
        {
            var hsv = new Hsv();

            double hue = 0;
            double sat = 0;
            double val = 0;

            if (!String.IsNullOrEmpty(s))
            {
                var words = new List<string>(s.Split(','));
                words.ForEach(x => x.Trim());

                if (words.Count == 3)
                {
                    Double.TryParse(words[0], out hue);
                    Double.TryParse(words[1], out sat);
                    Double.TryParse(words[2], out val);
                }

                hsv.Hue = hue;
                hsv.Satuation = sat;
                hsv.Value = val;
            }
            return hsv;
        }
    }
}
