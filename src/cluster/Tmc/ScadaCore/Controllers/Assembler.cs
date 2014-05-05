using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Robotics;
using System.Threading;

namespace Tmc.Scada.Core
{
    public sealed class Assembler : ControllerBase
    {
        //TODO: Remove this class when the OrderConfiguration is implemented place.
        private class MockOrderConfiguration
        {
            public Dictionary<int, String> OrderConfiguration { get; private set; }

            public MockOrderConfiguration()
            {
                OrderConfiguration = new Dictionary<int, String>();

                OrderConfiguration.Add(0, "Black");
                OrderConfiguration.Add(1, "Green");
                OrderConfiguration.Add(2, "Yellow");
                OrderConfiguration.Add(3, "Red");
                OrderConfiguration.Add(4, "Black");
                OrderConfiguration.Add(5, "Green");
                OrderConfiguration.Add(6, "Yellow");
                OrderConfiguration.Add(7, "Red");
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

                    //TODO: Should change to p.order
                    AssembleAsync(p.Magazine, p.tray, _orderConfiguration);
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

        private void AssembleAsync(TabletMagazine mag, Tray tray, OrderConfiguration order)
        {
            var ct = _cancelTokenSource.Token;
            Task.Run(() =>
            {
                var status = Assemble(mag, tray, order, ct);
                IsRunning = false;
                OnCompleted(new ControllerEventArgs() { OperationStatus = status });
            }, ct);
        }

        private ControllerOperationStatus Assemble(TabletMagazine mag, Tray tray, OrderConfiguration order, CancellationToken ct)
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                foreach(var entry in order)
                {
                    if (ct.IsCancellationRequested)
                    {
                        status = ControllerOperationStatus.Cancelled;
                        return status;
                    }

                    if (mag.IsSlotEmpty(entry.color))
                    {
                        // TODO
                    }

                    PlaceTablet(entry, mag, tray);
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
            // TODO
        }
    }

    public class AssemblerParams : ControllerParams
    {
        public Tray tray;
        public TabletMagazine Magazine;
        // public OrderConfiguration order;
    }
}
