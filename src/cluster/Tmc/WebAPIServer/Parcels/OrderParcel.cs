using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiServer.Parcels
{

    public class OrderParcel : Parcel
    {
        public int mOrderId { get; set; }
        public string mOrderOwner { get; set; }
        public string mOrderStatus { get; set; }
        public int black { get; set; }
        public int blue { get; set; }
        public int green { get; set; }
        public int red { get; set; }
        public int white { get; set; }
    }
}

