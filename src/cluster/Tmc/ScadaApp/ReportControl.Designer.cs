namespace Tmc.Scada.App
{
    partial class ReportControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dteStartTime = new System.Windows.Forms.DateTimePicker();
            this.dteEndTime = new System.Windows.Forms.DateTimePicker();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.cboReports = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlEnvironmentReportFilters = new System.Windows.Forms.Panel();
            this.clbEnvironmentSourceFilter = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlAlarmReportFilters = new System.Windows.Forms.Panel();
            this.clbAlarmsTypeFilter = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlOrderReportFilters = new System.Windows.Forms.Panel();
            this.txtOrderIdFilter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlEnvironmentReportFilters.SuspendLayout();
            this.pnlAlarmReportFilters.SuspendLayout();
            this.pnlOrderReportFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // dteStartTime
            // 
            this.dteStartTime.Location = new System.Drawing.Point(15, 93);
            this.dteStartTime.Name = "dteStartTime";
            this.dteStartTime.Size = new System.Drawing.Size(223, 20);
            this.dteStartTime.TabIndex = 1;
            // 
            // dteEndTime
            // 
            this.dteEndTime.Location = new System.Drawing.Point(15, 150);
            this.dteEndTime.Name = "dteEndTime";
            this.dteEndTime.Size = new System.Drawing.Size(223, 20);
            this.dteEndTime.TabIndex = 1;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(12, 77);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(74, 13);
            this.lblStartTime.TabIndex = 2;
            this.lblStartTime.Text = "Get data from:";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(12, 134);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(31, 13);
            this.lblEndTime.TabIndex = 2;
            this.lblEndTime.Text = "Until:";
            // 
            // cboReports
            // 
            this.cboReports.FormattingEnabled = true;
            this.cboReports.Location = new System.Drawing.Point(15, 40);
            this.cboReports.Name = "cboReports";
            this.cboReports.Size = new System.Drawing.Size(223, 21);
            this.cboReports.TabIndex = 3;
            this.cboReports.SelectedIndexChanged += new System.EventHandler(this.cboReports_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Report to Generate:";
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(15, 195);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(223, 58);
            this.btnGenerateReport.TabIndex = 4;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Filtering Options:";
            // 
            // pnlEnvironmentReportFilters
            // 
            this.pnlEnvironmentReportFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEnvironmentReportFilters.Controls.Add(this.clbEnvironmentSourceFilter);
            this.pnlEnvironmentReportFilters.Controls.Add(this.label3);
            this.pnlEnvironmentReportFilters.Location = new System.Drawing.Point(265, 40);
            this.pnlEnvironmentReportFilters.Name = "pnlEnvironmentReportFilters";
            this.pnlEnvironmentReportFilters.Size = new System.Drawing.Size(46, 41);
            this.pnlEnvironmentReportFilters.TabIndex = 5;
            // 
            // clbEnvironmentSourceFilter
            // 
            this.clbEnvironmentSourceFilter.FormattingEnabled = true;
            this.clbEnvironmentSourceFilter.Location = new System.Drawing.Point(13, 27);
            this.clbEnvironmentSourceFilter.Name = "clbEnvironmentSourceFilter";
            this.clbEnvironmentSourceFilter.Size = new System.Drawing.Size(131, 169);
            this.clbEnvironmentSourceFilter.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Source:";
            // 
            // pnlAlarmReportFilters
            // 
            this.pnlAlarmReportFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAlarmReportFilters.Controls.Add(this.clbAlarmsTypeFilter);
            this.pnlAlarmReportFilters.Controls.Add(this.label4);
            this.pnlAlarmReportFilters.Location = new System.Drawing.Point(265, 87);
            this.pnlAlarmReportFilters.Name = "pnlAlarmReportFilters";
            this.pnlAlarmReportFilters.Size = new System.Drawing.Size(46, 48);
            this.pnlAlarmReportFilters.TabIndex = 6;
            // 
            // clbAlarmsTypeFilter
            // 
            this.clbAlarmsTypeFilter.FormattingEnabled = true;
            this.clbAlarmsTypeFilter.Location = new System.Drawing.Point(13, 27);
            this.clbAlarmsTypeFilter.Name = "clbAlarmsTypeFilter";
            this.clbAlarmsTypeFilter.Size = new System.Drawing.Size(131, 169);
            this.clbAlarmsTypeFilter.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Alarm Type:";
            // 
            // pnlOrderReportFilters
            // 
            this.pnlOrderReportFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrderReportFilters.Controls.Add(this.txtOrderIdFilter);
            this.pnlOrderReportFilters.Controls.Add(this.label5);
            this.pnlOrderReportFilters.Location = new System.Drawing.Point(265, 150);
            this.pnlOrderReportFilters.Name = "pnlOrderReportFilters";
            this.pnlOrderReportFilters.Size = new System.Drawing.Size(46, 50);
            this.pnlOrderReportFilters.TabIndex = 7;
            // 
            // txtOrderIdFilter
            // 
            this.txtOrderIdFilter.Location = new System.Drawing.Point(13, 27);
            this.txtOrderIdFilter.Name = "txtOrderIdFilter";
            this.txtOrderIdFilter.Size = new System.Drawing.Size(102, 20);
            this.txtOrderIdFilter.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Order ID:";
            // 
            // ReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlOrderReportFilters);
            this.Controls.Add(this.pnlAlarmReportFilters);
            this.Controls.Add(this.pnlEnvironmentReportFilters);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.cboReports);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.dteEndTime);
            this.Controls.Add(this.dteStartTime);
            this.Name = "ReportControl";
            this.Size = new System.Drawing.Size(691, 267);
            this.Load += new System.EventHandler(this.ReportControl_Load);
            this.pnlEnvironmentReportFilters.ResumeLayout(false);
            this.pnlEnvironmentReportFilters.PerformLayout();
            this.pnlAlarmReportFilters.ResumeLayout(false);
            this.pnlAlarmReportFilters.PerformLayout();
            this.pnlOrderReportFilters.ResumeLayout(false);
            this.pnlOrderReportFilters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dteStartTime;
        private System.Windows.Forms.DateTimePicker dteEndTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.ComboBox cboReports;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlEnvironmentReportFilters;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox clbEnvironmentSourceFilter;
        private System.Windows.Forms.Panel pnlAlarmReportFilters;
        private System.Windows.Forms.CheckedListBox clbAlarmsTypeFilter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlOrderReportFilters;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOrderIdFilter;
    }
}
