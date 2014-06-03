using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.Core
{
    public enum OrderStatus
    {
        Completed,
        Pending,
        Assembling,
        Open
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
