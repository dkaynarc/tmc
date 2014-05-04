namespace PLC_form_test
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Connect = new System.Windows.Forms.Button();
            this.Quit = new System.Windows.Forms.Button();
            this.axAsadtcp1 = new AxASADTCPLib.AxAsadtcp();
            ((System.ComponentModel.ISupportInitialize)(this.axAsadtcp1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 29);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(727, 340);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Status";
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(13, 386);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 2;
            this.Connect.Text = "Get status";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Quit
            // 
            this.Quit.Location = new System.Drawing.Point(95, 386);
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(75, 23);
            this.Quit.TabIndex = 3;
            this.Quit.Text = "Quit";
            this.Quit.UseVisualStyleBackColor = true;
            this.Quit.Click += new System.EventHandler(this.Quit_Click);
            // 
            // axAsadtcp1
            // 
            this.axAsadtcp1.Enabled = true;
            this.axAsadtcp1.Location = new System.Drawing.Point(275, 386);
            this.axAsadtcp1.Name = "axAsadtcp1";
            this.axAsadtcp1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAsadtcp1.OcxState")));
            this.axAsadtcp1.Size = new System.Drawing.Size(45, 34);
            this.axAsadtcp1.TabIndex = 4;
            this.axAsadtcp1.Complete += new AxASADTCPLib._DAsadtcpEvents_CompleteEventHandler(this.axAsadtcp1_Complete);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 581);
            this.Controls.Add(this.axAsadtcp1);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "PLC prototype";
            ((System.ComponentModel.ISupportInitialize)(this.axAsadtcp1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button Quit;
        public AxASADTCPLib.AxAsadtcp axAsadtcp1;

    }
}

