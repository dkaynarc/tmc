using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core.Sequencing
{
    public interface ISequencer
    {
        OperationMode Mode { get; set; }
        string Name { get; set; }
        void StartSequencing();
        void StopSequencing();
        void FireStartTrigger();
        void FireStopTrigger();
        void FireResumeTrigger();
        void FireShutdownTrigger();

        StateLoggerExtension TransitionLogger { get; }
    }
}
