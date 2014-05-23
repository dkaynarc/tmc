using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Tmc.Scada.Core
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")] // test without the namespace, seems unneccesary.
    public interface IScada
    {
        [OperationContract]
        // TODO: Remove the "add" test function
        double Add(double n1, double n2);
        void Initialise();
        [OperationContract]
        void Start();
        [OperationContract]
        void Stop();
        [OperationContract]
        void Resume();
        [OperationContract]
        void EmergencyStop();
    }
}
