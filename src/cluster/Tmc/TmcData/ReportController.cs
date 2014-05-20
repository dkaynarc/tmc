using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmcData.Datasets;

namespace TmcData
{
    static class ReportController
    {
        public static ReportDataSet.EnvironmentDataTableDataTable GetEnvironmentReportData()
        {
            ReportDataSet.EnvironmentDataTableDataTable dataTable = new ReportDataSet.EnvironmentDataTableDataTable();
            var query = (from q in new ICTDEntities().EnvironmentLogViews.AsEnumerable()
                        select new
                        {
                            Source = q.Source,
                            Reading = q.Reading,
                            Timestamp = q.Timestamp
                        }).AsEnumerable();

            ReportDataSet.EnvironmentDataTableRow newRow = dataTable.NewEnvironmentDataTableRow();

            //TESTING LIEK A BAUS
            newRow.Sensor = "Humidity";
            newRow.Reading = 33.3;
            newRow.Timestamp = DateTime.Now;

            //REAL THING
            //newRow.Sensor = query.Source;
            //newRow.Reading = query.Reading;
            //newRow.Timestamp = query.Timestamp;

            return dataTable;
        }
    }
}
