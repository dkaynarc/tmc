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


namespace Tmc.Scada.App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            updateOrder();
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

        private void button_AddNewOrder_Click(object sender, EventArgs e)
        {
            int black = 1;
            int blue = 2;
            int red = 4;
            int green = 7;
            int white = 2;

            //Validate Input

            TmcRepository.AddNewOrder(new Guid(), black, blue, red, green, white);
            updateOrder();
        }

        private void updateOrder()
        {
            orderListViewBindingSource.DataSource = TmcRepository.OrderInfo();
        }
    }
}
