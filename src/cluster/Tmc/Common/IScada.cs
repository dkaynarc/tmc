using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Tmc.Common
{
    public interface IScada
    {
        [OperationContract]
        bool Initialize();
        [OperationContract]
        void Start();
        [OperationContract]
        void Stop();
        [OperationContract]
        void Resume();
        [OperationContract]
        void EmergencyStop();
        [OperationContract]
        IDictionary<string, HardwareStatus> GetLastHardwareStatuses();
        [OperationContract]
        IList<string> GetAllHardwareNames();
        [OperationContract]
        void SetSpeed(int speed);
    }
}
