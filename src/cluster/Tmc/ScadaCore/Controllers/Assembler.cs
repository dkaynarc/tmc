using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Robotics;
using System.Threading;
using Tmc.Common;

namespace Tmc.Scada.Core
{
    public enum AssemblerAction
    {
        Assemble,
        GetTabletMagazine,
        ReturnTabletMagazine
    }
    public sealed class Assembler : ControllerBase
    {
        private AssemblerRobot _assemblerRobot;
        private CancellationTokenSource _cancelTokenSource;
        private Dictionary<AssemblerAction, Action<AssemblerParams>> _actionMap;

        public Assembler(ClusterConfig config)
            : base(config)
        {
            _assemblerRobot = config.Robots[typeof(AssemblerRobot)] as AssemblerRobot;
            this._cancelTokenSource = new CancellationTokenSource();

            if (_assemblerRobot == null)
            {
                throw new ArgumentException("Could not retrieve a AssemblerRobot from ClusterConfig");
            }

            _actionMap = new Dictionary<AssemblerAction, Action<AssemblerParams>>()
            {
                { AssemblerAction.Assemble, (x) => AssembleAsync(x.Magazine, x.OrderConfiguration) },
                { AssemblerAction.GetTabletMagazine, (x) => GetTabletMagazineAsync() },
                { AssemblerAction.ReturnTabletMagazine, (x) => ReturnTabletMagazineAsync() }
            };
        }
        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as AssemblerParams;
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
                OnCompleted(new ControllerEventArgs() { OperationStatus = ControllerOperationStatus.Cancelled });
            }
        }

        private bool canCompleteOrder(TabletMagazine mag, OrderConfiguration orderConfiguration)
        {
            foreach (var pair in orderConfiguration.Tablets.Where(x => x.Value > 0))
            {
                if (pair.Value > mag.GetSlotDepth(pair.Key))
                {
                    return false;
                }
            }
            return true;
        }

        private void AssembleAsync(TabletMagazine mag, OrderConfiguration orderConfiguration)
        {
            var ct = _cancelTokenSource.Token;
            if (!canCompleteOrder(mag, orderConfiguration))
            {
                Logger.Instance.Write(new LogEntry("Not enough tablets to complete the order, Please refill tablet magazine"));
                OnCompleted(new AssemblerEventArgs() { OperationStatus = AssemblerOperationStatus.TabletRefill });
                IsRunning = false;
            }
            else
            {
                Task.Run(() =>
                {
                    var status = Assemble(mag, orderConfiguration, ct);
                    IsRunning = false;
                    OnCompleted(new ControllerEventArgs() { OperationStatus = status });
                }, ct);
            }
        }

        private ControllerOperationStatus GetTabletMagazine()
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                _assemblerRobot.GetMagazine();
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(new LogEntry(ex));
                status = ControllerOperationStatus.Failed;
            }
            return status;
        }

        private void GetTabletMagazineAsync()
        {
            Task.Run(() =>
            {
                var status = GetTabletMagazine();
                IsRunning = false;
                OnCompleted(new ControllerEventArgs() { OperationStatus = status });
            });
        }

        private ControllerOperationStatus ReturnTabletMagazine()
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                _assemblerRobot.ReturnMagazine();
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(new LogEntry(ex));
                status = ControllerOperationStatus.Failed;
            }
            return status;
        }

        private void ReturnTabletMagazineAsync()
        {
            Task.Run(() =>
            {
                var status = ReturnTabletMagazine();
                IsRunning = false;
                OnCompleted(new ControllerEventArgs() { OperationStatus = status });
            });
        }

        private ControllerOperationStatus Assemble(TabletMagazine mag, OrderConfiguration orderConfiguration, CancellationToken ct) //TODO Check for tray index outofbound
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                int trayIndex = 0;
                foreach (var pair in orderConfiguration.Tablets.Where(x => x.Value > 0))
                {
                    var numTablets = pair.Value;
                    for (int i = 0; i < numTablets; i++)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            status = ControllerOperationStatus.Cancelled;
                            return status;
                        }
                        else
                        {
                            _assemblerRobot.PlaceTablet(mag.GetSlotIndex(pair.Key), mag.GetSlotDepth(pair.Key), trayIndex); //TODO check chip depth = 10 or 0 when full?
                            trayIndex++;
                        }

                    }
                }
                status = ControllerOperationStatus.Succeeded;
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(new LogEntry(ex));
                status = ControllerOperationStatus.Failed;
            }
            return status;
        }
    }

    public class AssemblerParams : ControllerParams
    {
        public TabletMagazine Magazine;
        public OrderConfiguration OrderConfiguration;
        public AssemblerAction Action { get; set; }
    }

    public enum AssemblerOperationStatus
    {
        TabletRefill
    }

    public class AssemblerEventArgs : ControllerEventArgs
    {
        public AssemblerOperationStatus OperationStatus;
    }
}
