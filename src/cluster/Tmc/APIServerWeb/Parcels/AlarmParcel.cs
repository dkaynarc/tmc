using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServerWeb.Parcels
{
    class AlarmParcel: Parcel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
    }
}
