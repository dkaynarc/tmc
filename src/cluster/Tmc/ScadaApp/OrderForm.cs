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
    public partial class OrderForm : Form
    {
        //server dummy code for sequence diagram
        ScadaWCFServer server = new ScadaWCFServer();
        public OrderForm()
        {
            InitializeComponent();
        }

        private void createOrderButton_Click(object sender, EventArgs e)
        {
            //New order added to database
            server.createOrder();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
