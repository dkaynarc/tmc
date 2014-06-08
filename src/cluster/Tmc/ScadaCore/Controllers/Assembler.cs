using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Robotics;
using System.Threading;
using Tmc.Common;
using TmcData;

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
        
        public Tray<Tablet> LastOrderTray;
        private AssemblerRobot _assemblerRobot;
        private CancellationTokenSource _cancelTokenSource;
        private Dictionary<AssemblerAction, Action<AssemblerParams>> _actionMap;

        public Assembler(ClusterConfig config) : base(config)
        {
            _assemblerRobot = config.Robots[typeof(AssemblerRobot)] as AssemblerRobot;
            this._cancelTokenSource = new CancellationTokenSource();
            this.LastOrderTray = null;

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
                OnCompleted(new AssemblerEventArgs() { AssemblerOperationStatus = AssemblerOperationStatus.TabletRefill });
                IsRunning = false;
            }
            else
            {
                Task.Run(() =>
                {
                    var status = Assemble(mag, orderConfiguration, ct);
                    IsRunning = false;
                    OnCompleted(new AssemblerEventArgs()
                    {
                        OperationStatus = status,
                        AssemblerOperationStatus = AssemblerOperationStatus.Normal
                    });
                }, ct);
            }
        }

        private ControllerOperationStatus Assemble(TabletMagazine mag, OrderConfiguration orderConfiguration, CancellationToken ct)
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                var tray = MapOrderToTray(orderConfiguration);
                for (int i = 0; i < tray.Cells.Count; i++)
                {
                    var tablet = tray.Cells[i];
                    if (ct.IsCancellationRequested)
                    {
                        status = ControllerOperationStatus.Cancelled;
                        return status;
                    }
                    else
                    {
                        var slotDepth = mag.GetSlotDepth(tablet.Color);
                        var slotIndex = mag.GetSlotIndexReversed(tablet.Color);
                        Logger.Instance.Write(String.Format("[Assembler] Placing ({0}) tablet from slot {1} into slot {2}",
                            tablet.Color, slotIndex, i));
                        _assemblerRobot.PlaceTablet(slotIndex, slotDepth, i);
                    }
                }
                status = ControllerOperationStatus.Succeeded;
                LastOrderTray = tray;
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(new LogEntry(ex));
                status = ControllerOperationStatus.Failed;
            }
            return status;
        }

        private ControllerOperationStatus GetTabletMagazine()
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                Logger.Instance.Write("[Assembler] Getting tablet magazine");
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
                Logger.Instance.Write("[Assembler] Returning tablet magazine");
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

        private Tray<Tablet> MapOrderToTray(OrderConfiguration orderConfig)
        {
            int trayIndex = 0;
            var tray = new Tray<Tablet>();
            var tabletCollection = orderConfig.Tablets.Where(x => x.Value > 0);
            int totalTablets = 0;
            tabletCollection.ToList().ForEach(x => totalTablets += x.Value);

            if (totalTablets > Tray<Tablet>.MaxUsableCells - 1)
            {
                throw new Exception(String.Format("The number of tablets in the order exceeds number of usable tray cells ({0} > {1})",
                                        totalTablets, Tray<Tablet>.MaxUsableCells));
            }            

            foreach (var pair in tabletCollection)
            {
                var numTablets = pair.Value;
                for (int i = 0; i < numTablets; i++)
                {
                    if (trayIndex == Tray<Tablet>.MiddleCellIndex)
                    {
                        trayIndex++;
                    }
                    tray.Cells[i] = new Tablet() { Color = pair.Key };
                    trayIndex++;
                }
            }

            return tray;
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
        TabletRefill,
        Normal
    }

    public class AssemblerEventArgs : ControllerEventArgs
    {
        public AssemblerOperationStatus AssemblerOperationStatus;
        public Tray<Tablet> MappedTray;
    }
}
