using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmcData
{
    public static class TmcRepository
    {
        public static IList<OrderList> OrderInfo()
        {
            return new ICTDEntities().OrderLists.ToList();
        }

        public static void AddNewOrder(int userID, int black, int blue, int red, int green, int white )
        {
            new ICTDEntities().AddNewOrder(userID, black, blue, red, green, white);
        }
    }
}
