﻿#region Header
/// FileName: TrayVerifier.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Vision;
using Tmc.Common;
using TmcData;

namespace Tmc.Scada.Core
{
    public sealed class TrayVerifier : ControllerBase
    {
        private const string CameraName = "TrayVerifierCamera";

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
                VerifyTrayAsync(p.TraySpecification, p.VerificationMode);
            }
        }

        public override void Cancel()
        {
        }

        private VerificationResult DetermineValidity(Tray<Tablet> t1, Tray<Tablet> t2)
        {
            var validity = (t1.Equals(t2)) ? VerificationResult.Valid : VerificationResult.Invalid;

            return validity;
        }

        private void VerifyTrayAsync(Tray<Tablet> tray, VerificationMode mode)
        {
            Task.Run(() =>
                {
                    Logger.Instance.Write(String.Format("[TrayVerifier] Detecting tray. Verification mode: {0}", mode));
                    Tray<Tablet> detectedTray;
                    bool isTrayVisible = _trayDetector.GetTabletsInTray(out detectedTray);
                    VerificationResult verResult = VerificationResult.Invalid;
                    if (isTrayVisible)
                    {
                        if (mode == VerificationMode.Tray)
                        {
                            tray = new Tray<Tablet>();
                        }
                        verResult = DetermineValidity(tray, detectedTray);
                    }
                    Logger.Instance.Write(String.Format("[TrayVerifier] Is tray visible? {0}, tray validity: {1}",
                                            isTrayVisible, verResult));
                    IsRunning = false;
                    OnCompleted(new OnVerificationCompleteEventArgs
                        {
                            DetectedTray = detectedTray,
                            VerificationResult = verResult,
                            VerificationMode = mode,
                            OperationStatus = ControllerOperationStatus.Succeeded
                        });
                });
        }
    }

    public enum VerificationMode
    {
        Tray,
        Product
    }

    public enum VerificationResult
    {
        Valid,
        Invalid,
        NoTray
    }

    public class TrayVerifierParams : ControllerParams
    {
        public Tray<Tablet> TraySpecification { get; set; } 
        public VerificationMode VerificationMode { get;set; }
    }

    public class OnVerificationCompleteEventArgs : ControllerEventArgs
    {
        public Tray<Tablet> DetectedTray;
        public VerificationMode VerificationMode;
        public VerificationResult VerificationResult;
    }
}
