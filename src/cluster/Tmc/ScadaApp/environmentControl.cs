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
    public partial class environmentControl : UserControl
    {
        private Timer _timer;

        public environmentControl()
        {
            InitializeComponent();

            _timer = new Timer();
            _timer.Interval = 5000;
            _timer.Tick += Update;
            _timer.Start();

            Teperature = listView1.Items[0].SubItems[1];
            Humidity = listView1.Items[1].SubItems[1];
            Light = listView1.Items[2].SubItems[1];
            Sound = listView1.Items[3].SubItems[1];
            Dust = listView1.Items[4].SubItems[1];
        }

        private void Update(object source, EventArgs args)
        {
            Teperature.Text = TmcRepository.GetLatestEnvironment(Source.Temperature.ToString()).Reading.ToString();
            Humidity.Text = TmcRepository.GetLatestEnvironment(Source.Humidity.ToString()).Reading.ToString();
            Light.Text = TmcRepository.GetLatestEnvironment(Source.Light.ToString()).Reading.ToString();
            Sound.Text = TmcRepository.GetLatestEnvironment(Source.Sound.ToString()).Reading.ToString();
            Dust.Text = TmcRepository.GetLatestEnvironment(Source.Dust.ToString()).Reading.ToString();
        }

        private ListViewItem.ListViewSubItem Teperature;
        private ListViewItem.ListViewSubItem Humidity;
        private ListViewItem.ListViewSubItem Light;
        private ListViewItem.ListViewSubItem Sound;
        private ListViewItem.ListViewSubItem Dust;
    }
}
