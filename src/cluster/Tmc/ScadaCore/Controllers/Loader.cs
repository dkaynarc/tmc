using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Tmc.Robotics;

namespace Tmc.Scada.Core
{
    public enum LoaderAction
    {
        LoadToConveyor,
        LoadToAcceptedBuffer,
        LoadToRejectedBuffer
    }

    public sealed class Loader : ControllerBase
    {
        //TODO: Remove this class when the PLC is implemented place.
        private class MockPlc
        {
            public Dictionary<int, bool> TrayMagazine { get; private set; }

            public MockPlc()
            {
                TrayMagazine = new Dictionary<int, bool>();
                for (int i = 0; i < 5; i++)
                {
                    TrayMagazine.Add(i, (i % 2) == 0);
                }
            }
        }

        private LoaderRobot _loaderRobot;
        //private PlcSensor _traySensor;
        private MockPlc _traySensor;
        private Dictionary<LoaderAction, Action> _actionMap;

        public Loader(ClusterConfig config) : base(config)
        {
            _actionMap = new Dictionary<LoaderAction, Action>
            {
                { LoaderAction.LoadToConveyor, () => LoadToConveyorAsync() },
                { LoaderAction.LoadToAcceptedBuffer, () => LoadToAccpetedBufferAsync() },
                { LoaderAction.LoadToRejectedBuffer, () => LoadToRejectedBufferAsync() }
            };

            _loaderRobot = config.Robots[typeof(LoaderRobot)] as LoaderRobot;
            _traySensor = new MockPlc();

            if (_loaderRobot == null)
            {
                throw new ArgumentException("Could not retrieve a LoaderRobot from ClusterConfig");
            }
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as LoaderParams;
            if (p != null)
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    _actionMap[p.Action]();
                }
            }
        }

        public override void Cancel()
        {
        }

        /// <summary>
        /// Attempts to select a shelf from the Tray Magazine
        /// </summary>
        /// <param name="shelfIndex">The selected shelf. -1 if no shelf could be selected</param>
        /// <returns>false if no shelf could be selected</returns>
        private bool TrySelectShelf(out int shelfIndex)
        {
            shelfIndex = -1;
            var availableTrays = _traySensor.TrayMagazine.Where(x => x.Value == true).Select(y => y.Key).ToList();
            if (availableTrays.Count == 0)
            {
                return false;
            }

            shelfIndex = availableTrays[0];
            return true;
        }

        private ControllerOperationStatus LoadTray()
        {
            int shelf;
            var status = ControllerOperationStatus.Succeeded;
            if (TrySelectShelf(out shelf))
            {
                try
                {
                    _loaderRobot.GetTray(shelf);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Write(new LogEntry(ex));
                    status = ControllerOperationStatus.Failed;
                }
            }
            else
            {
                Logger.Instance.Write("No trays available in Tray Magazine", LogType.Warning);
                status = ControllerOperationStatus.Failed;
            }
            return status;
        }

        private ControllerOperationStatus Palletise()
        {
            var status = ControllerOperationStatus.Succeeded;
            try
            {
                _loaderRobot.Palletise();
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(new LogEntry(ex));
                status = ControllerOperationStatus.Failed;
            }
            return status;
        }

        private void LoadToConveyorAsync()
        {
            Task.Run(() =>
                {
                    var status = LoadTray();
                    IsRunning = false;
                    OnCompleted(new ControllerEventArgs() { OperationStatus = status });
                });
        }

        private void LoadToAccpetedBufferAsync()
        {
            Task.Run(() =>
                {
                    var status = Palletise();
                    IsRunning = false;
                    OnCompleted(new ControllerEventArgs() { OperationStatus = status });
                });
        }

        private void LoadToRejectedBufferAsync()
        {
            Task.Run(() =>
                {
                    var status = Palletise();
                    IsRunning = false;
                    OnCompleted(new ControllerEventArgs() { OperationStatus = status });
                });
        }
    }
    public class LoaderParams : ControllerParams
    {
        public LoaderAction Action { get; set; }
    }
}
