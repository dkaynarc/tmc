using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tmc.Sensors;


namespace Tmc.Scada.App
{
    public partial class MainForm : Form
    {
        private Plc _plc;
        public MainForm()
        {
            InitializeComponent();

            _plc = new Plc();
            _plc.SetParameters(new Dictionary<string, string>
            {
                { "Name", "PLC" },
                { "IPAddress", "192.168.1.5"}
            });
            try
            {
                _plc.Initialise();
                var switchStates = _plc.GetSwitchStates();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
