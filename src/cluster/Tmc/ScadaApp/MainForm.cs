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
        private WcfHost _wcfHost;

        public MainForm()
        {
            InitializeComponent();
            Logger.Instance.Strategy = LogStrategy.All;
            this.createUserButton.Hide();
            _scadaEngine = new ScadaEngine();
            this.InitializeAll();
            //Only proceed if SCADA is initialised
            _webApiClient = new WebApiClient("http://192.168.1.102:8080/");
            //disableUserControl(); // Default on startup - user must login first
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var mbResult = MessageBox.Show("Do you want to shut down the cluster?", "Shutdown Confirmation", MessageBoxButtons.YesNo);
            if (mbResult == DialogResult.Yes)
            {
                _scadaEngine.Shutdown();
            }
            base.OnFormClosing(e);
        }

        private bool InitializeScada()
        {
            DialogResult mbResult = DialogResult.Retry;
            bool isInitialized = false;
            while (!isInitialized && mbResult == DialogResult.Retry)
            {
                isInitialized = _scadaEngine.Initialize();
                if (!isInitialized)
                {
                    mbResult = MessageBox.Show("SCADA Engine failed to initialize. {0}", "SCADA Engine Failure", MessageBoxButtons.RetryCancel);
                }
                if (mbResult == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        private void InitializeAll()
        {
            if (this.InitializeScada())
            {
                this.controlPage1.InitialiseScadaEngine(_scadaEngine);
                this.plantMimic1.Initialise(_scadaEngine.HardwareMonitor);
                this.orderControl.Initialise();
                this.debugOverrides.Initialize(_scadaEngine);
                CalibrationManager.Instance.DataFilesDirectory = @".\calibration";
                CalibrationManager.Instance.LoadAllDataFiles();
                _wcfHost = new WcfHost(_scadaEngine);
                _wcfHost.Open();
            }
            else
            {
                this.Load += new EventHandler((s,e) => this.Close());
            }
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
