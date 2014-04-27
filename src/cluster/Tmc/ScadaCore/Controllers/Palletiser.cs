using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Robotics;

namespace Tmc.Scada.Core
{
    public sealed class Palletiser : ControllerBase
    {
        private PalletiserRobot _palletiserRobot;
        public Palletiser(ClusterConfig config) : base(config)
        {
            _palletiserRobot = config.Robots[typeof(PalletiserRobot)] as PalletiserRobot;

            if (_palletiserRobot == null)
            {
                throw new ArgumentException("Could not retrieve a PalletiserRobot from ClusterConfig");
            }
        }

        public override void Begin(ControllerParams parameters)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                PalletiseAsync();
            }
        }

        private void PalletiseAsync()
        {
            var task = new Task(() =>
                {
                    _palletiserRobot.Palletise();
                    IsRunning = false;
                    OnCompleted(new EventArgs());
                });
            task.Start();
        }
    }
}
