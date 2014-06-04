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
using TmcData;

namespace Tmc.Scada.Core
{
    public enum SorterAction
    {
        Sort,
        LoadToConveyor,
        LoadToBuffer,
        Undefined
    }

    public sealed class Sorter : ControllerBase
    {
        public int MaxShakeRetryAttempts { get; set; }
        private SorterVision _vision;
        private SorterRobot _robot;
        private CancellationTokenSource _cancelTokenSource;
        private Dictionary<SorterAction, Action<SorterParams>> _actionMap;

        public Sorter(ClusterConfig config)
            : base(config)
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

            _actionMap = new Dictionary<SorterAction, Action<SorterParams>>()
            {
                { SorterAction.LoadToBuffer, (x) => LoadToBufferAsync() },
                { SorterAction.LoadToConveyor, (x) => LoadToConveyorAsync() },
                { SorterAction.Sort, (x) => SortAsync(x.Magazine) }
            };
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as SorterParams;
            if (p != null)
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    _actionMap[p.Action](p);
                }
            }
            else
            {
                throw new ArgumentException("Controller Parameters cannot be null");
            }
        }

        public override void Cancel()
        {
            if (IsRunning && _cancelTokenSource != null)
            {
                _cancelTokenSource.Cancel();
                IsRunning = false;
                OnCompleted(new SorterCompletedEventArgs()
                {
                    OperationStatus = ControllerOperationStatus.Cancelled,
                    Action = SorterAction.Undefined
                });
            }
        }

        private void SortAsync(TabletMagazine mag)
        {
            var ct = _cancelTokenSource.Token;
            Task.Run(() =>
            {
                var status = Sort(mag, ct);
                IsRunning = false;
                OnCompleted(new SorterCompletedEventArgs()
                {
                    OperationStatus = status,
                    Action = SorterAction.Sort
                });
            }, ct);
        }

        private void LoadToConveyorAsync()
        {
            Task.Run(() =>
            {
                var status = LoadToConveyor();
                IsRunning = false;
                OnCompleted(new SorterCompletedEventArgs()
                {
                    OperationStatus = status,
                    Action = SorterAction.LoadToConveyor
                });
            });
        }

        private void LoadToBufferAsync()
        {
            Task.Run(() =>
            {
                var status = LoadToConveyor();
                IsRunning = false;
                OnCompleted(new SorterCompletedEventArgs()
                {
                    OperationStatus = status,
                    Action = SorterAction.LoadToBuffer
                });
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
            _robot.GetTablet(p.X, p.Y, mag.GetSlotIndex(tablet.Color));
            mag.AddTablet(tablet.Color);
        }

        private Point TransformToRobotSpace(PointF p)
        {
            const float xScale = 1.25f;
            const float yScale = 1.286f;
            const float xOff = -121.9f;
            const float yOff = 357.6f;

            float camX = p.X * xScale;
            float camY = p.Y * yScale;

            int robotX = (int)(camY + yOff);
            int robotY = (int)(camX + xOff);

            return new Point(robotX, robotY);
        }
    }

    public class SorterParams : ControllerParams
    {
        public TabletMagazine Magazine;
        public SorterAction Action;
    }

    public class SorterCompletedEventArgs : ControllerEventArgs
    {
        public SorterAction Action;
    }
}
