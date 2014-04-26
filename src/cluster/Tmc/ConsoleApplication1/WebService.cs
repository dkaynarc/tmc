using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    class WebService : IWebService
    {
        ScadaClient scadaClient = new ScadaClient();

        public double Add(double n1, double n2)
        {
            return scadaClient.Add(n1, n2);
        }
    }
}
