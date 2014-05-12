using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tmc.Scada.App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

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
    }
}
