using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private LoaderRobot _loaderRobot;
        //private PlcSensor _traySensor;
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

        private int SelectShelf()
        {
            // TODO: write an algorithm to select the first available tray in the magazine
            // Requires the PLC
            return 0;
        }

        private void LoadToConveyorAsync()
        {
            var task = new Task(() =>
                {
                    var shelf = SelectShelf();
                    _loaderRobot.GetTray(shelf);
                    IsRunning = false;
                    OnCompleted(new EventArgs());
                });
            task.Start();
        }

        private void LoadToAccpetedBufferAsync()
        {
            var task = new Task(() =>
                {
                    _loaderRobot.Palletise();
                    IsRunning = false;
                    OnCompleted(new EventArgs());
                });
            task.Start();
        }

        private void LoadToRejectedBufferAsync()
        {
            var task = new Task(() =>
                {
                    _loaderRobot.Palletise();
                    IsRunning = false;
                    OnCompleted(new EventArgs());
                });
            task.Start();
        }
    }
    public class LoaderParams : ControllerParams
    {
        public LoaderAction Action { get; set; }
    }
}
