namespace Tmc.Scada.App
{
    partial class CalibrationControl
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
            this.btnCalibrateSensorCamera = new System.Windows.Forms.Button();
            this.BtnCalibrateAssemblyCamera = new System.Windows.Forms.Button();
            this.imgAssemblyCameraFeed = new System.Windows.Forms.PictureBox();
            this.imgSorterCameraFeed = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgAssemblyCameraFeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSorterCameraFeed)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCalibrateSensorCamera
            // 
            this.btnCalibrateSensorCamera.Location = new System.Drawing.Point(3, 314);
            this.btnCalibrateSensorCamera.Name = "btnCalibrateSensorCamera";
            this.btnCalibrateSensorCamera.Size = new System.Drawing.Size(335, 37);
            this.btnCalibrateSensorCamera.TabIndex = 0;
            this.btnCalibrateSensorCamera.Text = "Calibrate Sorter Camera";
            this.btnCalibrateSensorCamera.UseVisualStyleBackColor = true;
            this.btnCalibrateSensorCamera.Click += new System.EventHandler(this.btnCalibrateSensorCamera_Click);
            // 
            // BtnCalibrateAssemblyCamera
            // 
            this.BtnCalibrateAssemblyCamera.Enabled = false;
            this.BtnCalibrateAssemblyCamera.Location = new System.Drawing.Point(349, 314);
            this.BtnCalibrateAssemblyCamera.Name = "BtnCalibrateAssemblyCamera";
            this.BtnCalibrateAssemblyCamera.Size = new System.Drawing.Size(335, 37);
            this.BtnCalibrateAssemblyCamera.TabIndex = 0;
            this.BtnCalibrateAssemblyCamera.Text = "Calibrate Assembly Camera";
            this.BtnCalibrateAssemblyCamera.UseVisualStyleBackColor = true;
            // 
            // imgAssemblyCameraFeed
            // 
            this.imgAssemblyCameraFeed.Location = new System.Drawing.Point(349, 3);
            this.imgAssemblyCameraFeed.Name = "imgAssemblyCameraFeed";
            this.imgAssemblyCameraFeed.Size = new System.Drawing.Size(335, 305);
            this.imgAssemblyCameraFeed.TabIndex = 1;
            this.imgAssemblyCameraFeed.TabStop = false;
            // 
            // imgSorterCameraFeed
            // 
            this.imgSorterCameraFeed.Location = new System.Drawing.Point(3, 3);
            this.imgSorterCameraFeed.Name = "imgSorterCameraFeed";
            this.imgSorterCameraFeed.Size = new System.Drawing.Size(335, 305);
            this.imgSorterCameraFeed.TabIndex = 1;
            this.imgSorterCameraFeed.TabStop = false;
            // 
            // CalibrationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imgAssemblyCameraFeed);
            this.Controls.Add(this.imgSorterCameraFeed);
            this.Controls.Add(this.BtnCalibrateAssemblyCamera);
            this.Controls.Add(this.btnCalibrateSensorCamera);
            this.Name = "CalibrationControl";
            this.Size = new System.Drawing.Size(687, 354);
            this.Load += new System.EventHandler(this.CalibrationControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgAssemblyCameraFeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSorterCameraFeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCalibrateSensorCamera;
        private System.Windows.Forms.Button BtnCalibrateAssemblyCamera;
        private System.Windows.Forms.PictureBox imgSorterCameraFeed;
        private System.Windows.Forms.PictureBox imgAssemblyCameraFeed;
    }
}
