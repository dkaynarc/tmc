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
using System.Text.RegularExpressions;

namespace Tmc.Scada.App
{
    public partial class ReportControl : UserControl
    {
        #region Class-wide Objects, Enums and Properties

        protected enum ReportType
        {
            Alarm, Cycle, Environment, Order, Production
        }

        private Dictionary<ReportType, string> ReportNameDictionary = new Dictionary<ReportType,string>()
        {
            { ReportType.Alarm, "AlarmReport" },
            { ReportType.Cycle, "CycleReport" },
            { ReportType.Environment, "EnvironmentReport" },
            { ReportType.Order, "OrderReport" },
            { ReportType.Production, "ProductionReport" }
        };

        private Dictionary<ReportType, Delegate> ReportDataSourceMethodsDictionary = new Dictionary<ReportType, Delegate>()
        {
            { ReportType.Alarm, new Func<DateTime, DateTime, List<string>, IEnumerable>(TmcData.ReportController.GetAlarmsReportData) },
            { ReportType.Cycle, new Func<DateTime, DateTime, IEnumerable>(TmcData.ReportController.GetCycleReportData) },
            { ReportType.Environment, new Func<DateTime, DateTime, List<string>, IEnumerable>(TmcData.ReportController.GetEnvironmentReportData) },
            { ReportType.Order, new Func<DateTime, DateTime, string, IEnumerable>(TmcData.ReportController.GetOrderReportData) },
            { ReportType.Production, new Func<DateTime, DateTime, IEnumerable>(TmcData.ReportController.GetProductionReportData) }
        };

        private Dictionary<int, ReportType> ReportGenerationIndexDictionary = new Dictionary<int,ReportType>()
        {
            { 1, ReportType.Alarm},
            { 2, ReportType.Cycle},
            { 3, ReportType.Environment },
            { 4, ReportType.Order },
            { 5, ReportType.Production }
        };

        public DateTime SelectedStartDate
        {
            get { return this.dteStartTime.Value; }
            set { this.dteStartTime.Value = value; }
        }

        public DateTime SelectedEndDate
        {
            get { return this.dteEndTime.Value; }
            set { this.dteEndTime.Value = value; }
        }

        #endregion

        public ReportControl()
        {
            InitializeComponent();
        }

        private void ReportControl_Load(object sender, EventArgs e)
        {
            this.SelectedStartDate = DateTime.Now.AddDays(-1);
            this.SelectedEndDate = DateTime.Now;
            this.PopulateReportComboBox();
            this.InitialiseFilteringPanels();
            this.ClearAllFilteringOptions();
        }

        private void PopulateReportComboBox()
        {
            List<string> reports = new List<string>();
            reports.Add(String.Empty);
            foreach (KeyValuePair<int, ReportType> pair in this.ReportGenerationIndexDictionary)
            {
                reports.Add(pair.Value.ToString() + " Report");
            }

            this.cboReports.DataSource = reports;
        }

        private void CreateReport(ReportType reportType)
        {
            if (!this.DatesWithinValidRange())
            {
                MessageBox.Show("Please ensure the start date is equal or earlier than the end date.");
                return;
            }

            ReportViewerForm reportViewerForm = new ReportViewerForm();
            this.BuildReport(reportType, reportViewerForm, this.SelectedStartDate, this.SelectedEndDate);
            reportViewerForm.Text = Regex.Replace(ReportNameDictionary[reportType], "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
            reportViewerForm.Show();
        }

        private void BuildReport(ReportType reportType, ReportViewerForm reportViewerForm, DateTime startDate, DateTime endDate)
        {
            if (reportViewerForm == null)
            {
                throw new Exception();
            }

            string reportName = ReportNameDictionary[reportType];
            reportViewerForm.ReportViewer.LocalReport.ReportPath = "..\\..\\Reporting\\" + reportName + ".rdlc";
            reportViewerForm.ReportViewer.LocalReport.DataSources.Clear();
            reportViewerForm.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource(reportName + "DataSet", this.GetReportDataSource(reportType, startDate, endDate)));
        }

        private bool DatesWithinValidRange()
        {
            return this.SelectedStartDate <= this.SelectedEndDate;
        }

        private IEnumerable GetReportDataSource(ReportType reportType, DateTime startDate, DateTime endDate)
        {
            //return ReportDataSourceMethodsDictionary[reportType](startDate, endDate);
            return new List<string>().AsEnumerable();
        }

        private void ClearAllFilteringOptions()
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(Panel))
                {
                    control.Visible = false;
                }
            }
        }

        private void InitialiseFilteringPanels()
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(Panel))
                {
                    control.Location = new Point(265, 40);
                    control.Size = new Size(405, 213);
                }
            }
        }

        private void CheckAllListboxItems(ListBox listBox)
        {
            if (listBox.Items.Count <= 0)
            {
                return;
            }

            for (int listBoxIndex = 0; listBoxIndex < listBox.Items.Count; listBoxIndex++)
            {
                listBox.SetSelected(listBoxIndex, true);
            }
        }

        private void cboReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox reportsComboBox = sender as ComboBox;

            this.ClearAllFilteringOptions();
            if (reportsComboBox.SelectedIndex > 0)
            {
                this.ShowFilteringOptions(this.ReportGenerationIndexDictionary[reportsComboBox.SelectedIndex]);
            }
        }

        private void ShowFilteringOptions(ReportType reportType)
        {
            switch (reportType)
            {
                case ReportType.Alarm: this.ShowAlarmReportFilters(); break;
                case ReportType.Environment: this.ShowEnvironmentReportFilters(); break;
                case ReportType.Order: this.ShowOrderReportFilters(); break;
                default: ClearAllFilteringOptions(); break;
            }
        }

        private void ShowEnvironmentReportFilters()
        {
            this.ClearAllFilteringOptions();
            this.pnlEnvironmentReportFilters.Visible = true;
            this.clbEnvironmentSourceFilter.Items.Clear();
            //foreach (string source in TmcData.TmcRepository.GetEnvironmentSourceTypes())
            //{
            //    this.clbEnvironmentSourceFilter.Items.Add(source);
            //}
            this.CheckAllListboxItems(this.clbEnvironmentSourceFilter);
        }

        private void ShowAlarmReportFilters()
        {
            this.ClearAllFilteringOptions();
            this.pnlAlarmReportFilters.Visible = true;
            this.clbAlarmsTypeFilter.Items.Clear();
            //foreach (string type in TmcData.TmcRepository.GetAlarmTypes())
            //{
            //    this.clbEnvironmentSourceFilter.Items.Add(type);
            //}
            this.CheckAllListboxItems(this.clbAlarmsTypeFilter);
        }

        private void ShowOrderReportFilters()
        {
            this.ClearAllFilteringOptions();
            this.pnlOrderReportFilters.Visible = true;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            //this.CreateReport(this.ReportGenerationDictionary[this.cboReports.SelectedIndex]);
        }
    }
}
