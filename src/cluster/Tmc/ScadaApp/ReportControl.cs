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

        /// <summary>
        /// Enum to indicate the different types of reports that can be produced
        /// </summary>
        protected enum ReportType
        {
            Alarm, Cycle, Environment, Order, Production
        }

        /// <summary>
        /// A dictionary used to produce a 'report string' based on the report type
        /// This is primarily used in place of constants to ensure no 'magical strings' are used
        /// </summary>
        private Dictionary<ReportType, string> ReportNameDictionary = new Dictionary<ReportType,string>()
        {
            { ReportType.Alarm, "AlarmReport" },
            { ReportType.Cycle, "CycleReport" },
            { ReportType.Environment, "EnvironmentReport" },
            { ReportType.Order, "OrderReport" },
            { ReportType.Production, "ProductionReport" }
        };

        /// <summary>
        /// A dictionary to determine report type based on the selection index of the report combo box
        /// </summary>
        private Dictionary<int, ReportType> ReportGenerationIndexDictionary = new Dictionary<int,ReportType>()
        {
            { 1, ReportType.Alarm},
            { 2, ReportType.Cycle},
            { 3, ReportType.Environment },
            { 4, ReportType.Order },
            { 5, ReportType.Production }
        };

        /// <summary>
        /// Get or sets the date of the start date picker control
        /// </summary>
        public DateTime SelectedStartDate
        {
            get { return this.dteStartTime.Value; }
            set { this.dteStartTime.Value = value; }
        }

        /// <summary>
        /// Get or set the date of the end date picker control
        /// </summary>
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

        /// <summary>
        /// Event method called when this control is loaded
        /// </summary>
        /// <param name="sender">This control as an object</param>
        /// <param name="e">Event object</param>
        private void ReportControl_Load(object sender, EventArgs e)
        {
            // set date picker controls and call various set up methods
            this.SelectedStartDate = DateTime.Now.AddDays(-1);
            this.SelectedEndDate = DateTime.Now;
            this.PopulateReportComboBox();
            this.InitialiseFilteringPanels();
            this.ClearAllFilteringOptions();
        }

        /// <summary>
        /// Populates the report selection combo box using the report index dictionary
        /// </summary>
        private void PopulateReportComboBox()
        {
            List<string> reports = new List<string>();

            // adds an empty item as first item in the combo box
            reports.Add(String.Empty);

            foreach (KeyValuePair<int, ReportType> pair in this.ReportGenerationIndexDictionary)
            {
                reports.Add(pair.Value.ToString() + " Report");
            }

            this.cboReports.DataSource = reports;
        }

        /// <summary>
        /// Creates and displays a report based on selected report type
        /// </summary>
        /// <param name="reportType">The type of report to create</param>
        private void CreateReport(ReportType reportType)
        {
            // check if start and end dates are valid
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

        /// <summary>
        /// Builds a report by using query results
        /// </summary>
        /// <param name="reportType">The type of report to build</param>
        /// <param name="reportViewerForm">The form used to show the report</param>
        /// <param name="startDate">The start date to get data from</param>
        /// <param name="endDate">The end date to get data till</param>
        private void BuildReport(ReportType reportType, ReportViewerForm reportViewerForm, DateTime startDate, DateTime endDate)
        {
            if (reportViewerForm == null)
            {
                throw new Exception();
            }

            string reportName = ReportNameDictionary[reportType]; // get report name from dicitionary
            reportViewerForm.ReportViewer.LocalReport.ReportPath = "Reporting\\" + reportName + ".rdlc"; // get report template file
            reportViewerForm.ReportViewer.LocalReport.DataSources.Clear();
            reportViewerForm.ReportViewer.LocalReport.DataSources.Add(new ReportDataSource(reportName + "DataSet", this.GetReportDataSource(reportType, startDate, endDate)));
        }

        /// <summary>
        /// Get report data sources from entity model
        /// </summary>
        /// <param name="reportType">The type of report</param>
        /// <param name="startTime">The start time to get data from</param>
        /// <param name="endTime">The end time to get data till</param>
        /// <returns>The query result (data source) as an IEnumerable</returns>
        private IEnumerable GetReportDataSource(ReportType reportType, DateTime startTime, DateTime endTime)
        {
            switch (reportType)
            {
                case ReportType.Alarm: return TmcData.ReportController.GetAlarmsReportData(startTime, endTime, this.GetFilterOptionsList(reportType));
                case ReportType.Cycle: return TmcData.ReportController.GetCycleReportData(startTime, endTime);
                case ReportType.Environment: return TmcData.ReportController.GetEnvironmentReportData(startTime, endTime, this.GetFilterOptionsList(reportType));
                case ReportType.Order: return TmcData.ReportController.GetOrderReportData(startTime, endTime, this.txtOrderIdFilter.Text);
                case ReportType.Production: return TmcData.ReportController.GetProductionReportData(startTime, endTime);
                default: throw new Exception("Selected report does not exist.");
            };
        }

        /// <summary>
        /// Gets the filter options in the form of selected checkbox values from the check list box
        /// </summary>
        /// <param name="reportType">The type of report</param>
        /// <returns>A list of checked options</returns>
        private List<string> GetFilterOptionsList(ReportType reportType)
        {
            switch (reportType)
            {
                case ReportType.Alarm: return this.GetCheckedBoxListValues(this.clbAlarmsTypeFilter);
                case ReportType.Environment: return this.GetCheckedBoxListValues(this.clbEnvironmentSourceFilter);
                default: throw new Exception("Report type not supported.");
            }
        }

        /// <summary>
        /// Gets a list of selected values in a check list box
        /// </summary>
        /// <param name="checkedListBox">The check list box to get values from</param>
        /// <returns>A list of checked options</returns>
        private List<string> GetCheckedBoxListValues(CheckedListBox checkedListBox)
        {
            List<string> values = new List<string>();
            foreach (var item in checkedListBox.CheckedItems)
            {
                values.Add(item.ToString());
            }

            return values;
        }

        /// <summary>
        /// Checks if the selected start date is earlier than the selected end date
        /// </summary>
        /// <returns>Boolean value determining whether that is the case</returns>
        private bool DatesWithinValidRange()
        {
            return this.SelectedStartDate <= this.SelectedEndDate;
        }

        /// <summary>
        /// Hide all filtering options
        /// </summary>
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

        /// <summary>
        /// Set the filtering option panels in the correct place and size
        /// </summary>
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

        /// <summary>
        /// Check all items in a checked list box
        /// </summary>
        /// <param name="listBox">The checked list box to modify</param>
        private void CheckAllListboxItems(CheckedListBox listBox)
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

        /// <summary>
        /// Event called every time the value of the report selection combo box is changed
        /// </summary>
        /// <param name="sender">The combo box as an object</param>
        /// <param name="e">Event object</param>
        private void cboReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox reportsComboBox = sender as ComboBox;

            this.ClearAllFilteringOptions();

            // show filtering options based on selection
            if (reportsComboBox.SelectedIndex > 0)
            {
                this.ShowFilteringOptions(this.ReportGenerationIndexDictionary[reportsComboBox.SelectedIndex]);
            }
        }

        /// <summary>
        /// Show filtering options based on selected report type
        /// </summary>
        /// <param name="reportType">The selected report type</param>
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

        /// <summary>
        /// Show the filtering options panel for environment reports
        /// </summary>
        private void ShowEnvironmentReportFilters()
        {
            this.ClearAllFilteringOptions();
            this.pnlEnvironmentReportFilters.Visible = true;
            //this.clbEnvironmentSourceFilter.Items.Clear();
            //foreach (string source in TmcData.TmcRepository.GetEnvironmentSourceTypes())
            //{
            //    this.clbEnvironmentSourceFilter.Items.Add(source);
            //}


            this.CheckAllListboxItems(this.clbEnvironmentSourceFilter);
        }

        /// <summary>
        /// Show the filtering options panel for alarm reports
        /// </summary>
        private void ShowAlarmReportFilters()
        {
            this.ClearAllFilteringOptions();
            this.pnlAlarmReportFilters.Visible = true;
            //this.clbAlarmsTypeFilter.Items.Clear();
            //foreach (string type in TmcData.TmcRepository.GetAlarmTypes())
            //{
            //    this.clbEnvironmentSourceFilter.Items.Add(type);
            //}
            this.CheckAllListboxItems(this.clbAlarmsTypeFilter);
        }

        /// <summary>
        /// Show the filtering options panel for order reports
        /// </summary>
        private void ShowOrderReportFilters()
        {
            this.ClearAllFilteringOptions();
            this.pnlOrderReportFilters.Visible = true;
        }

        /// <summary>
        /// Event method which is called every time the generate report button is clicked
        /// </summary>
        /// <param name="sender">The button as an object</param>
        /// <param name="e">Event object</param>
        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            this.CreateReport(this.ReportGenerationIndexDictionary[this.cboReports.SelectedIndex]);
        }
    }
}
