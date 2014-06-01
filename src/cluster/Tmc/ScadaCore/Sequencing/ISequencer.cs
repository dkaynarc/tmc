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
        void Start();
        void Stop();
        void Resume();
        void Shutdown();
    }
}
