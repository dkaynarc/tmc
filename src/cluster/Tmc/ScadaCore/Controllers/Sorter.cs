using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;
using Tmc.Robotics;
using Tmc.Vision;

namespace Tmc.Scada.Core
{
    public sealed class Sorter : ControllerBase
    {
        public int MaxShakeRetryAttempts { get; set; }
        public int ShakeRetryAttempts { get; private set; }
        private SorterVision _vision;
        private SorterRobot _robot;
        public Sorter(ClusterConfig config) : base(config)
        {
            this.MaxShakeRetryAttempts = 1;
            this.ShakeRetryAttempts = 0;
            this._vision = new SorterVision(config.Cameras["SorterCamera"] as Camera);
            this._robot = config.Robots[typeof(SorterRobot)] as SorterRobot;

            if (_vision == null)
            {
                throw new ArgumentException("Could not create SorterVision");
            }
            if (_robot == null)
            {
                throw new ArgumentException("Could not retrieve a SorterRobot from ClusterConfig");
            }
        }

        public override void Begin(ControllerParams parameters)
        {
            var p = parameters as SorterParams;
            if (p != null)
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    SortAsync(p.Magazine);
                }
            }
        }

        private void SortAsync(TabletMagazine mag)
        {
            var task = new Task(() => 
                {
                    Sort(mag);
                    IsRunning = false;
                    OnCompleted(new EventArgs());
                });
            task.Start();
        }

        private void Sort(TabletMagazine mag)
        {
            var visibleTablets = _vision.GetVisibleTablets();
            while (ShakeRetryAttempts < MaxShakeRetryAttempts)
            {
                if (visibleTablets.Count == 0)
                {
                    //_robot.Shake();
                    visibleTablets = _vision.GetVisibleTablets();
                    break;
                }
                ShakeRetryAttempts++;
            }

            if (visibleTablets.Count > 0)
            {
            }
        }

        private void PlaceTablets(List<Tablet> tablets, TabletMagazine mag)
        {

        }
    }

    public class SorterParams : ControllerParams
    {
        public TabletMagazine Magazine;
    }
}
