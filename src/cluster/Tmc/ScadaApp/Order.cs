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
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();

            orderListBindingSource.DataSource = TmcRepository.OrderInfo();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void orderListBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            
        }
    }
}
