using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    enum EventType
    {
        Notification, Alarm, Error
    }

    class Event
    {
        public EventType EventType { get; private set; }
    }
}
