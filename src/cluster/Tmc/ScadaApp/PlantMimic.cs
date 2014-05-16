using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tmc.Common;
using Tmc.Scada.Core;

namespace Tmc.Scada.App.UserControls
{
    public partial class PlantMimic : UserControl
    {
        Timer timer;
        private const int ONE_SEC_IN_MILLISECS = 1000;

        public enum Hardware
        {
            SorterRobot, AssemblerRobot, LoaderRobot, PalletiserRobot, SorterCamera, AssemblerCamera, SorterConveyor, AssemblerConveyor
        }

        List<IHardware> hardwareList;

        public PlantMimic()
        {
            InitializeComponent();
            this.timer = new Timer();
            this.timer.Interval = ONE_SEC_IN_MILLISECS;
            this.timer.Tick += timer_Tick;
        }

        private void PlantMimic_Load(object sender, EventArgs e)
        {
            this.timer.Start();
            this.EnabledChanged += PlantMimic_EnabledChanged;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //do whatever happens every second 
        }

        void PlantMimic_EnabledChanged(object sender, EventArgs e)
        {
            if (!this.Enabled)
            {
                this.timer.Stop();
            }
            else
            {
                this.timer.Start();
            }
        }

        private void GetHardwareList()
        {
            //Check if SCADA is initialised
            this.hardwareList = new ClusterConfig().GetAllHardware();
        }

        /// <summary>
        /// Changes a machine's status on the UI based on its status.
        /// </summary>
        /// <param name="hardware">The hardware to change.</param>
        /// <param name="hardwareStatus">The status to change to.</param>
        public void ChangeHardwareStatus(Hardware hardware, HardwareStatus hardwareStatus)
        {
            string pictureBoxName = this.GetHardwarePictureBoxName(hardware);
            PictureBox pictureBox = (PictureBox)this.pnlContainer.Controls.Find(pictureBoxName, false).FirstOrDefault(x => x.Name == pictureBoxName);

            if (hardware == Hardware.SorterRobot || hardware == Hardware.LoaderRobot 
                || hardware == Hardware.AssemblerRobot || hardware == Hardware.PalletiserRobot)
            {
                this.ChangeRobotPictureBox(pictureBox, hardwareStatus);
            }
            else if (hardware == Hardware.SorterCamera || hardware == Hardware.AssemblerCamera)
            {
                this.ChangeCameraPictureBox(pictureBox, hardwareStatus);
            }
            else if (hardware == Hardware.AssemblerConveyor || hardware == Hardware.SorterConveyor)
            {
                this.ChangeConveyorPictureBox(pictureBox, hardwareStatus);
            }
        }

        /// <summary>
        /// Get the name of the PictureBox which representes a selected hardware item.
        /// </summary>
        /// <param name="hardware">The selected hardware item.</param>
        /// <returns>The name of the PictureBox which represents the hardware.</returns>
        private string GetHardwarePictureBoxName(Hardware hardware)
        {
            switch (hardware)
            {
                case Hardware.SorterRobot: return this.imgSorterRobot.Name;
                case Hardware.AssemblerRobot: return this.imgAssemblerRobot.Name;
                case Hardware.LoaderRobot: return this.imgLoaderRobot.Name;
                case Hardware.PalletiserRobot: return this.imgPalletiserRobot.Name;
                case Hardware.SorterCamera: return this.imgSorterCamera.Name;
                case Hardware.AssemblerCamera: return this.imgAssemblerCamera.Name;
                case Hardware.SorterConveyor: return this.imgSorterConveyor.Name;
                case Hardware.AssemblerConveyor: return this.imgAssemblyConveyor.Name;
                default: return "";
            }
        }

        private void ChangeRobotPictureBox(PictureBox pictureBox, HardwareStatus hardwareStatus)
        {
            switch (hardwareStatus)
            {
                case HardwareStatus.Offline: pictureBox.Image = Properties.Resources.robot_arm; 
                    return;
                case HardwareStatus.Operational: pictureBox.Image = Properties.Resources.robot_arm_green;
                    return;
                case HardwareStatus.Failed: pictureBox.Image = Properties.Resources.robot_arm_green;
                    return;
            }
        }

        private void ChangeCameraPictureBox(PictureBox pictureBox, HardwareStatus hardwareStatus)
        {
            switch (hardwareStatus)
            {
                case HardwareStatus.Offline: pictureBox.Image = Properties.Resources.camera;
                    return;
                case HardwareStatus.Operational: pictureBox.Image = Properties.Resources.camera_green;
                    return;
                case HardwareStatus.Failed: pictureBox.Image = Properties.Resources.camera_red;
                    return;
            }
        }

        private void ChangeConveyorPictureBox(PictureBox pictureBox, HardwareStatus hardwareStatus)
        {
            switch (hardwareStatus)
            {
                case HardwareStatus.Offline: pictureBox.Image = Properties.Resources.conveyor_belt;
                    return;
                case HardwareStatus.Operational: pictureBox.Image = Properties.Resources.conveyor_belt_green;
                    return;
                case HardwareStatus.Failed: pictureBox.Image = Properties.Resources.conveyor_belt_red;
                    return;
            }
        }

        #region Click Events

        private void imgPalletiserRobot_Click(object sender, EventArgs e)
        {

        }

        private void imgSorterCamera_Click(object sender, EventArgs e)
        {

        }

        private void imgSorterRobot_Click(object sender, EventArgs e)
        {

        }

        private void imgSorterConveyor_Click(object sender, EventArgs e)
        {

        }

        private void imgAssemblerRobot_Click(object sender, EventArgs e)
        {

        }

        private void imgAssemblerCamera_Click(object sender, EventArgs e)
        {

        }

        private void imgLoaderRobot_Click(object sender, EventArgs e)
        {

        }

        private void imgAssemblyConveyor_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
