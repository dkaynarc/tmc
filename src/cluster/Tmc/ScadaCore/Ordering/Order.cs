using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core.Ordering
{
    public class Order
    {
        public OrderConfiguration Configuration { get; set; }

        public string Id { get; set; }


        public Order()
        {
            this.Configuration = new OrderConfiguration();
        }
    }
}
