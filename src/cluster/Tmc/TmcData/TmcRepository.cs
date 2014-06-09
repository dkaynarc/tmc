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
        private static object _lock = new Object();

        public static IList<OrderListView> OrderInfo()
        {
            IList<OrderListView> results;
            lock (_lock)
            {
                results = _entities.OrderListViews.ToList();
            }
            return results;
        }

        public static IList<OrderListView> GetOrdersByStatus(int orderStatus)
        {
            IList<OrderListView> results;
            lock (_lock)
            {
                results = _entities.OrderListViews.Where(e => e.StatusID == orderStatus).ToList(); 
            }
            return results;
        }

        public static IList<EnvironmentLogView> EnvironmentLog()
        {
            IList<EnvironmentLogView> results;
            lock (_lock)
            {
                results = _entities.EnvironmentLogViews.ToList();
            }
            return results;
        }

        public static EnvironmentLogView GetLatestEnvironment(string source)
        {
            EnvironmentLogView results;
            lock (_lock)
            {
                results = _entities.EnvironmentLogViews.Where(e => e.Source.Contains(source)).OrderByDescending(e => e.Timestamp).First();
            }
            return results;
        }

        public static IList<ComponentEventLogView> EventLog()
        {
            IList<ComponentEventLogView> results;
            lock (_lock)
            {
                results = _entities.ComponentEventLogViews.ToList();
            }
            return results;
        }

        public static void AddNewOrder(Guid userID, int black, int blue, int red, int green, int white )
        {
            lock (_lock)
            {
                _entities.AddNewOrder(userID, black, blue, red, green, white);
            }
        }

        public static void CompleteOrder(int orderId)
        {
            lock (_lock)
            {
                _entities.CompleteOrder(orderId);
            }
        }

        public static void UpdateNumberOfProducts(int orderId, int numberOfTrays)
        {
            lock (_lock)
            {
                _entities.CompleteOrder(_entities.UpdateProductProduced(orderId, numberOfTrays));
            }
        }

        public static void UpdateOrderStatus(int orderId, string status)
        {
            lock (_lock)
            {
                _entities.UpdateOrderStatus(orderId, status);
            }
        }

        public static void UpdateOrderStatus(int orderId, int statusId)
        {
            lock (_lock)
            {
                _entities.UpdateOrderStatusByID(orderId, statusId);
            }
        }

        public static void AddEnvironmentalReading(int sourceID, DateTime timestamp, float reading, int typeID)
        {
            lock (_lock)
            {
                _entities.AddNewEnvironmentLog(timestamp, sourceID, reading, typeID);
            }
        }

        public static void AddEnvironmentalReading(Source source, DateTime timestamp, float reading, EnvironmentType type)
        {
            lock (_lock)
            {
                _entities.AddNewEnvironmentLog(timestamp, (int)source, reading, (int)type);
            }
        }

        public static void AddNewEventLog(DateTime timestamp, string description, int sourceID, int logTypeID)
        {
            lock (_lock)
            {
                _entities.AddNewEventLog(timestamp, description, sourceID, logTypeID);
            }
        }

        public static ComponentEventLogView GetLatestAlarm()
        {
            ComponentEventLogView results;
            lock (_lock)
            {
                results = _entities.ComponentEventLogViews.Where(e => e.LogType == LogType.Warning.ToString() || e.LogType == LogType.Error.ToString()).OrderByDescending(e => e.Timestamp).First();
            }
            return results;
        }

        public static IList<ComponentEventLogView> GetAllAlarms()
        {
            IList<ComponentEventLogView> results;
            lock (_lock)
            {
                results = _entities.ComponentEventLogViews.Where(e => e.LogType == LogType.Warning.ToString() || e.LogType == LogType.Error.ToString()).OrderByDescending(e => e.Timestamp).ToList();
            }
            return results;
        }

        public static void CancelOrder(int orderID)
        {
            lock (_lock) 
            {
                _entities.CancelOrder(orderID);
            }
        }

        public static void AddNewEventLog(DateTime date, string message, int logType)
        {
            lock (_lock)
            {
                _entities.AddNewEventLog(date, message, (int)Source.System, logType);
            }
        }
        public static void AcknowledgeEvent(int eventId)
        {
            lock (_lock)
            {
                _entities.AcknowledgeEvent(eventId);
            }
        }

    }
}
