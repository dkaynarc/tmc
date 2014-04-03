using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core.Ordering
{
    public class OrderConsumer
    {
        private Queue<Order> _orderQueue;

        public OrderConsumer()
        {
            this._orderQueue = new Queue<Order>();
        }
    }
}
