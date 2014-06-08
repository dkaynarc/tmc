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
        private static ICTDEntities _entities = new ICTDEntities();

        public static IList<OrderListView> OrderInfo()
        {
            return _entities.OrderListViews.ToList();
        }

        public static IList<OrderListView> GetOrdersByStatus(int orderStatus)
        {
            return _entities.OrderListViews.Where(e => e.StatusID == orderStatus).ToList();
        }

        public static IList<EnvironmentLogView> EnvironmentLog()
        {
            return _entities.EnvironmentLogViews.ToList();
        }

        public static EnvironmentLogView GetLatestEnvironment(string source)
        {   
            return _entities.EnvironmentLogViews.Where(e => e.Source.Contains(source)).OrderByDescending(e => e.Timestamp).First();
        }

        public static IList<ComponentEventLogView> EventLog()
        {
            return _entities.ComponentEventLogViews.ToList();
        }

        public static void AddNewOrder(Guid userID, int black, int blue, int red, int green, int white )
        {
            _entities.AddNewOrder(userID, black, blue, red, green, white);
        }

        public static void CompleteOrder(int orderId)
        {
            _entities.CompleteOrder(orderId);
        }

        public static void UpdateNumberOfProducts(int orderId, int numberOfTrays)
        {
            _entities.UpdateProductProduced(orderId, numberOfTrays);
        }

        public static void UpdateOrderStatus(int orderId, string status)
        {
            _entities.UpdateOrderStatus(orderId, status);
        }

        public static void UpdateOrderStatus(int orderId, int statusId)
        {
            _entities.UpdateOrderStatusByID(orderId, statusId);
        }

        public static void AddEnvironmentalReading(int sourceID, DateTime timestamp, float reading, int typeID)
        {
            _entities.AddNewEnvironmentLog(timestamp, sourceID, reading, typeID);
        }

        public static void AddEnvironmentalReading(Source source, DateTime timestamp, float reading, EnvironmentType type)
        {
            _entities.AddNewEnvironmentLog(timestamp, (int)source, reading, (int)type);
        }

        public static void AddNewEventLog(DateTime timestamp, string description, int sourceID, int logTypeID)
        {
            _entities.AddNewEventLog(timestamp, description, sourceID, logTypeID);
        }

        public static ComponentEventLogView GetLatestAlarm()
        {
            return _entities.ComponentEventLogViews.Where(e => e.LogType == LogType.Warning.ToString() || e.LogType == LogType.Error.ToString()).OrderByDescending(e => e.Timestamp).First();
        }

        public static IList<ComponentEventLogView> GetAllAlarms()
        {
            return _entities.ComponentEventLogViews.Where(e => e.LogType == LogType.Warning.ToString() || e.LogType == LogType.Error.ToString()).OrderByDescending(e => e.Timestamp).ToList();
        }

        public static void CancelOrder(int orderID)
        {
            throw new NotImplementedException();
        }

        public static void AddNewEventLog(DateTime date, string message, int logType)
        {
            _entities.AddNewEventLog(date, message, (int)Source.System, logType);
        }
        public static void AcknowledgeEvent(int eventId)
        {
            _entities.AcknowledgeEvent(eventId);
        }

    }
}
