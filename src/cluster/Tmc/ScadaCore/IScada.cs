using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Tmc.Scada.Core
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IScada
    {
        [OperationContract]
        double Add(double n1, double n2);
        //void Initialise();
        //void Start();
        //void Stop();
        //void Resume();
        //void EmergencyStop();
    }
}
