using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tmc.Scada.Core;
using Tmc.Scada.Core.Sequencing;

namespace Tmc.Scada.App
{
    public partial class controlPage : UserControl
    {
        private ScadaEngine _scadaEngine;

        public controlPage()
        {
            InitializeComponent();
            this.normalModeRadioButton.BackColor = Color.Green;
        }

        /// <summary>
        /// Called by main form to pass in access to the scada engine
        /// </summary>
        /// <param name="scadaEngine"></param>
        public void InitialiseScadaEngine(ScadaEngine scadaEngine)
        {
            this._scadaEngine = scadaEngine;
        }

        /// <summary>
        /// Initialises the control page items with correct labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlPageStartup()
        {
            if(_scadaEngine.GetOperationStatus() == "Offline")
            {
                this.startOrStopButton.Text = "Start";
                this.systemStatusLabel.Text = "Idle";
            }
            else if(_scadaEngine.GetOperationStatus() == "Online")
            {
                this.startOrStopButton.Text = "Stop";
                this.systemStatusLabel.Text = "Online";
            }
            else
            {
                this.startOrStopButton.Hide();
                this.systemStatusLabel.Text = "Disabled";
            }

            normalModeRadioButton.BackColor = Color.Green;
        }

        /// <summary>
        /// Handles user's request to start or stop the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startOrStopButton_Click(object sender, EventArgs e)
        {
            if(startOrStopButton.Text == "Start")
            {
                _scadaEngine.Start();
                startOrStopButton.Text = "Stop";
            }
            else if(startOrStopButton.Text == "Stop")
            {
                _scadaEngine.Stop();
                startOrStopButton.Text = "Resume";
            }
            else
            {
                _scadaEngine.Resume();
                startOrStopButton.Text = "Stop";
            }
        }

        private void confirmOperatingModeButton_Click(object sender, EventArgs e)
        {
            if(normalModeRadioButton.Checked)
            {
                _scadaEngine.SetOperatingMode(OperationMode.Normal);
                
                normalModeRadioButton.BackColor = Color.Green;
                sortingModeRadioButton.BackColor = Color.White;
                producingModeRadioButton.BackColor = Color.White;
            }
            else if(sortingModeRadioButton.Checked)
            {
                _scadaEngine.SetOperatingMode(OperationMode.SortOnly);

                sortingModeRadioButton.BackColor = Color.Green;
                normalModeRadioButton.BackColor = Color.White;
                producingModeRadioButton.BackColor = Color.White;
            }
            else if(producingModeRadioButton.Checked)
            {
                _scadaEngine.SetOperatingMode(OperationMode.AssembleOnly);

                producingModeRadioButton.BackColor = Color.Green;
                sortingModeRadioButton.BackColor = Color.White;
                normalModeRadioButton.BackColor = Color.White;
            }
            else
            {
                MessageBox.Show("Please select an operating mode");
            }

            // reset radio button list
            clearRadioButton(normalModeRadioButton);
            clearRadioButton(sortingModeRadioButton);
            clearRadioButton(producingModeRadioButton); 
        }

        private void clearRadioButton(RadioButton b)
        {
            b.Checked = false;
        }

        private void buttonShutdown_Click(object sender, EventArgs e)
        {
            _scadaEngine.Shutdown();
        }
    }
}
