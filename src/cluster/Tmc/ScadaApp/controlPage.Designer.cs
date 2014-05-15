namespace Tmc.Scada.App
{
    partial class controlPage
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
            this.startOrStopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.systemStatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startOrStopButton
            // 
            this.startOrStopButton.Location = new System.Drawing.Point(186, 19);
            this.startOrStopButton.Name = "startOrStopButton";
            this.startOrStopButton.Size = new System.Drawing.Size(75, 23);
            this.startOrStopButton.TabIndex = 0;
            this.startOrStopButton.Text = "Start";
            this.startOrStopButton.UseVisualStyleBackColor = true;
            this.startOrStopButton.Click += new System.EventHandler(this.startOrStopButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "System status:";
            // 
            // systemStatusLabel
            // 
            this.systemStatusLabel.AutoSize = true;
            this.systemStatusLabel.Location = new System.Drawing.Point(122, 24);
            this.systemStatusLabel.Name = "systemStatusLabel";
            this.systemStatusLabel.Size = new System.Drawing.Size(37, 13);
            this.systemStatusLabel.TabIndex = 2;
            this.systemStatusLabel.Text = "Offline";
            // 
            // controlPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.systemStatusLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startOrStopButton);
            this.Name = "controlPage";
            this.Size = new System.Drawing.Size(296, 66);
            this.Load += new System.EventHandler(this.controlPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startOrStopButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label systemStatusLabel;
    }
}
