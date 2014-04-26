using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mobile
{
    public partial class MobileForm : Form
    {
        private WebServiceClient webService;

        public MobileForm()
        {
            InitializeComponent();
        }

        public MobileForm(WebServiceClient webService)
        {
            // TODO: Complete member initialization
            this.webService = webService;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = webService.Add(Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text)).ToString();
        }
    }
}
