using Microsoft.Reporting.WinForms;
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
    public partial class ReportViewerForm : Form
    {
        public ReportViewer ReportViewer { get { return this.rpvReportViewer; } }

        public ReportViewerForm()
        {
            InitializeComponent();
        }

        private void ReportViewerForm_Load(object sender, EventArgs e)
        {
            this.rpvReportViewer.RefreshReport();
        }
    }
}
