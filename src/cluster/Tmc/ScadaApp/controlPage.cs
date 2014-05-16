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

namespace Tmc.Scada.App
{
    public partial class controlPage : UserControl
    {
        private ScadaEngine scadaEngine;
        public controlPage(ScadaEngine scadaEngine)
        {
            InitializeComponent();
            this.scadaEngine = scadaEngine;
        }

        /// <summary>
        /// Initialises the control page items with correct labels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlPage_Load(object sender, EventArgs e)
        {
            if(scadaEngine.GetOperationStatus() == "Offline")
            {
                this.startOrStopButton.Text = "Start";
                this.systemStatusLabel.Text = "Offline";
            }
            else if(scadaEngine.GetOperationStatus() == "Online")
            {
                this.startOrStopButton.Text = "Stop";
                this.systemStatusLabel.Text = "Online";
            }
            else
            {
                this.startOrStopButton.Hide();
                this.systemStatusLabel.Text = "Disabled";
            }
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
                scadaEngine.Start();
            }
            else
            {
                scadaEngine.Stop();
            }
        }

        private void confirmOperatingModeButton_Click(object sender, EventArgs e)
        {
            if(normalModeRadioButton.Checked)
            {
                this.scadaEngine.SetOperatingMode("normal");
            }
            else if(sortingModeRadioButton.Checked)
            {
                this.scadaEngine.SetOperatingMode("sorting");
            }
            else if(producingModeRadioButton.Checked)
            {
                this.scadaEngine.SetOperatingMode("producing");
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
    }
}
