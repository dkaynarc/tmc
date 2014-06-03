using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmcData.Datasets;

namespace TmcData
{
    public static class ReportController
    {
        /// <summary>
        /// Get envrionment report data from Entity model.
        /// </summary>
        /// <param name="startTime">The start date to get data from. </param>
        /// <param name="endTime">The end date to get data from.</param>
        /// <param name="sourcesToShow">List of sources to filter by.</param>
        /// <returns>The result data table as an IEnumerable.</returns>
        public static IEnumerable GetEnvironmentReportData(DateTime startTime, DateTime endTime, List<string> sourcesToShow)
        {
            ReportDataSet.EnvironmentDataTableDataTable dataTable = new ReportDataSet.EnvironmentDataTableDataTable();
            
            //TEST DATA

            //ReportDataSet.EnvironmentDataTableRow newRow = dataTable.NewEnvironmentDataTableRow();
            //newRow.Sensor = "Humidity";
            //newRow.Reading = 33.3;
            //newRow.Timestamp = DateTime.Now;
            //dataTable.AddEnvironmentDataTableRow(newRow);

            //REAL THING

            // LINQ query to get data from model
            var query = from q in new ICTDEntities().EnvironmentLogViews
                        where q.Timestamp >= startTime && q.Timestamp <= endTime && q.Source.Any(s => sourcesToShow.Contains(q.Source))
                        select new
                        {
                            Source = q.Source,
                            Reading = q.Reading,
                            Timestamp = q.Timestamp
                        };

            // Cycle through query results to put values into data table
            foreach (var view in query)
            {
                ReportDataSet.EnvironmentDataTableRow newRow = dataTable.NewEnvironmentDataTableRow();
                newRow.Timestamp = view.Timestamp;
                newRow.Sensor = view.Source;
                newRow.Reading = view.Reading;
                dataTable.AddEnvironmentDataTableRow(newRow);
            }

            return dataTable.AsEnumerable();
        }

        /// <summary>
        /// Get alarm report data from Entity model.
        /// </summary>
        /// <param name="startTime">The start date to get data from.</param>
        /// <param name="endTime">The end data to get data from.</param>
        /// <param name="typesToShow">List of types to filter by.</param>
        /// <returns>The result data table as an IEnumerable</returns>
        public static IEnumerable GetAlarmsReportData(DateTime startTime, DateTime endTime, List<string> typesToShow)
        {
            ReportDataSet.AlarmDataTableDataTable dataTable = new ReportDataSet.AlarmDataTableDataTable();

            // TEST DATA
            //ReportDataSet.AlarmDataTableRow newRow = dataTable.NewAlarmDataTableRow();
            //newRow.Timestamp = DateTime.Now;
            //newRow.EventType = "Warning";
            //newRow.Description = "Something blew up.";
            //dataTable.AddAlarmDataTableRow(newRow);

            // REAL THING

            // LINQ query to get data from model
            var query = from q in new ICTDEntities().ComponentEventLogViews
                        where q.Timestamp >= startTime && q.Timestamp <= endTime && q.LogType.Any(s => typesToShow.Contains(q.LogType))
                        select new
                        {
                            Name = q.Name,
                            Timestamp = q.Timestamp,
                            Description = q.Description,
                            LogType = q.LogType
                        };

            // Cycle through results to put values into data table
            foreach (var view in query)
            {
                ReportDataSet.AlarmDataTableRow newRow = dataTable.NewAlarmDataTableRow();
                newRow.Timestamp = view.Timestamp.HasValue ? view.Timestamp.Value : DateTime.MinValue;
                newRow.Description = view.Description;
                newRow.EventType = view.LogType;
                dataTable.AddAlarmDataTableRow(newRow);
            }

            return dataTable.AsEnumerable();
        }

        /// <summary>
        /// Get cycle report data from Entity model.
        /// </summary>
        /// <param name="startTime">The start date to get data from. </param>
        /// <param name="endTime">The end date to get data from.</param>
        /// <returns>The result data table as an IEnumerable.</returns>
        public static IEnumerable GetCycleReportData(DateTime startTime, DateTime endTime)
        {
            ReportDataSet.CycleDataTableDataTable dataTable = new ReportDataSet.CycleDataTableDataTable();

            // TEST DATA
            //for (int i = 0; i < 3; i++)
            //{
            //    ReportDataSet.CycleDataTableRow newRow = dataTable.NewCycleDataTableRow();
            //    newRow.Timestamp = DateTime.Now.AddDays(i);
            //    newRow.CycleTime = 50 + i * i;
            //    dataTable.AddCycleDataTableRow(newRow);
            //    newRow = dataTable.NewCycleDataTableRow();
            //    newRow.Timestamp = DateTime.Now.AddDays(i);
            //    newRow.CycleTime = 50 - i * i;
            //    dataTable.AddCycleDataTableRow(newRow);
            //}

            //REAL THING

            // LINQ query to get data from model
            var query = (from q in new ICTDEntities().ComponentCycleLogViews
                        where q.Timestamp >= startTime && q.Timestamp <= endTime
                        orderby q.ID descending
                        select new
                        {
                            Name = q.Name,
                            Timestamp = q.Timestamp,
                            CycleTime = q.CycleTime
                        }).Take(100); // get top 100 results only

            // loop through results to put values in data table
            foreach (var view in query)
            {
                ReportDataSet.CycleDataTableRow newRow = dataTable.NewCycleDataTableRow();
                newRow.Timestamp = view.Timestamp.HasValue ? view.Timestamp.Value : DateTime.MinValue;
                newRow.CycleTime = view.CycleTime.HasValue ? view.CycleTime.Value : 0;
                dataTable.AddCycleDataTableRow(newRow);
            }

            return dataTable.AsEnumerable();
        }

