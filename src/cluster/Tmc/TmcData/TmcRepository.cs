using System;
using System.Collections.Generic;
using System.Data;
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

        public static IList<EnvironmentLogView> EnvironmentLog()
        {
            return new ICTDEntities().EnvironmentLogViews.ToList();
        }

        //public static IList<EnvironmentLogView> EnvironmentLog(DateTime startTime, DateTime endTime)
        //{
        //    return ReportController.GetEnvironmentReportData(startTime, endTime).
        //}

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

        public static ComponentEventLogView GetLatestAlarm()
        {
            return new ICTDEntities().ComponentEventLogViews.LastOrDefault();
        }
        public static void AddEnvironmentalReading(int sourceID, DateTime timestamp, float reading, string type)
        {
            new ICTDEntities().AddNewEnvironmentLog(timestamp, sourceID, reading, typeID);
        }

        public static void AddEnvironmentalReading(Source source, DateTime timestamp, float reading, EnvironmentType type)
        {
            new ICTDEntities().AddNewEnvironmentLog(timestamp, (int)source, reading, (int)type);
        }

        public static List<string> GetEnvironmentSourceTypes()
        {
            return new ICTDEntities().EnvironmentLogViews.Where(x => !String.IsNullOrWhiteSpace(x.Source)).Select(y => y.Source).Distinct().ToList();
        }

        public static List<string> GetAlarmTypes()
        {
            return new ICTDEntities().ComponentEventLogViews.Where(x => !String.IsNullOrWhiteSpace(x.LogType)).Where(y => y.LogType.Equals("Warning", StringComparison.InvariantCultureIgnoreCase) || y.LogType.Equals("Error", StringComparison.InvariantCultureIgnoreCase) || y.LogType.Equals("Alarm", StringComparison.InvariantCultureIgnoreCase)).Select(z => z.LogType).Distinct().ToList();
        }

        //public static List<string> GetAlarmTypes()
        //{
        //    return new ICTDEntities().ComponentCycleLogViews.Where(x => !String.IsNullOrWhiteSpace(x.
        //}

        public static void AddNewEventLog(DateTime timestamp, string description, int sourceID, int logTypeID)
        {
            new ICTDEntities().AddNewEventLog(timestamp, description, sourceID, logTypeID);
        }
    }
}
