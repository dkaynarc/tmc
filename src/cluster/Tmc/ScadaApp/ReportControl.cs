using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Reflection;
using System.Collections;

namespace Tmc.Scada.App
{
    public partial class ReportControl : UserControl
    {
        protected enum ReportType
        {
            Alarm, Cycle, Environment, Last100Orders, Machine, Order, Production
        }

        private Dictionary<ReportType, string> ReportNameDictionary = new Dictionary<ReportType,string>()
        {
            { ReportType.Alarm, "AlarmReport" },
            { ReportType.Cycle, "CycleReport" },
            { ReportType.Environment, "EnvironmentReport" },
            { ReportType.Last100Orders, "LastHundredOrdersAverageCycleTimeReport" },
            { ReportType.Machine, "MachineReport" },
            { ReportType.Order, "OrderReport" },
            { ReportType.Production, "ProductionReport" }
        };

        private Dictionary<ReportType, Func<IEnumerable>> ReportDataSourceDictionary = new Dictionary<ReportType, Func<IEnumerable>>()
        {
            //{ ReportType.Alarm, "AlarmReport" },
            //{ ReportType.Cycle, "CycleReport" },
            { ReportType.Environment, TmcData.ReportController.GetEnvironmentReportData },
            //{ ReportType.Last100Orders, "LastHundredOrdersAverageCycleTimeReport" },
            //{ ReportType.Machine, "MachineReport" },
            //{ ReportType.Order, "OrderReport" },
            //{ ReportType.Production, "ProductionReport" }
        };

        private ReportViewerForm reportViewerForm = new ReportViewerForm();

        public ReportControl()
        {
            InitializeComponent();
        }

        private void btnGenerateEnvironmentReport_Click(object sender, EventArgs e)
        {
            this.BuildReport(ReportType.Environment, reportViewerForm);
            //Not sure what this line is for, there was no semicolon so couldn't build.. 
            //reportViewerForm.Text 
            reportViewerForm.Show();
        }

        //private void BuildEnvironmentReport(ReportViewerForm reportViewerForm)
        //{
            
        //}

        private void BuildReport(ReportType reportType, ReportViewerForm reportViewerForm)
        {
            string reportName = ReportNameDictionary[reportType];
            reportViewerForm.ReportViewer.LocalReport.ReportPath = "..\\..\\Reporting\\" + reportName + ".rdlc";
            reportViewerForm.ReportViewer.LocalReport.DataSources.Clear();
            reportViewerForm.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource(reportName + "DataSet", this.GetReportDataSource(reportType)));
        }

        private IEnumerable GetReportDataSource(ReportType reportType)
        {
            return ReportDataSourceDictionary[reportType]();
        }
    }
}
