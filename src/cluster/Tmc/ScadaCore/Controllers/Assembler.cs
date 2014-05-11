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
    public sealed class Assembler : ControllerBase
    {
        private AssemblerRobot _assemblerRobot;
        private CancellationTokenSource _cancelTokenSource;

        public Assembler(ClusterConfig config) : base(config)
        {
            _assemblerRobot = config.Robots[typeof(AssemblerRobot)] as AssemblerRobot;
            this._cancelTokenSource = new CancellationTokenSource();

            if (_assemblerRobot == null)
            {
                throw new ArgumentException("Could not retrieve a AssemblerRobot from ClusterConfig");
            }
        }  
        public override void Begin(ControllerParams parameters) // TODO
        {
            var p = parameters as AssemblerParams;
            if (p != null)
            {
                foreach (var pair in p.OrderConfiguration.Tablets.Where(x => x.Value > 0))
                {
                    if(pair.Value > p.Magazine.GetSlotDepth(pair.Key))
                    {
                        Logger.Instance.Write(new LogEntry("Not enough tablets to complete the order, Please refill tablet magazine"));
                        OnCompleted(new ControllerEventArgs() { OperationStatus = ControllerOperationStatus.Failed });
                    }
                }

                if (!IsRunning)
                {
                    IsRunning = true;
                    AssembleAsync(p.Magazine, p.OrderConfiguration);
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

        private void AssembleAsync(TabletMagazine mag, OrderConfiguration orderConfiguration)
        {
            var ct = _cancelTokenSource.Token;
            Task.Run(() =>
            {
                var status = Assemble(mag, orderConfiguration, ct);
                IsRunning = false;
                OnCompleted(new ControllerEventArgs() { OperationStatus = status });
            }, ct);
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
    }
}
