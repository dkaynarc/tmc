using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Tmc.Robotics;
using Tmc.Sensors;
using TmcData;

namespace Tmc.Scada.Core
{
    public enum LoaderAction
    {
        LoadToConveyor,
        LoadToPalletiser
    }

    public sealed class Loader : ControllerBase
    {
        private LoaderRobot _loaderRobot;
        private IPlc _traySensor;
        private Dictionary<LoaderAction, Action> _actionMap;
        private Dictionary<PlcAttachedSwitch, int> _traySwitchMap;

        public Loader(ClusterConfig config) : base(config)
        {
            _actionMap = new Dictionary<LoaderAction, Action>
            {
                { LoaderAction.LoadToConveyor, () => LoadToConveyorAsync() },
                { LoaderAction.LoadToPalletiser, () => LoadToPalletiserAsync() }
            };

            _loaderRobot = config.Robots[typeof(LoaderRobot)] as LoaderRobot;
            _traySensor = config.Plcs["MainPlc"] as IPlc;
            _traySwitchMap = CreateTraySwitchMap();

            if (_loaderRobot == null)
            {
                throw new ArgumentException("Could not retrieve a LoaderRobot from ClusterConfig");
            }
            if (_traySensor == null)
            {
                throw new ArgumentException("Could not retrieve a Plc from ClusterConfig");
            }
        }

        private Dictionary<PlcAttachedSwitch, int> CreateTraySwitchMap()
        {
            return new Dictionary<PlcAttachedSwitch, int>
            {
                { PlcAttachedSwitch.Tray1, 1 },
                { PlcAttachedSwitch.Tray2, 2 },
                { PlcAttachedSwitch.Tray3, 3 },
                { PlcAttachedSwitch.Tray4, 4 },
                { PlcAttachedSwitch.Tray5, 5 },
                { PlcAttachedSwitch.Tray6, 6 }
            };
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
            var switchStates = _traySensor.GetSwitchStates();
            var availableTrays = switchStates.Where(x => x.Value == true && x.Key.ToString().Contains("Tray"))
                                                .Select(y => y.Key).ToList();

            if (availableTrays.Count == 0)
            {
                return false;
            }

            shelfIndex = _traySwitchMap[availableTrays[0]];
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
                    Logger.Instance.Write(String.Format("[Loader] Getting tray {0}", shelf));
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
                Logger.Instance.Write("[Loader] Palletising");
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

        private void LoadToPalletiserAsync()
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
