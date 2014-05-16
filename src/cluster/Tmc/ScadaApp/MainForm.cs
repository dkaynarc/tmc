using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmcData;
using Tmc.Scada.Core;


namespace Tmc.Scada.App
{
    public partial class MainForm : Form
    {
        //private ScadaEngine scadaEngine = new ScadaEngine();
        private DataTable AlarmsDataTable = new DataTable();
        private static int AlarmCount = 1;
        private const string ALARM_LIST_TAB_PAGE_NAME = "tabAlarmList";
        private const string ALARM_GRID_DISMISS_BUTTON_NAME = "alarmDismiss";
        private const string ALARM_GRID_ALARM_NUMBER_COLUMN_NAME = "alarmNumber";
        private const string ALARM_GRID_ALARM_TYPE_COLUMN_NAME = "alarmType";
        private const string ALARM_GRID_ALARM_MESSAGE_COLUMN_NAME = "alarmMessage";
        private const string ALARM_GRID_ALARM_DATETIME_COLUMN_NAME = "alarmDateTime";

        public MainForm()
        {
            InitializeComponent();
            //Initialise SCADA
        }

        

        private void plantMimicScreenButton_Click(object sender, EventArgs e)
        {
            this.tablessControlPanel.SelectedTab = this.plantMimicTab;
        }

        private void controlTabButton_Click(object sender, EventArgs e)
        {
            this.tablessControlPanel.SelectedTab = this.controlTab;
        }

        private void environmentTabButton_Click(object sender, EventArgs e)
        {
            this.tablessControlPanel.SelectedTab = this.environmentTab;
        }

        private void ordersTabButton_Click(object sender, EventArgs e)
        {
            this.tablessControlPanel.SelectedTab = this.ordersTab;
        }

        private void reportsTabButton_Click(object sender, EventArgs e)
        {
            this.tablessControlPanel.SelectedTab = this.reportsTab;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void orderListBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }


        #region Alarm Bar Methods

        private void AddAlarmNotification(string alarmType, string alarmMessage, DateTime alarmDateTime)
        {
            Label alarmTypeLabel = this.GenerateAlarmTypeLabel(alarmType);
            Label alarmMessageLabel = this.GenerateAlarmMessageLabel(alarmMessage);
            Label alarmDateTimeLabel = this.GenerateAlarmDateTimeLabel(alarmDateTime);

            this.pnlAlarms.Controls.Add(alarmTypeLabel);
            this.pnlAlarms.Controls.SetChildIndex(alarmTypeLabel, 0);

            this.pnlAlarms.Controls.Add(alarmMessageLabel);
            this.pnlAlarms.Controls.SetChildIndex(alarmMessageLabel, 1);

            this.pnlAlarms.Controls.Add(alarmDateTimeLabel);
            this.pnlAlarms.Controls.SetChildIndex(alarmDateTimeLabel, 2);
        }

        private void DismissAlarmNotifcation(int alarmIndex)
        {
            if (this.pnlAlarms.Controls.Count > 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    this.pnlAlarms.Controls.RemoveAt((this.pnlAlarms.Controls.Count - 1) - (alarmIndex * 3));
                }

                this.AlarmsDataTable.Rows.RemoveAt(alarmIndex);
                this.dgvAlarmsGrid.DataSource = this.AlarmsDataTable;
            }
        }

        private void DismissAllAlarmNotifications()
        {
            this.pnlAlarms.Controls.Clear();
            this.AlarmsDataTable.Clear();
            this.dgvAlarmsGrid.DataSource = this.AlarmsDataTable;
        }

        #endregion

        #region Alarm Bar Label Generation Methods

        private Label GenerateAlarmTypeLabel(string alarmType)
        {
            Label alarmNameLabel = new Label();

            alarmNameLabel.Text = alarmType.ToString() + ": ";
            alarmNameLabel.AutoSize = true;
            //alarmNameLabel.ForeColor = this.GetAlarmTypeColor(alarmType);

            return alarmNameLabel;
        }

        private Label GenerateAlarmMessageLabel(string alarmMessage)
        {
            Label alarmDescriptionLabel = new Label();

            alarmDescriptionLabel.Text = alarmMessage;
            alarmDescriptionLabel.AutoSize = true;

            return alarmDescriptionLabel;
        }

        private Label GenerateAlarmDateTimeLabel(DateTime alarmDateTime)
        {
            Label alarmDateTimeLabel = new Label();

            alarmDateTimeLabel.Text = alarmDateTime.ToString("dd/MM/yy HH:mm:ss");
            alarmDateTimeLabel.AutoSize = true;

            return alarmDateTimeLabel;
        }

