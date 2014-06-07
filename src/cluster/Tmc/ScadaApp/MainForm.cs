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
using Tmc.Common;
using Tmc.Vision;
using Tmc.Scada.App.UserControls;


namespace Tmc.Scada.App
{
    public partial class MainForm : Form
    {
        private ScadaEngine _scadaEngine;
        private WebApiClient _webApiClient;

        /// <summary>
        /// Data table used to store TMC alarms and warnings
        /// </summary>
        private DataTable AlarmsDataTable;

        // Constants for column names in the alarms data table so we don't need to check for spelling every time
        private const string ALARM_LIST_TAB_PAGE_NAME = "tabAlarmList";
        private const string ALARM_GRID_DISMISS_BUTTON_NAME = "alarmDismiss";
        private const string ALARM_GRID_ALARM_NUMBER_COLUMN_NAME = "alarmNumber";
        private const string ALARM_GRID_ALARM_TYPE_COLUMN_NAME = "alarmType";
        private const string ALARM_GRID_ALARM_MESSAGE_COLUMN_NAME = "alarmMessage";
        private const string ALARM_GRID_ALARM_DATETIME_COLUMN_NAME = "alarmDateTime";

        /// <summary>
        /// Timer used for the main form only; used to periodically get alarm updates from SCADA Core
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Tracks the Id value of the last alarm received
        /// </summary>
        private int lastAlarmId;

        /* Authentication code
         * public SCADAUserManager UserManager
           {
               get { return new SCADAUserManager(); }
           }
         * 
         */

        public MainForm()
        {
            InitializeComponent();
            this.createUserButton.Hide();
            _scadaEngine = new ScadaEngine();
            this.InitializeAll(_scadaEngine);
            //Only proceed if SCADA is initialised
            //this.InitialiseAlarmControls();
            _webApiClient = new WebApiClient("http://192.168.1.102:8080/");
            //disableUserControl(); // Default on startup - user must login first
        }

        /// <summary>
        /// Super method to call other setup methods for the alarm controls set
        /// </summary>
        private void InitialiseAlarmControls()
        {
            AlarmsDataTable = new DataTable();
            this.InitialiseAlarmDataTable();
            this.SetGridViewOptions();
        }

        private void InitializeAll(ScadaEngine engine)
        {
            this.controlPage1.InitialiseScadaEngine(engine);
            this.plantMimic1.Initialise(_scadaEngine.ClusterConfig);
            CalibrationManager.Instance.DataFilesDirectory = @".\calibration";
            CalibrationManager.Instance.LoadAllDataFiles();
        }

        /// <summary>
        /// Initialise the timer and its settings
        /// </summary>
        private void InitialiseTimer()
        {
            this.timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
        }

        /// <summary>
        /// Timer tick event - called whenever the time hits the interval value
        /// </summary>
        /// <param name="sender">Timer as an object</param>
        /// <param name="e">The event object</param>
        void timer_Tick(object sender, EventArgs e)
        {
            TmcData.ComponentEventLogView newAlarm = TmcRepository.GetLatestAlarm();
            if (this.AlarmIsNew(newAlarm))
            {
                this.AddAlarmEntryToDataTable(newAlarm.ID, newAlarm.LogType, newAlarm.Description, newAlarm.Timestamp.Value);
            }
        }

        /// <summary>
        /// Checks if the latest event item fetched from the database is a new event 
        /// by checking the fetched event's Id with the latest alarm Id stored in the data table 
        /// </summary>
        /// <param name="newAlarm">The alarm object</param>
        /// <returns>Boolean value stating whether the alarm is new</returns>
        private bool AlarmIsNew(TmcData.ComponentEventLogView newAlarm)
        {
            return this.AlarmsDataTable.Rows[AlarmsDataTable.Rows.Count - 1].Field<int>(ALARM_GRID_ALARM_NUMBER_COLUMN_NAME) < newAlarm.ID;
        }

