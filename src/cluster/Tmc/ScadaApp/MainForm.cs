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

        public MainForm()
        {
            InitializeComponent();
            //Logger.Instance.Strategy = LogStrategy.File;
            this.createUserButton.Hide();
            //_scadaEngine = new ScadaEngine();
            //this.InitializeAll(_scadaEngine);
            //Only proceed if SCADA is initialised
            _webApiClient = new WebApiClient("http://192.168.1.102:8080/");
            //disableUserControl(); // Default on startup - user must login first
        }

        private void InitializeAll(ScadaEngine engine)
        {
            this.controlPage1.InitialiseScadaEngine(engine);
            this.plantMimic1.Initialise(_scadaEngine.HardwareMonitor);
            this.orderControl.Initialise();
            CalibrationManager.Instance.DataFilesDirectory = @".\calibration";
            CalibrationManager.Instance.LoadAllDataFiles();
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