        #endregion Methods

        #region Alarm Data Table Methods

        private void InitialiseAlarmDataTable()
        {
            this.AlarmsDataTable.Columns.Add("alarmNumber", typeof(int));
            this.AlarmsDataTable.Columns.Add("alarmType", typeof(string));
            this.AlarmsDataTable.Columns.Add("alarmDescription", typeof(string));
            this.AlarmsDataTable.Columns.Add("alarmDateTime", typeof(DateTime));

            this.AlarmsDataTable.Columns["alarmNumber"].Caption = "Alarm #";
            this.AlarmsDataTable.Columns["alarmType"].Caption = "Type";
            this.AlarmsDataTable.Columns["alarmDescription"].Caption = "Description";
            this.AlarmsDataTable.Columns["alarmDateTime"].Caption = "Date & Time";
        }

        private void AddAlarmEntryToDataTable(string alarmType, string alarmDescription, DateTime alarmDateTime)
        {
            DataRow alarmEntryRow = this.AlarmsDataTable.NewRow();
            alarmEntryRow["alarmNumber"] = AlarmCount;
            alarmEntryRow["alarmType"] = alarmType.ToString();
            alarmEntryRow["alarmDescription"] = alarmDescription;
            alarmEntryRow["alarmDateTime"] = alarmDateTime;

            this.AlarmsDataTable.Rows.Add(alarmEntryRow);
            AlarmCount++;
        }

        #endregion

        #region Alarm Gridview Methods

        private void SetGridViewOptions()
        {
            if (this.dgvAlarmsGrid.DataSource == null)
            {
                return;
            }

            this.dgvAlarmsGrid.ReadOnly = true;
            this.dgvAlarmsGrid.AllowUserToAddRows = false;
            this.dgvAlarmsGrid.AllowUserToDeleteRows = false;
            this.dgvAlarmsGrid.AllowUserToOrderColumns = false;

            this.dgvAlarmsGrid.Columns[ALARM_GRID_ALARM_NUMBER_COLUMN_NAME].HeaderText = "Alarm #";
            this.dgvAlarmsGrid.Columns[ALARM_GRID_ALARM_TYPE_COLUMN_NAME].HeaderText = "Type";
            this.dgvAlarmsGrid.Columns[ALARM_GRID_ALARM_MESSAGE_COLUMN_NAME].HeaderText = "Message";
            this.dgvAlarmsGrid.Columns[ALARM_GRID_ALARM_MESSAGE_COLUMN_NAME].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvAlarmsGrid.Columns[ALARM_GRID_ALARM_DATETIME_COLUMN_NAME].HeaderText = "Date & Time";
            this.dgvAlarmsGrid.Columns[ALARM_GRID_ALARM_DATETIME_COLUMN_NAME].DefaultCellStyle.Format = "dd/MM/yy HH:mm:ss";
        }

        private void AddDismissButtonColumnToGridView()
        {
            if (this.dgvAlarmsGrid.Columns.Contains(ALARM_GRID_DISMISS_BUTTON_NAME))
            {
                return;
            }

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.UseColumnTextForButtonValue = true;
            buttonColumn.Text = "Dismiss";
            buttonColumn.Name = ALARM_GRID_DISMISS_BUTTON_NAME;
            buttonColumn.HeaderText = "";
            this.dgvAlarmsGrid.Columns.Add(buttonColumn);
        }

        public void RefreshAlarmGridView()
        {
            this.dgvAlarmsGrid.DataSource = this.AlarmsDataTable;
        }

        #endregion

        private void GoToAlarmsListTabPage()
        {
            this.tbcContentsTabControl.SelectTab(ALARM_LIST_TAB_PAGE_NAME);
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            this.GoToAlarmsListTabPage();
        }

        private void btnDismissAll_Click(object sender, EventArgs e)
        {
            this.DismissAllAlarmNotifications();
        }

        private void eStopButton_Click(object sender, EventArgs e)
        {
            //this.scadaEngine.EmergencyStop();
        }

        private void tbcContentsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            for (int tabPageIndex = 0; tabPageIndex < tabControl.TabPages.Count; tabPageIndex++)
            {
                foreach (Control control in tabControl.TabPages[tabPageIndex].Controls)
                {
                    if (control.GetType().BaseType == typeof(UserControl))
                    {
                        control.Enabled = (tabPageIndex != tabControl.SelectedIndex) ? false : true;
                    }
                }
            }
        }
    }
}
