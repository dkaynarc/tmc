using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Scada.Core.Sequencing;

namespace Tmc.Scada.Core
{
    public class ScadaEngine
    {
        public ClusterConfig ClusterConfig { get; set; }
        private ISequencer _sequencer;

        public ScadaEngine()
        {
            this._sequencer = new FSMSequencer(this);
        }
    }
}
