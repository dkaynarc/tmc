﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Scada.Core.Sequencing;
using Tmc.Scada.Core.Reporting;
using System.ServiceModel;

namespace Tmc.Scada.Core
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class ScadaEngine : IScada
    {
        public ClusterConfig ClusterConfig { get; set; }
        private ISequencer _sequencer;
        private HardwareMonitor _hardwareMonitor;
        private EnvironmentMonitor _environmentMonitor;

        public ScadaEngine()
        {
            this._sequencer = new FSMSequencer(this);
            this._hardwareMonitor = new HardwareMonitor(this);
            this._environmentMonitor = new EnvironmentMonitor(this);
        }

        private static double i = 0;

        public double Add(double n1, double n2)
        {
            i += n1 + n2;
            return i;
        }
    }
}
