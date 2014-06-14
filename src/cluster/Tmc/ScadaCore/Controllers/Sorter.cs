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
        Sort = 0,
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

        private const float XMax = 275;
        private const float YMax = 200;
        private const float YMin = 0;
        private const float XMin = 20;

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
                var status = LoadToBuffer();
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
                Logger.Instance.Write("[Sorter] Loading to conveyor");
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
                Logger.Instance.Write("[Sorter] Loading to buffer");
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
            int shakeRetryAttempts = 0;
            try
            {
                List<Tablet> visibleTablets = null;
                while (!mag.IsFull())
                {
                    if (ct.IsCancellationRequested)
                    {
                        status = ControllerOperationStatus.Cancelled;
                        return status;
                    }

                    visibleTablets = GetVisibleTablets(mag);

                    if (visibleTablets.Count != 0)
                    {
                        var tablet = visibleTablets[0];
                        Logger.Instance.Write(String.Format("[Sorter] Seen ({0}) tablet seen at ({1},{2}) in camera space",
                            tablet.Color, tablet.LocationPoint.X, tablet.LocationPoint.Y));
                        shakeRetryAttempts = 0;

                        PlaceTablet(tablet, mag);
                    }
                    else if ((visibleTablets.Count == 0) && (shakeRetryAttempts < MaxShakeRetryAttempts))
                    {
                        Logger.Instance.Write(String.Format("[Sorter] No tablets found. Shaking (attempt {0} of {1})",
                            shakeRetryAttempts, MaxShakeRetryAttempts));
                        _robot.Shake();
                        shakeRetryAttempts++;
                    }
                    else
                    {
                        Logger.Instance.Write(String.Format("[Sorter] Couldnt fully fill the tablet magazine due to insuffient tablets"));
                        break;
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

        private List<Tablet> GetVisibleTablets(TabletMagazine mag)
        {
            List<Tablet> visibleTablets = _vision.GetVisibleTablets().Where(t => t.Color != TabletColors.Unknown).ToList();

            List<Tablet> updatedVisibleTablets = new List<Tablet>();

            foreach (Tablet tablet in FencedTablets(visibleTablets))
            {
                if(!mag.IsSlotFull(tablet.Color))
                {
                    updatedVisibleTablets.Add(tablet);
                }
            }

            return updatedVisibleTablets;
        }

        private IEnumerable<Tablet> FencedTablets(List<Tablet> visibleTablets)
        {
            var fencedTablets = visibleTablets.Where(t => t.LocationPoint.X > XMin
                                                        && t.LocationPoint.Y > YMin
                                                        && t.LocationPoint.X < XMax
                                                        && t.LocationPoint.Y < YMax);
            return fencedTablets;
        }

        private void PlaceTablet(Tablet tablet, TabletMagazine mag)
        {
            var p = TransformToRobotSpace(tablet.LocationPoint);
            Logger.Instance.Write(String.Format("[Sorter] Picking ({0}) tablet from ({1},{2}) in robot space",
                        tablet.Color, p.X, p.Y));
            _robot.GetTablet(p.X, p.Y, mag.GetSlotIndex(tablet.Color));
            mag.AddTablet(tablet.Color);
        }

        private Point TransformToRobotSpace(PointF p)
        {
            const float xScale = 1f;
            const float yScale = 1f;
            const float xOff = -117.8f;
            const float yOff = 365f;

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
