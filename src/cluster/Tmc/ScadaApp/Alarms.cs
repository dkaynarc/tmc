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
    public partial class Alarms : UserControl
    {
        public Alarms()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            this.UpdateAlarms();
        }

        private void UpdateAlarms()
        {
            this.alarmsBindingSource.DataSource = TmcRepository.GetAllAlarms();
        }

        private void buttonDismiss_Click(object sender, EventArgs e)
        {
            if(this.dataGridViewAlarms.SelectedCells.Count > 0)
            {
                foreach(var item in this.dataGridViewAlarms.SelectedCells)
                {
                    
                }
            }
        }
    }
}
