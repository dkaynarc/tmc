using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tmc.Scada.App
{
    public partial class environmentControl : UserControl
    {
        public environmentControl()
        {
            InitializeComponent();

            Teperature = listView1.Items[0];
            Humidity = listView1.Items[1];
            Light = listView1.Items[2];
            Sound = listView1.Items[3];
            Dust = listView1.Items[4];
        }

        private ListViewItem Teperature;
        private ListViewItem Humidity;
        private ListViewItem Light;
        private ListViewItem Sound;
        private ListViewItem Dust;
    }
}
