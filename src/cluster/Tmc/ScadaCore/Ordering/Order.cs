using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public enum OrderStatus
    {
        Pending = 1,
        Assembling = 2,
        Completed = 3,
        Cancelled = 4,
        Open = 5
    }

    public class Order
    {
        public OrderConfiguration Configuration { get; set; }
        public int Id { get; set; }
        public OrderStatus Status { get; set; }

        public Order()
        {
            this.Configuration = new OrderConfiguration();
            this.Status = OrderStatus.Open;
        }
    }
}
