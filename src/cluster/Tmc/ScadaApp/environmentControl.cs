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

            Teperature = listView1.Items[0].SubItems[1];
            Humidity = listView1.Items[1].SubItems[1];
            Light = listView1.Items[2].SubItems[1];
            Sound = listView1.Items[3].SubItems[1];
            Dust = listView1.Items[4].SubItems[1];
        }

        private ListViewItem.ListViewSubItem Teperature;
        private ListViewItem.ListViewSubItem Humidity;
        private ListViewItem.ListViewSubItem Light;
        private ListViewItem.ListViewSubItem Sound;
        private ListViewItem.ListViewSubItem Dust;
    }
}
