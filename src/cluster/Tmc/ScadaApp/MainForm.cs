using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tmc.Scada.Core;


namespace Tmc.Scada.App
{
    public partial class MainForm : Form
    {
        private ScadaEngine scadaEngine;
        public MainForm(ScadaEngine scadaEngine)
        {
            InitializeComponent();
            this.scadaEngine = scadaEngine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
