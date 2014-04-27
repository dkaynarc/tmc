using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tmc.Scada.Core;


namespace Tmc.Scada.App
{
    public partial class MainForm : Form
    {
        ScadaEngine _engine;
        StatusManager _statusManager;
        OrderManager _orderManager;
        ReportManager _reportManager;
        ScadaWCFServer _wcfServer;

        public MainForm()
        {
            InitializeComponent();

            _wcfServer = new ScadaWCFServer();
            _engine = new ScadaEngine();
            _statusManager = new StatusManager(_engine, this);
            _orderManager = new OrderManager(_wcfServer, this);
            _reportManager = new ReportManager(_wcfServer, this);
        }

        private void login(String id, String password)
        {
            //Query DB with parameters, check if valid (Waiting for function names from Denis R)
            _wcfServer.login();
            //Check if system is running
            //If not, prompt user to start the TMC
            startupCheck();
           
            //Set focus to Status page

            this.hiddenTabsControl.SelectedTab = this.statusPage;
        }

        private void startupCheck()
        {
            if (_engine.GetStatus() == "Offline")
            {
                StartPrompt startup = new StartPrompt();
                startup.ShowDialog();

                if (startup.DialogResult == DialogResult.OK)
                {
                    _engine.Initialise();
                    _engine.Start();
                }

                startup.Close();
            }
        }

        private void eStopButton_Click(object sender, EventArgs e)
        {
            _engine.EmergencyStop();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            login(userIDTextBox.Text, passwordTextBox.Text);
        }

        private void stopProductionButton_Click(object sender, EventArgs e)
        {
            _engine.Stop();
        }

        private void resumeProductionButton_Click(object sender, EventArgs e)
        {
            _engine.Resume();
        }

        private void createReportButton_Click(object sender, EventArgs e)
        {
            _reportManager.createReport();
        }

        private void createOrderButton_Click(object sender, EventArgs e)
        {
            _orderManager.createOrder();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            _wcfServer.logout();
        }

        private void orderPage_Click(object sender, EventArgs e)
        {

        }
    }
}
