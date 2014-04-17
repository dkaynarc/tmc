using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core.Sequencing
{
    public interface ISequencer
    {
        string Name { get; set; }
        bool Enabled { get; }

        void Start();
        void Update();
        void Stop();
    }
}
