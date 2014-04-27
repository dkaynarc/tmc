using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tmc.Scada.App
{
    /// <summary>
    /// Sets up a control that allows the different UI screens to be managed using tabs
    /// </summary>
    public partial class HiddenTabsControl : TabControl
    {
        //Might choose to leave tabs blank or use them as controls
        /*
        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == 0x1328 && !DesignMode)
            {
                m.Result = (IntPtr)1;
            }
            else
            {
                base.WndProc(ref m);
            }
        }*/
    }
}
