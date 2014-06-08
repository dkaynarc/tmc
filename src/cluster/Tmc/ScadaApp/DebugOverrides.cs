using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tmc.Scada.Core;

namespace Tmc.Scada.App
{
    public partial class DebugOverrides : UserControl
    {
        public DebugOverrides()
        {
            InitializeComponent();
        }

        public void Initialize(ScadaEngine engine)
        {
            lvBinding.DataSource = engine.TabletMagazine.Slots;
            dgvTabMag.DataSource = lvBinding;
        }
    }
}
