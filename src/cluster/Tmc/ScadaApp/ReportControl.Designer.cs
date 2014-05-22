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
            this.btnGenerateEnvironmentReport = new System.Windows.Forms.Button();
            this.btnGenerateAlarmsReport = new System.Windows.Forms.Button();
            this.btnGenerateProductionReport = new System.Windows.Forms.Button();
            this.btnGenerateMachineReport = new System.Windows.Forms.Button();
            this.btnGenerateCycleTimeReport = new System.Windows.Forms.Button();
            this.btnGenerateOrderReport = new System.Windows.Forms.Button();
            this.dteStartTime = new System.Windows.Forms.DateTimePicker();
            this.dteEndTime = new System.Windows.Forms.DateTimePicker();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGenerateEnvironmentReport
            // 
            this.btnGenerateEnvironmentReport.Location = new System.Drawing.Point(15, 83);
            this.btnGenerateEnvironmentReport.Name = "btnGenerateEnvironmentReport";
            this.btnGenerateEnvironmentReport.Size = new System.Drawing.Size(187, 82);
            this.btnGenerateEnvironmentReport.TabIndex = 0;
            this.btnGenerateEnvironmentReport.Text = "Environment Report";
            this.btnGenerateEnvironmentReport.UseVisualStyleBackColor = true;
            this.btnGenerateEnvironmentReport.Click += new System.EventHandler(this.btnGenerateEnvironmentReport_Click);
            // 
            // btnGenerateAlarmsReport
            // 
            this.btnGenerateAlarmsReport.Location = new System.Drawing.Point(15, 171);
            this.btnGenerateAlarmsReport.Name = "btnGenerateAlarmsReport";
            this.btnGenerateAlarmsReport.Size = new System.Drawing.Size(187, 82);
            this.btnGenerateAlarmsReport.TabIndex = 0;
            this.btnGenerateAlarmsReport.Text = "Alarms Report";
            this.btnGenerateAlarmsReport.UseVisualStyleBackColor = true;
            this.btnGenerateAlarmsReport.Click += new System.EventHandler(this.btnGenerateAlarmsReport_Click);
            // 
            // btnGenerateProductionReport
            // 
            this.btnGenerateProductionReport.Location = new System.Drawing.Point(251, 83);
            this.btnGenerateProductionReport.Name = "btnGenerateProductionReport";
            this.btnGenerateProductionReport.Size = new System.Drawing.Size(187, 82);
            this.btnGenerateProductionReport.TabIndex = 0;
            this.btnGenerateProductionReport.Text = "Production Report";
            this.btnGenerateProductionReport.UseVisualStyleBackColor = true;
            this.btnGenerateProductionReport.Click += new System.EventHandler(this.btnGenerateProductionReport_Click);
            // 
            // btnGenerateMachineReport
            // 
            this.btnGenerateMachineReport.Location = new System.Drawing.Point(490, 171);
            this.btnGenerateMachineReport.Name = "btnGenerateMachineReport";
            this.btnGenerateMachineReport.Size = new System.Drawing.Size(187, 82);
            this.btnGenerateMachineReport.TabIndex = 0;
            this.btnGenerateMachineReport.Text = "Machine Report";
            this.btnGenerateMachineReport.UseVisualStyleBackColor = true;
            this.btnGenerateMachineReport.Click += new System.EventHandler(this.btnGenerateMachineReport_Click);
            // 
            // btnGenerateCycleTimeReport
            // 
            this.btnGenerateCycleTimeReport.Location = new System.Drawing.Point(490, 83);
            this.btnGenerateCycleTimeReport.Name = "btnGenerateCycleTimeReport";
            this.btnGenerateCycleTimeReport.Size = new System.Drawing.Size(187, 82);
            this.btnGenerateCycleTimeReport.TabIndex = 0;
            this.btnGenerateCycleTimeReport.Text = "Average Cycle Time Report";
            this.btnGenerateCycleTimeReport.UseVisualStyleBackColor = true;
            this.btnGenerateCycleTimeReport.Click += new System.EventHandler(this.btnGenerateCycleTimeReport_Click);
            // 
            // btnGenerateOrderReport
            // 
            this.btnGenerateOrderReport.Location = new System.Drawing.Point(251, 171);
            this.btnGenerateOrderReport.Name = "btnGenerateOrderReport";
            this.btnGenerateOrderReport.Size = new System.Drawing.Size(187, 82);
            this.btnGenerateOrderReport.TabIndex = 0;
            this.btnGenerateOrderReport.Text = "Order Report";
            this.btnGenerateOrderReport.UseVisualStyleBackColor = true;
            this.btnGenerateOrderReport.Click += new System.EventHandler(this.btnGenerateOrderReport_Click);
            // 
            // dteStartTime
            // 
            this.dteStartTime.Location = new System.Drawing.Point(15, 45);
            this.dteStartTime.Name = "dteStartTime";
            this.dteStartTime.Size = new System.Drawing.Size(187, 20);
            this.dteStartTime.TabIndex = 1;
            // 
            // dteEndTime
            // 
            this.dteEndTime.Location = new System.Drawing.Point(490, 45);
            this.dteEndTime.Name = "dteEndTime";
            this.dteEndTime.Size = new System.Drawing.Size(187, 20);
            this.dteEndTime.TabIndex = 1;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(12, 29);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(58, 13);
            this.lblStartTime.TabIndex = 2;
            this.lblStartTime.Text = "Start Time:";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(487, 29);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(55, 13);
            this.lblEndTime.TabIndex = 2;
            this.lblEndTime.Text = "End Time:";
            // 
            // ReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.dteEndTime);
            this.Controls.Add(this.dteStartTime);
            this.Controls.Add(this.btnGenerateOrderReport);
            this.Controls.Add(this.btnGenerateMachineReport);
            this.Controls.Add(this.btnGenerateProductionReport);
            this.Controls.Add(this.btnGenerateCycleTimeReport);
            this.Controls.Add(this.btnGenerateAlarmsReport);
            this.Controls.Add(this.btnGenerateEnvironmentReport);
            this.Name = "ReportControl";
            this.Size = new System.Drawing.Size(691, 267);
            this.Load += new System.EventHandler(this.ReportControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateEnvironmentReport;
        private System.Windows.Forms.Button btnGenerateAlarmsReport;
        private System.Windows.Forms.Button btnGenerateProductionReport;
        private System.Windows.Forms.Button btnGenerateMachineReport;
        private System.Windows.Forms.Button btnGenerateCycleTimeReport;
        private System.Windows.Forms.Button btnGenerateOrderReport;
        private System.Windows.Forms.DateTimePicker dteStartTime;
        private System.Windows.Forms.DateTimePicker dteEndTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblEndTime;
    }
}
