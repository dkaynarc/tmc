using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
[ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")] // test without the namespace, seems unneccesary.
    public interface IWebService
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
