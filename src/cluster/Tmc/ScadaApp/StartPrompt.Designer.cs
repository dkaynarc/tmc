namespace Tmc.Scada.App
{
    partial class StartPrompt
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
            this.startPromptOKButton = new System.Windows.Forms.Button();
            this.startPromptCancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startPromptOKButton
            // 
            this.startPromptOKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.startPromptOKButton.Location = new System.Drawing.Point(68, 113);
            this.startPromptOKButton.Name = "startPromptOKButton";
            this.startPromptOKButton.Size = new System.Drawing.Size(75, 23);
            this.startPromptOKButton.TabIndex = 0;
            this.startPromptOKButton.Text = "OK";
            this.startPromptOKButton.UseVisualStyleBackColor = true;
            // 
            // startPromptCancelButton
            // 
            this.startPromptCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.startPromptCancelButton.Location = new System.Drawing.Point(159, 113);
            this.startPromptCancelButton.Name = "startPromptCancelButton";
            this.startPromptCancelButton.Size = new System.Drawing.Size(75, 23);
            this.startPromptCancelButton.TabIndex = 1;
            this.startPromptCancelButton.Text = "Cancel";
            this.startPromptCancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "The TMC is currently switched off, would you like to start it?";
            // 
            // StartPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 148);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startPromptCancelButton);
            this.Controls.Add(this.startPromptOKButton);
            this.Name = "StartPrompt";
            this.Text = "StartPrompt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startPromptOKButton;
        private System.Windows.Forms.Button startPromptCancelButton;
        private System.Windows.Forms.Label label1;
    }
}