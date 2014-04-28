using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;
using Tmc.Robotics;
using Tmc.Vision;
using System.Drawing;
using System.Threading;

namespace Tmc.Scada.Core
{
    public sealed class Sorter : ControllerBase
    {
        public int MaxShakeRetryAttempts { get; set; }
        public int ShakeRetryAttempts { get; private set; }
        private SorterVision _vision;
        private SorterRobot _robot;
        private CancellationTokenSource _cancelTokenSource; 

        public Sorter(ClusterConfig config) : base(config)
        {
            this.MaxShakeRetryAttempts = 1;
            this.ShakeRetryAttempts = 0;
            this._vision = new SorterVision(config.Cameras["SorterCamera"] as Camera);
            this._robot = config.Robots[typeof(SorterRobot)] as SorterRobot;
            this._cancelTokenSource = new CancellationTokenSource();

            if (_vision == null)
            {
                throw new ArgumentException("Could not create SorterVision");
            }
            if (_robot == null)
            {
                throw new ArgumentException("Could not retrieve a SorterRobot from ClusterConfig");
            }
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as SorterParams;
            if (p != null)
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    SortAsync(p.Magazine);
                }
            }
        }

        public override void Cancel()
        {
            if (IsRunning && _cancelTokenSource != null)
            {
                _cancelTokenSource.Cancel();
                IsRunning = false;
                OnCompleted(new ControllerEventArgs() { OperationStatus = ControllerOperationStatus.Cancelled });
            }
        }

        private void SortAsync(TabletMagazine mag)
        {
            var ct = _cancelTokenSource.Token;
            Task.Run(() => 
                {
                    var status = Sort(mag, ct);
                    IsRunning = false;
                    OnCompleted(new ControllerEventArgs() { OperationStatus = status });
                }, ct);
        }

        private ControllerOperationStatus Sort(TabletMagazine mag, CancellationToken ct)
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                var visibleTablets = _vision.GetVisibleTablets();
                while (ShakeRetryAttempts < MaxShakeRetryAttempts)
                {
                    if (ct.IsCancellationRequested)
                    {
                        status = ControllerOperationStatus.Cancelled;
                        return status;
                    }
                    if (visibleTablets.Count == 0)
                    {
                        //_robot.Shake();
                        visibleTablets = _vision.GetVisibleTablets();
                        break;
                    }
                    ShakeRetryAttempts++;
                }
                foreach (var tablet in visibleTablets)
                {
                    if (mag.IsFull() || ct.IsCancellationRequested)
                    {
                        break;
                    }
                    if (!mag.GetFullSlots().Contains(tablet.Color))
                    {
                        PlaceTablet(tablet, mag);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(new LogEntry(ex));
                status = ControllerOperationStatus.Failed;
            }
            return status;
        }

        private void PlaceTablet(Tablet tablet, TabletMagazine mag)
        {
            var p = TransformToRobotSpace(tablet.LocationPoint);
            _robot.GetTablet(p.X, p.Y, mag.GetSlotIndex(tablet.Color));
            mag.AddTablet(tablet.Color);
        }

        private Point TransformToRobotSpace(Point p)
        {
            //TODO: add logic to transform from camera space to sorter robot space.
            return p;
        }
    }

    public class SorterParams : ControllerParams
    {
        public TabletMagazine Magazine;
    }
}
