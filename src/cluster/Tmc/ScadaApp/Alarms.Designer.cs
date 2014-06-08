namespace Tmc.Scada.App
{
    partial class Alarms
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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewAlarms = new System.Windows.Forms.DataGridView();
            this.alarmsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonDismiss = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alarmsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAlarms
            // 
            this.dataGridViewAlarms.AutoGenerateColumns = false;
            this.dataGridViewAlarms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAlarms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.logTypeDataGridViewTextBoxColumn});
            this.dataGridViewAlarms.DataSource = this.alarmsBindingSource;
            this.dataGridViewAlarms.Location = new System.Drawing.Point(0, 53);
            this.dataGridViewAlarms.Name = "dataGridViewAlarms";
            this.dataGridViewAlarms.Size = new System.Drawing.Size(681, 296);
            this.dataGridViewAlarms.TabIndex = 0;
            // 
            // alarmsBindingSource
            // 
            this.alarmsBindingSource.DataSource = typeof(TmcData.ComponentEventLogView);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            this.timestampDataGridViewTextBoxColumn.DataPropertyName = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.HeaderText = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.Width = 340;
            // 
            // logTypeDataGridViewTextBoxColumn
            // 
            this.logTypeDataGridViewTextBoxColumn.DataPropertyName = "LogType";
            this.logTypeDataGridViewTextBoxColumn.HeaderText = "LogType";
            this.logTypeDataGridViewTextBoxColumn.Name = "logTypeDataGridViewTextBoxColumn";
            // 
            // buttonDismiss
            // 
            this.buttonDismiss.Location = new System.Drawing.Point(593, 12);
            this.buttonDismiss.Name = "buttonDismiss";
            this.buttonDismiss.Size = new System.Drawing.Size(75, 23);
            this.buttonDismiss.TabIndex = 1;
            this.buttonDismiss.Text = "Dismiss";
            this.buttonDismiss.UseVisualStyleBackColor = true;
            this.buttonDismiss.Click += new System.EventHandler(this.buttonDismiss_Click);
            // 
            // Alarms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonDismiss);
            this.Controls.Add(this.dataGridViewAlarms);
            this.Name = "Alarms";
            this.Size = new System.Drawing.Size(684, 352);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alarmsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAlarms;
        private System.Windows.Forms.BindingSource alarmsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn logTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button buttonDismiss;
    }
}
