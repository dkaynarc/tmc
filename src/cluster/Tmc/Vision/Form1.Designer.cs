namespace Tmc.Vision
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Npar1 = new System.Windows.Forms.NumericUpDown();
            this.Npar2 = new System.Windows.Forms.NumericUpDown();
            this.Nmin = new System.Windows.Forms.NumericUpDown();
            this.Nmax = new System.Windows.Forms.NumericUpDown();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Npar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Npar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(538, 385);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Npar1
            // 
            this.Npar1.DecimalPlaces = 2;
            this.Npar1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Npar1.Location = new System.Drawing.Point(43, 420);
            this.Npar1.Name = "Npar1";
            this.Npar1.Size = new System.Drawing.Size(120, 22);
            this.Npar1.TabIndex = 1;
            this.Npar1.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // Npar2
            // 
            this.Npar2.DecimalPlaces = 2;
            this.Npar2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Npar2.Location = new System.Drawing.Point(43, 457);
            this.Npar2.Name = "Npar2";
            this.Npar2.Size = new System.Drawing.Size(120, 22);
            this.Npar2.TabIndex = 2;
            this.Npar2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Nmin
            // 
            this.Nmin.Location = new System.Drawing.Point(244, 420);
            this.Nmin.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Nmin.Name = "Nmin";
            this.Nmin.Size = new System.Drawing.Size(120, 22);
            this.Nmin.TabIndex = 3;
            // 
            // Nmax
            // 
            this.Nmax.Location = new System.Drawing.Point(244, 456);
            this.Nmax.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.Nmax.Name = "Nmax";
            this.Nmax.Size = new System.Drawing.Size(120, 22);
            this.Nmax.TabIndex = 4;
            this.Nmax.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(564, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(573, 383);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 557);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.Nmax);
            this.Controls.Add(this.Nmin);
            this.Controls.Add(this.Npar2);
            this.Controls.Add(this.Npar1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Npar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Npar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Nmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown Npar1;
        private System.Windows.Forms.NumericUpDown Npar2;
        private System.Windows.Forms.NumericUpDown Nmin;
        private System.Windows.Forms.NumericUpDown Nmax;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}