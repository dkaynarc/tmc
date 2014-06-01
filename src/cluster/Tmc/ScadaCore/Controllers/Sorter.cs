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
    public enum SorterAction
    {
        Sort,
        LoadToConveyor,
        LoadToBuffer
    }

    public sealed class Sorter : ControllerBase
    {
        public int MaxShakeRetryAttempts { get; set; }
        private SorterVision _vision;
        private SorterRobot _robot;
        private CancellationTokenSource _cancelTokenSource;

        public Sorter(ClusterConfig config) : base(config)
        {
            this.MaxShakeRetryAttempts = 1;
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
                    switch (p.Action)
                    {
                        case SorterAction.Sort: 
                            SortAsync(p.Magazine);
                            break;
                        case SorterAction.LoadToBuffer:
                            LoadToBufferAsync();
                            break;
                        case SorterAction.LoadToConveyor:
                            LoadToConveyorAsync();
                            break;
                    }
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

        private void LoadToConveyorAsync()
        {
            Task.Run(() =>
                {
                    var status = LoadToConveyor();
                    IsRunning = false;
                    OnCompleted(new ControllerEventArgs() { OperationStatus = status });
                });
        }

        private void LoadToBufferAsync()
        {
            Task.Run(() =>
                {
                    var status = LoadToConveyor();
                    IsRunning = false;
                    OnCompleted(new ControllerEventArgs() { OperationStatus = status });
                });
        }

        private ControllerOperationStatus LoadToConveyor()
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                _robot.ReturnMagazine();
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(new LogEntry(ex));
                status = ControllerOperationStatus.Failed;
            }
            return status;
        }

        private ControllerOperationStatus LoadToBuffer()
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                _robot.GetMagazine();
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(new LogEntry(ex));
                status = ControllerOperationStatus.Failed;
            }
            return status;
        }

        private ControllerOperationStatus Sort(TabletMagazine mag, CancellationToken ct)
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                var visibleTablets = this.GetVisibleTablets();
                foreach (var tablet in visibleTablets)
                {
                    if (mag.IsFull() || ct.IsCancellationRequested)
                    {
                        status = ControllerOperationStatus.Cancelled;
                        return status;
                    }
                    if (!mag.IsSlotFull(tablet.Color))
                    {
                        PlaceTablet(tablet, mag);
                        visibleTablets = this.GetVisibleTablets();
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

        private List<Tablet> GetVisibleTablets()
        {
            int shakeRetryAttempts = 0;
            var visibleTablets = _vision.GetVisibleTablets();
            while ((visibleTablets.Count() < 0) && (shakeRetryAttempts < MaxShakeRetryAttempts))
            {
                _robot.Shake();
                visibleTablets = _vision.GetVisibleTablets();
                shakeRetryAttempts++;
            }
            return visibleTablets;
        }

        private void PlaceTablet(Tablet tablet, TabletMagazine mag)
        {
            var p = TransformToRobotSpace(tablet.LocationPoint);
            //_robot.GetTablet(p.X, p.Y, mag.GetSlotIndex(tablet.Color));
            mag.AddTablet(tablet.Color);
        }

        private PointF TransformToRobotSpace(PointF p)
        {
            //TODO: add logic to transform from camera space to sorter robot space.
            return p;
        }
    }

    public class SorterParams : ControllerParams
    {
        public TabletMagazine Magazine;
        public SorterAction Action;
    }
}
