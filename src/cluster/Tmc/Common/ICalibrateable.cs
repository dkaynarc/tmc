using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Common
{
    public interface ICalibrateable
    {
        void Calibrate();
        void Register();
        void Unregister();
    }
}
