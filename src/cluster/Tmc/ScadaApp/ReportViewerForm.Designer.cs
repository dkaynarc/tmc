namespace Tmc.Scada.App
{
    partial class ReportViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rpvReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpvReportViewer
            // 
            this.rpvReportViewer.Location = new System.Drawing.Point(0, 0);
            this.rpvReportViewer.Name = "rpvReportViewer";
            this.rpvReportViewer.Size = new System.Drawing.Size(741, 403);
            this.rpvReportViewer.TabIndex = 0;
            // 
            // ReportViewerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(742, 403);
            this.Controls.Add(this.rpvReportViewer);
            this.Name = "ReportViewerForm";
            this.Text = "Report Viewer";
            this.Load += new System.EventHandler(this.ReportViewerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpvReportViewer;
    }
}