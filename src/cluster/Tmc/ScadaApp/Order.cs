using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmcData;

namespace Tmc.Scada.App
{
    public partial class Order : UserControl
    {
        public Order()
        {
            InitializeComponent();
            updateOrder();
        }

        private void button_Add_Click(object sender, EventArgs e)
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
