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
        private Timer _timer;

        public Alarms()
        {
            InitializeComponent();
            this.Initialize();
            _timer = new Timer();
            _timer.Interval = 2000;
            _timer.Tick += timerTick;
            _timer.Start();
        }

        public void Initialize()
        {
            this.alarmsBindingSource.DataSource = TmcRepository.GetAllAlarms();
        }

        private void UpdateAlarms()
        {
            var newAlarms = TmcRepository.GetAllAlarms();
            var oldAlarms = this.alarmsBindingSource.DataSource as IList<ComponentEventLogView>;
            
            if(oldAlarms != null && newAlarms.Count != oldAlarms.Count)
            {
                this.alarmsBindingSource.DataSource = newAlarms;
            }
        }

        private void timerTick(object source, EventArgs e)
        {
            this.UpdateAlarms();
        }

        private void buttonDismiss_Click(object sender, EventArgs e)
        {
            if(this.dataGridViewAlarms.SelectedCells.Count > 0)
            {
                foreach(DataGridViewCell item in this.dataGridViewAlarms.SelectedCells)
                {
                    var alarmsList = this.alarmsBindingSource.DataSource as IList<ComponentEventLogView>;
                    TmcRepository.AcknowledgeEvent(alarmsList.ElementAt(item.RowIndex).ID);                    
                }
            }

            this.UpdateAlarms();
        }
    }
}