        /// <summary>
        /// Updates the last alarm Id class variable
        /// </summary>
        /// <param name="alarmId">The new Id value to update to</param>
        private void UpdateLastAlarmId(int alarmId)
        {
            this.lastAlarmId = alarmId;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void orderListBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        #region Entry Point Method for Creating Alarm Notifications

        /// <summary>
        /// Adds new alarm entry to data table which in turn adds to the alarm bar
        /// </summary>
        /// <param name="alarmId">The Id of the new alarm</param>
        /// <param name="alarmType">The type of the new alarm</param>
        /// <param name="alarmDescription">The message of the new alarm</param>
        /// <param name="alarmDateTime">The date and time this alarm was raised</param>
        private void AddAlarmEntryToDataTable(int alarmId, string alarmType, string alarmDescription, DateTime alarmDateTime)
        {
            DataRow alarmEntryRow = this.AlarmsDataTable.NewRow();
            alarmEntryRow[ALARM_GRID_ALARM_NUMBER_COLUMN_NAME] = alarmId;
            alarmEntryRow[ALARM_GRID_ALARM_TYPE_COLUMN_NAME] = alarmType.ToString();
            alarmEntryRow[ALARM_GRID_ALARM_MESSAGE_COLUMN_NAME] = alarmDescription;
            alarmEntryRow[ALARM_GRID_ALARM_DATETIME_COLUMN_NAME] = alarmDateTime;

            this.AlarmsDataTable.Rows.Add(alarmEntryRow);
            this.UpdateLastAlarmId(alarmId);
            this.AddAlarmNotification(alarmType, alarmDescription, alarmDateTime); // add alarm notification to alarm bar on the UI
            this.AddDismissButtonColumnToGridView(); // adds dismiss button for new alarm entry in the grid view control
        }

        #endregion

        #region Alarm Bar Methods

        /// <summary>
        /// Adds a visual alarm notification to the alarm bar on the main form
        /// </summary>
        /// <param name="alarmType">The type of the new alarm</param>
        /// <param name="alarmMessage">The message of the new alarm</param>
        /// <param name="alarmDateTime">The date and time of the new alarm being raised</param>
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

        /// <summary>
        /// Removes alarm notification from the data table, the grid view and the alarm bar
        /// </summary>
        /// <param name="alarmIndex">The alarm index according to the grid view</param>
        private void DismissAlarmNotifcation(int alarmIndex)
        {
            if (this.pnlAlarms.Controls.Count > 0)
            {
                // removes the visual alarm notification fromn the alarm bar
                // loops 3 times to remove 3 labels; the type label, message label and date time label
                for (int i = 0; i < 3; i++)
                {
                    this.pnlAlarms.Controls.RemoveAt((this.pnlAlarms.Controls.Count - 1) - (alarmIndex * 3));
                }

                this.AlarmsDataTable.Rows.RemoveAt(alarmIndex);
                this.RefreshAlarmGridView();
            }
        }

        /// <summary>
        /// Remove all alarm notifications from data table, grid view and alarm bar
        /// </summary>
        private void DismissAllAlarmNotifications()
        {
            this.pnlAlarms.Controls.Clear();
            this.AlarmsDataTable.Clear();
            this.RefreshAlarmGridView();
        }

        #endregion

        #region Alarm Bar Label Generation Methods

        /// <summary>
        /// Generates label to show alarm type on the alarm bar
        /// </summary>
        /// <param name="alarmType">The type of the alarm</param>
        /// <returns>The formatted label object including the alarm type text</returns>
        private Label GenerateAlarmTypeLabel(string alarmType)
        {
            Label alarmNameLabel = new Label();

            alarmNameLabel.Text = alarmType.ToString() + ": ";
            alarmNameLabel.AutoSize = true;
            //alarmNameLabel.ForeColor = this.GetAlarmTypeColor(alarmType);

            return alarmNameLabel;
        }

        /// <summary>
        /// Generates label to show alarm message on the alarm bar
        /// </summary>
        /// <param name="alarmType">The alamr message</param>
        /// <returns>The formatted label object including the alarm message text</returns>
        private Label GenerateAlarmMessageLabel(string alarmMessage)
        {
            Label alarmDescriptionLabel = new Label();

            alarmDescriptionLabel.Text = alarmMessage;
            alarmDescriptionLabel.AutoSize = true;

            return alarmDescriptionLabel;
        }

        /// <summary>
        /// Generates label to show alarm date and time on the alarm bar
        /// </summary>
        /// <param name="alarmType">The date and time of the alarm</param>
        /// <returns>The formatted label object including the alarm date and time text</returns>
        private Label GenerateAlarmDateTimeLabel(DateTime alarmDateTime)
        {
            Label alarmDateTimeLabel = new Label();

            alarmDateTimeLabel.Text = alarmDateTime.ToString("dd/MM/yy HH:mm:ss");
            alarmDateTimeLabel.AutoSize = true;

            return alarmDateTimeLabel;
        }

        #endregion Methods

        #region Alarm Data Table Methods

        /// <summary>
        /// Sets up the alarm data table's columns
        /// </summary>
        private void InitialiseAlarmDataTable()
        {
            this.AlarmsDataTable.Columns.Add(ALARM_GRID_ALARM_NUMBER_COLUMN_NAME, typeof(int));
            this.AlarmsDataTable.Columns.Add(ALARM_GRID_ALARM_TYPE_COLUMN_NAME, typeof(string));
            this.AlarmsDataTable.Columns.Add(ALARM_GRID_ALARM_MESSAGE_COLUMN_NAME, typeof(string));
            this.AlarmsDataTable.Columns.Add(ALARM_GRID_ALARM_DATETIME_COLUMN_NAME, typeof(DateTime));

            this.AlarmsDataTable.Columns[ALARM_GRID_ALARM_NUMBER_COLUMN_NAME].Caption = "Alarm #";
            this.AlarmsDataTable.Columns[ALARM_GRID_ALARM_TYPE_COLUMN_NAME].Caption = "Type";
            this.AlarmsDataTable.Columns[ALARM_GRID_ALARM_MESSAGE_COLUMN_NAME].Caption = "Description";
            this.AlarmsDataTable.Columns[ALARM_GRID_ALARM_DATETIME_COLUMN_NAME].Caption = "Date & Time";
        }

        #endregion

        #region Alarm Gridview Methods

        /// <summary>
        /// Set up alarm grid view options
        /// </summary>
        private void SetGridViewOptions()
        {
            if (this.dgvAlarmsGrid == null || this.dgvAlarmsGrid.DataSource == null)
            {
                return;
            }

            // set grid view to be read only; users cannot interact with it outside of using the dismiss button
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

        /// <summary>
        /// Adds dismiss button column to alarm grid view
        /// </summary>
        private void AddDismissButtonColumnToGridView()
        {
            // if the column already exists, jump out
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

        /// <summary>
        /// Rebind the alarm grid view with the current alarm data table
        /// </summary>
        public void RefreshAlarmGridView()
        {
            this.dgvAlarmsGrid.DataSource = this.AlarmsDataTable;
        }

        #endregion

        /// <summary>
        /// Jump to alarms list page in the tab control
        /// </summary>
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
            this._scadaEngine.EmergencyStop();
        }

        /// <summary>
        /// Event method which triggers every time a different tab in the tab control is clicked
        /// </summary>
        /// <param name="sender">Tab control as an object</param>
        /// <param name="e">Event object</param>
        private void tbcContentsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            // when the user selects a tab page, disable all other user controls in every other tab page
            // this will prevent the timers in those user controls from ticking
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

        /// <summary>
        /// Event method which triggers every time a cell in the alarm grid view is clicked
        /// </summary>
        /// <param name="sender">Grid view as an object</param>
        /// <param name="e">Grid view cell event object</param>
        private void dgvAlarmsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // detects if the dismiss button is clicked
            if (this.dgvAlarmsGrid.Columns[e.ColumnIndex].Name == ALARM_GRID_DISMISS_BUTTON_NAME)
            {
                this.DismissAlarmNotifcation(e.RowIndex);
            }
        }

        private void loginAndLogoutButton_Click(object sender, EventArgs e)
        {
            if (loginAndLogoutButton.Text == "Login")
            {
                login();
            }
            else
            {
                logout();
            }
        }

        private void login()
        {
            new LoginForm(this).Show();
        }

        public void Authenticate(string username, string password)
        {
            var userInfo = _webApiClient.Authenticate(username, password);

            if (userInfo.Result == "success")
            {
                this.currentUserLabel.Text = username;
                this.loginAndLogoutButton.Text = "Logout";
                this.tbcContentsTabControl.Enabled = true;
                if (userInfo.Role == "operator")
                {
                    this.createUserButton.Show(); 
                }
            }
            else
            {
                MessageBox.Show("Invalid credentials");
                new LoginForm(this).Show();
            }
        }

        private void logout()
        {
            this.currentUserLabel.Text = "No current user";
            disableUserControl();
        }

        private void disableUserControl()
        {
            this.tbcContentsTabControl.SelectTab(1); // Set plant mimic tab
            this.tbcContentsTabControl.Enabled = false;
            this.controlPage1.Enabled = false;
            this.btnShowList.Enabled = false;
        }

        private void createUserButton_Click(object sender, EventArgs e)
        {
            new CreateUserForm(_webApiClient).Show();
        }
    }
}
