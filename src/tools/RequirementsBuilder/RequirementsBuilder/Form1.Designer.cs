namespace RequirementsBuilder
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
            this.btnBuild = new System.Windows.Forms.Button();
            this.tbSpreadsheetPath = new System.Windows.Forms.TextBox();
            this.btnSpreadsheetBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbBuildTrace = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnBuild
            // 
            this.btnBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuild.Location = new System.Drawing.Point(137, 51);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(132, 23);
            this.btnBuild.TabIndex = 0;
            this.btnBuild.Text = "Build Requirements";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // tbSpreadsheetPath
            // 
            this.tbSpreadsheetPath.Location = new System.Drawing.Point(15, 25);
            this.tbSpreadsheetPath.Name = "tbSpreadsheetPath";
            this.tbSpreadsheetPath.Size = new System.Drawing.Size(325, 20);
            this.tbSpreadsheetPath.TabIndex = 3;
            // 
            // btnSpreadsheetBrowse
            // 
            this.btnSpreadsheetBrowse.Location = new System.Drawing.Point(363, 23);
            this.btnSpreadsheetBrowse.Name = "btnSpreadsheetBrowse";
            this.btnSpreadsheetBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnSpreadsheetBrowse.TabIndex = 4;
            this.btnSpreadsheetBrowse.Text = "Browse";
            this.btnSpreadsheetBrowse.UseVisualStyleBackColor = true;
            this.btnSpreadsheetBrowse.Click += new System.EventHandler(this.btnSpreadsheetBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Requirements Spreadsheet";
            // 
            // cbBuildTrace
            // 
            this.cbBuildTrace.AutoSize = true;
            this.cbBuildTrace.Location = new System.Drawing.Point(290, 55);
            this.cbBuildTrace.Name = "cbBuildTrace";
            this.cbBuildTrace.Size = new System.Drawing.Size(137, 17);
            this.cbBuildTrace.TabIndex = 7;
            this.cbBuildTrace.Text = "Build Traceability Matrix";
            this.cbBuildTrace.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 84);
            this.Controls.Add(this.cbBuildTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSpreadsheetBrowse);
            this.Controls.Add(this.tbSpreadsheetPath);
            this.Controls.Add(this.btnBuild);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Requirements Doc Builder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.TextBox tbSpreadsheetPath;
        private System.Windows.Forms.Button btnSpreadsheetBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbBuildTrace;

    }
}

