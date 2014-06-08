namespace Tmc.Scada.App
{
    partial class DebugOverrides
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
            this.gbTabMag = new System.Windows.Forms.GroupBox();
            this.dgvTabMag = new System.Windows.Forms.DataGridView();
            this.lvBinding = new System.Windows.Forms.BindingSource(this.components);
            this.gbTabMag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabMag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTabMag
            // 
            this.gbTabMag.Controls.Add(this.dgvTabMag);
            this.gbTabMag.Location = new System.Drawing.Point(3, 3);
            this.gbTabMag.Name = "gbTabMag";
            this.gbTabMag.Size = new System.Drawing.Size(294, 203);
            this.gbTabMag.TabIndex = 0;
            this.gbTabMag.TabStop = false;
            this.gbTabMag.Text = "Tablet Magazine";
            // 
            // dgvTabMag
            // 
            this.dgvTabMag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTabMag.Location = new System.Drawing.Point(7, 17);
            this.dgvTabMag.Name = "dgvTabMag";
            this.dgvTabMag.Size = new System.Drawing.Size(281, 180);
            this.dgvTabMag.TabIndex = 11;
            // 
            // DebugOverrides
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTabMag);
            this.Name = "DebugOverrides";
            this.Size = new System.Drawing.Size(308, 212);
            this.gbTabMag.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabMag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvBinding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTabMag;
        private System.Windows.Forms.BindingSource lvBinding;
        private System.Windows.Forms.DataGridView dgvTabMag;

    }
}
