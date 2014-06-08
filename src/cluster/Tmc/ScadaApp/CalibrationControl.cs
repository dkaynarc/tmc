using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tmc.Common;
using Tmc.Vision;
using TmcData;

namespace Tmc.Scada.App
{
    public partial class CalibrationControl : UserControl
    {
        Timer timer;
        private const int ONE_SEC_IN_MILLISECS = 1000;

        public CalibrationControl()
        {
            InitializeComponent();
            this.timer = new Timer();
            this.timer.Interval = ONE_SEC_IN_MILLISECS;
            this.timer.Tick += timer_Tick;
        }

        private void CalibrationControl_Load(object sender, EventArgs e)
        {
            this.timer.Start();
            this.EnabledChanged += CalibrationControl_EnabledChanged;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //do whatever happens every second
        }

        void CalibrationControl_EnabledChanged(object sender, EventArgs e)
        {
            if (!this.Enabled)
            {
                this.timer.Stop();
            }
            else
            {
                this.timer.Start();
            }
        }

        private void btnCalibrateSensorCamera_Click(object sender, EventArgs e)
        {
            try
            {
                CalibrationManager.Instance.CalibrateSpecific<SorterVision>();
                MessageBox.Show("Calibration completed");
            }
            catch (Exception ex)
            {
                Logger.Instance.Write(new LogEntry(ex));
            }
        }
    }
}
