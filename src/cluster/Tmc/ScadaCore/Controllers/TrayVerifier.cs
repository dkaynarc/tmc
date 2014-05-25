using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Vision;
using Tmc.Common;

namespace Tmc.Scada.Core
{
    public sealed class TrayVerifier : ControllerBase
    {
        private const string CameraName = "TrayDetectorCamera";

        private TrayDetectorVision _trayDetector;
        public TrayVerifier(ClusterConfig config) : base(config)
        {
            ICamera c;
            if (config.Cameras.TryGetValue(CameraName, out c))
            {
                _trayDetector = new TrayDetectorVision(c as Camera);
            }
            else
            {
                throw new ArgumentException("Unable to get " + CameraName + "from ClusterConfig");
            }
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as TrayVerifierParams;
            if (p != null)
            {
                IsRunning = true;
                VerifyTrayAsync(p.TraySpecification);
            }
        }

        public override void Cancel()
        {
        }

        private bool DetermineValidity(Tray<Tablet> t1, Tray<Tablet> t2)
        {
            return (t1 == t2);
        }

        private void VerifyTrayAsync(Tray<Tablet> tray)
        {
            var task = new Task(() =>
                {
                    //var detected = _trayDetector.RunTrayDetectionVision();
                    //var isValid = DetermineValidity(tray, detected);
                    //IsRunning = false;
                    //OnCompleted(new OnVerificationCompleteEventArgs
                    //    {
                    //        DetectedTray = detected,
                    //        IsValid = isValid,
                    //        OperationStatus = ControllerOperationStatus.Succeeded
                    //    })
                });
            task.Start();
        }
    }

    public class TrayVerifierParams : ControllerParams
    {
        public Tray<Tablet> TraySpecification { get; set; }
    }

    public class OnVerificationCompleteEventArgs : ControllerEventArgs
    {
        public Tray<Tablet> DetectedTray;
        public bool IsValid;
    }
}
