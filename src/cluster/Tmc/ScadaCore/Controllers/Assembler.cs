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
    //TODO: Refactor so that the Assembler retrieves an order from the OrderConsumer.
    //      The OrderConsumer will be implemented as a singleton. 
    public sealed class Assembler : ControllerBase
    {
        //TODO: Remove this class when the OrderConfiguration is implemented place.
        private class MockOrderConfiguration : OrderConfiguration
        {
            public MockOrderConfiguration()
            {
                this.AddTablet(TabletColors.Green, 2);
                this.AddTablet(TabletColors.White, 1);
                this.AddTablet(TabletColors.Blue, 3);
            }
        }

        private AssemblerRobot _assemblerRobot;
        private MockOrderConfiguration _orderConfiguration;
        private CancellationTokenSource _cancelTokenSource;

        public Assembler(ClusterConfig config) : base(config)
        {
            _assemblerRobot = config.Robots[typeof(AssemblerRobot)] as AssemblerRobot;
            this._cancelTokenSource = new CancellationTokenSource();

            //TODO: Remove this class when the OrderConfiguration is implemented place.
            _orderConfiguration = new MockOrderConfiguration();

            if (_assemblerRobot == null)
            {
                throw new ArgumentException("Could not retrieve a AssemblerRobot from ClusterConfig");
            }
        }  
        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as AssemblerParams;
            if (p != null)
            {
                if (!IsRunning)
                {
                    IsRunning = true;

                    //TODO: Should change to p.Order
                    AssembleAsync(p.Magazine, p.Tray, _orderConfiguration);
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

        private void AssembleAsync(TabletMagazine mag, Tray<Tablet> tray, OrderConfiguration order)
        {
            var ct = _cancelTokenSource.Token;
            Task.Run(() =>
            {
                var status = Assemble(mag, tray, order, ct);
                IsRunning = false;
                OnCompleted(new ControllerEventArgs() { OperationStatus = status });
            }, ct);
        }

        private ControllerOperationStatus Assemble(TabletMagazine mag, Tray<Tablet> tray, OrderConfiguration order, CancellationToken ct)
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                foreach(var pair in order.Tablets.Where(x => x.Value > 0))
                {
                    var numTablets = pair.Value;
                    for (int i = 0; i < 0; i++)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            status = ControllerOperationStatus.Cancelled;
                            return status;
                        }

                        if (mag.IsSlotEmpty(pair.Key))
                        {
                            // TODO handle the magazine being empty
                        }
                        else
                        {
                            PlaceTablet(pair.Key, mag);
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

        private void PlaceTablet(TabletColors tablet, TabletMagazine mag)
        {
            // TODO
        }
    }

    public class AssemblerParams : ControllerParams
    {
        public Tray<Tablet> Tray;
        public TabletMagazine Magazine;
        // public OrderConfiguration Order;
    }
}
