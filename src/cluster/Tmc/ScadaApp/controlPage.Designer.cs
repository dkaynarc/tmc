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
            this.normalModeRadioButton = new System.Windows.Forms.RadioButton();
            this.sortingModeRadioButton = new System.Windows.Forms.RadioButton();
            this.producingModeRadioButton = new System.Windows.Forms.RadioButton();
            this.operatingModeGroupBox = new System.Windows.Forms.GroupBox();
            this.confirmOperatingModeButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.operatingModeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // startOrStopButton
            // 
            this.startOrStopButton.Location = new System.Drawing.Point(140, 11);
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
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "System status:";
            // 
            // systemStatusLabel
            // 
            this.systemStatusLabel.AutoSize = true;
            this.systemStatusLabel.Location = new System.Drawing.Point(84, 16);
            this.systemStatusLabel.Name = "systemStatusLabel";
            this.systemStatusLabel.Size = new System.Drawing.Size(37, 13);
            this.systemStatusLabel.TabIndex = 2;
            this.systemStatusLabel.Text = "Offline";
            // 
            // normalModeRadioButton
            // 
            this.normalModeRadioButton.AutoSize = true;
            this.normalModeRadioButton.Location = new System.Drawing.Point(9, 23);
            this.normalModeRadioButton.Name = "normalModeRadioButton";
            this.normalModeRadioButton.Size = new System.Drawing.Size(172, 17);
            this.normalModeRadioButton.TabIndex = 3;
            this.normalModeRadioButton.TabStop = true;
            this.normalModeRadioButton.Text = "Normal (Sorting and Producing)";
            this.normalModeRadioButton.UseVisualStyleBackColor = true;
            // 
            // sortingModeRadioButton
            // 
            this.sortingModeRadioButton.AutoSize = true;
            this.sortingModeRadioButton.Location = new System.Drawing.Point(9, 46);
            this.sortingModeRadioButton.Name = "sortingModeRadioButton";
            this.sortingModeRadioButton.Size = new System.Drawing.Size(58, 17);
            this.sortingModeRadioButton.TabIndex = 4;
            this.sortingModeRadioButton.TabStop = true;
            this.sortingModeRadioButton.Text = "Sorting";
            this.sortingModeRadioButton.UseVisualStyleBackColor = true;
            // 
            // producingModeRadioButton
            // 
            this.producingModeRadioButton.AutoSize = true;
            this.producingModeRadioButton.Location = new System.Drawing.Point(9, 69);
            this.producingModeRadioButton.Name = "producingModeRadioButton";
            this.producingModeRadioButton.Size = new System.Drawing.Size(73, 17);
            this.producingModeRadioButton.TabIndex = 5;
            this.producingModeRadioButton.TabStop = true;
            this.producingModeRadioButton.Text = "Producing";
            this.producingModeRadioButton.UseVisualStyleBackColor = true;
            // 
            // operatingModeGroupBox
            // 
            this.operatingModeGroupBox.Controls.Add(this.confirmOperatingModeButton);
            this.operatingModeGroupBox.Controls.Add(this.normalModeRadioButton);
            this.operatingModeGroupBox.Controls.Add(this.sortingModeRadioButton);
            this.operatingModeGroupBox.Controls.Add(this.producingModeRadioButton);
            this.operatingModeGroupBox.Location = new System.Drawing.Point(15, 49);
            this.operatingModeGroupBox.Name = "operatingModeGroupBox";
            this.operatingModeGroupBox.Size = new System.Drawing.Size(200, 127);
            this.operatingModeGroupBox.TabIndex = 7;
            this.operatingModeGroupBox.TabStop = false;
            this.operatingModeGroupBox.Text = "Operating Mode";
            // 
            // confirmOperatingModeButton
            // 
            this.confirmOperatingModeButton.Location = new System.Drawing.Point(9, 98);
            this.confirmOperatingModeButton.Name = "confirmOperatingModeButton";
            this.confirmOperatingModeButton.Size = new System.Drawing.Size(75, 23);
            this.confirmOperatingModeButton.TabIndex = 6;
            this.confirmOperatingModeButton.Text = "Confirm";
            this.confirmOperatingModeButton.UseVisualStyleBackColor = true;
            this.confirmOperatingModeButton.Click += new System.EventHandler(this.confirmOperatingModeButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(226, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Shutdown";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // controlPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.operatingModeGroupBox);
            this.Controls.Add(this.systemStatusLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startOrStopButton);
            this.Name = "controlPage";
            this.Size = new System.Drawing.Size(309, 186);
            this.Load += new System.EventHandler(this.controlPage_Load);
            this.operatingModeGroupBox.ResumeLayout(false);
            this.operatingModeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startOrStopButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label systemStatusLabel;
        private System.Windows.Forms.RadioButton normalModeRadioButton;
        private System.Windows.Forms.RadioButton sortingModeRadioButton;
        private System.Windows.Forms.RadioButton producingModeRadioButton;
        private System.Windows.Forms.GroupBox operatingModeGroupBox;
        private System.Windows.Forms.Button confirmOperatingModeButton;
        private System.Windows.Forms.Button button1;
    }
}
