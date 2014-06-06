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
            List<int> orderConfig = 
                new List<decimal>()
                {
                    numericUpDown_black.Value,
                    numericUpDown_blue.Value,
                    numericUpDown_red.Value,
                    numericUpDown_green.Value, 
                    numericUpDown_white.Value
                }.
                Select(Decimal.ToInt32).
                ToList();

            bool tooManyTablets = orderConfig.Where(tabletNo => tabletNo > 4).Count() != 0;
            bool emptyOrder = orderConfig.Where(tabletNo => tabletNo == 0).Count() == orderConfig.Count();

            if (orderIsValid(tooManyTablets, emptyOrder))
            {
                TmcRepository.AddNewOrder(
                    new Guid(), //replace with user id
                    orderConfig[0],
                    orderConfig[1],
                    orderConfig[2],
                    orderConfig[3],
                    orderConfig[4]);

                updateOrder();
            }
        }

        private bool orderIsValid(bool tooManyTablets, bool emptyOrder)
        {
            if (tooManyTablets || emptyOrder)
            {
                label_addOrderError.Show();
                label_invalidOrderReason.Text = tooManyTablets ? "Order can have a maximum of 4 tablets of each type" : "Order can't be empty";
                label_invalidOrderReason.Show();

                return false;
            }
            else
            {
                label_addOrderError.Hide();
                label_invalidOrderReason.Hide();

                return true;
            }
        }


        private void updateOrder()
        {
            //var orderList = TmcRepository.OrderInfo();

            //orderListViewBindingSource.DataSource = checkBox_showCancelled.Checked ? orderList : orderList.Where(order => order.Status != "Cancelled");
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;

            if (row != null)
            {
                int orderID = (int)row.Cells[0].Value;
                TmcRepository.CancelOrder(orderID);
                updateOrder();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            updateOrder();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
