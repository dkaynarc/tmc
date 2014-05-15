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
        public static IList<OrderListView> OrderInfo()
        {
            return new ICTDEntities().OrderListViews.ToList();
        }

        public static void AddNewOrder(Guid userID, int black, int blue, int red, int green, int white )
        {
            new ICTDEntities().AddNewOrder(userID, black, blue, red, green, white);
        }

        public static void CompleteOrder(int orderId)
        {
            new ICTDEntities().CompleteOrder(orderId);
        }

        public static void UpdateNumberOfProducts(int orderId, int numberOfTrays)
        {
            new ICTDEntities().UpdateProductProduced(orderId, numberOfTrays);
        }

        public static void UpdateOrderStatus(int orderId, string status)
        {
            new ICTDEntities().UpdateOrderStatus(orderId, status);
        }

        public static void UpdateOrderStatus(int orderId, int statusId)
        {
            new ICTDEntities().UpdateOrderStatusByID(orderId, statusId);
        }

        public static void CancelOrder(int orderId)
        {
            new ICTDEntities().CancelOrder(orderId);
        }
    }
}