        /// <summary>
        /// Get order report data from Entity model.
        /// </summary>
        /// <param name="startTime">The start date to get data from. </param>
        /// <param name="endTime">The end date to get data from.</param>
        /// <returns>The result data table as an IEnumerable.</returns>
        public static IEnumerable GetOrderReportData(DateTime startTime, DateTime endTime, string orderIdFilter = "")
        {
            ReportDataSet.OrderDataTableDataTable dataTable = new ReportDataSet.OrderDataTableDataTable();

            // TEST DATA
            //Random rng = new Random();
            //for (int i = 0; i < 3; i++)
            //{
            //    ReportDataSet.OrderDataTableRow newRow = dataTable.NewOrderDataTableRow();
            //    newRow.OrderId = i + 1;
            //    newRow.White = rng.Next(1, 10);
            //    newRow.Black = rng.Next(1, 10);
            //    newRow.Red = rng.Next(1, 10);
            //    newRow.Blue = rng.Next(1, 10);
            //    newRow.Green = rng.Next(1, 10);
            //    newRow.StartTime = DateTime.Now.AddMinutes(rng.Next(1, 10) * -1);
            //    newRow.EndTime = DateTime.Now;
            //    dataTable.AddOrderDataTableRow(newRow);
            //}

            // REAL THING

            // LINQ query to get data from model
            var query = from q in new ICTDEntities().OrderListViews
                        where q.StartTime >= startTime && q.EndTime <= endTime
                        select new
                        {
                            OrderId = q.OrderID,
                            White = q.White,
                            Black = q.Black,
                            Green = q.Green,
                            Red = q.Red,
                            Blue = q.Blue,
                            StartTime = q.StartTime,
                            EndTime = q.EndTime
                        };

            // get order from OrderId filter if applicable
            int id;
            if (!String.IsNullOrWhiteSpace(orderIdFilter) && Int32.TryParse(orderIdFilter, out id))
            {
                query.FirstOrDefault(x => x.OrderId == id);
            }

            // loop through results to put values in data table
            foreach (var view in query)
            {
                ReportDataSet.OrderDataTableRow newRow = dataTable.NewOrderDataTableRow();
                newRow.OrderId = view.OrderId;
                newRow.White = view.White;
                newRow.Black = view.Black;
                newRow.Green = view.Green;
                newRow.Red = view.Red;
                newRow.Blue = view.Blue;
                newRow.StartTime = view.StartTime.HasValue ? view.StartTime.Value : DateTime.MinValue;
                newRow.EndTime = view.EndTime.HasValue ? view.EndTime.Value : DateTime.MinValue;
                dataTable.AddOrderDataTableRow(newRow);
            }

            return dataTable.AsEnumerable();
        }

        /// <summary>
        /// Get production report data from Entity model.
        /// </summary>
        /// <param name="startTime">The start date to get data from. </param>
        /// <param name="endTime">The end date to get data from.</param>
        /// <returns>The result data table as an IEnumerable.</returns>
        public static IEnumerable GetProductionReportData(DateTime startDate, DateTime endDate)
        {
            ReportDataSet.ProductionDataTableDataTable dataTable = new ReportDataSet.ProductionDataTableDataTable();

            // TEST DATA

            // REAL THING

            // LINQ query to get data from model
            var query = from q in new ICTDEntities().OrderListViews
                        where q.StartTime >= startDate && q.EndTime <= endDate
                        select new
                        {
                            StartTime = q.StartTime,
                            EndTime = q.EndTime,
                            ProductsProduced = q.NumberOfProducts
                        };

            // loop through results to put values in data table
            foreach (var view in query)
            {
                ReportDataSet.ProductionDataTableRow newRow = dataTable.NewProductionDataTableRow();
                newRow.ShiftStartTime = view.StartTime.HasValue ? view.StartTime.Value : DateTime.MinValue;
                newRow.ShiftEndTime = view.EndTime.HasValue ? view.EndTime.Value : DateTime.MinValue;
                newRow.ProductsProducedForShift = view.ProductsProduced.HasValue ? view.ProductsProduced.Value : 0;
                dataTable.AddProductionDataTableRow(newRow);
            }

            return dataTable.AsEnumerable();
        }
    }
}
