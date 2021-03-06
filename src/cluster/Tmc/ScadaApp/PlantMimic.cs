﻿using System;
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
        private const int ONE_SEC_IN_MILLISECS = 1000;
        private HardwareMonitor _hardwareMonitor;

        public enum Hardware
        {
            SorterRobot, AssemblerRobot, LoaderRobot, PalletiserRobot, SorterCamera, TrayVerifierCamera, SorterConveyor, AssemblyConveyor, MainPlc, TemperatureSensor, SoundSensor, DustSensor, LightSensor, HumiditySensor
        }

        List<IHardware> hardwareList;

        public PlantMimic()
        {
            InitializeComponent();
        }

        public void Initialise(HardwareMonitor hardwareMonitor)
        {
            _hardwareMonitor = hardwareMonitor;
            _hardwareMonitor.StatusChanged += Update;
        }

        private void Update(object sender, HardwareEventArgs args)
        {
            if (this.Enabled)
            {
                this.ChangeHardwareStatus((Hardware)Enum.Parse(typeof(Hardware), args.hardware.Name), args.hardware.GetStatus());
            }
        }

        /// <summary>
        /// Changes a machine's status on the UI based on its status.
        /// </summary>
        /// <param name="hardware">The hardware to change.</param>
        /// <param name="hardwareStatus">The status to change to.</param>
        public void ChangeHardwareStatus(Hardware hardware, HardwareStatus hardwareStatus)
        {
            PictureBox pictureBox;
            string pictureBoxName = this.GetHardwarePictureBoxName(hardware);
            var pictureBoxCollection = !string.IsNullOrWhiteSpace(pictureBoxName) ? this.pnlContainer.Controls.Find(pictureBoxName, false) : null;

            if (pictureBoxCollection == null)
            {
                return;
            }

            pictureBox = pictureBoxCollection.FirstOrDefault(x => x.Name == pictureBoxName) as PictureBox;

            if (hardware == Hardware.SorterRobot || hardware == Hardware.LoaderRobot
                || hardware == Hardware.AssemblerRobot || hardware == Hardware.PalletiserRobot)
            {
                this.ChangeRobotPictureBox(pictureBox, hardwareStatus);
            }
            else if (hardware == Hardware.SorterCamera || hardware == Hardware.TrayVerifierCamera)
            {
                this.ChangeCameraPictureBox(pictureBox, hardwareStatus);
            }
            else if (hardware == Hardware.AssemblyConveyor)
            {
                this.ChangeAssemblyConveyorPictureBox(pictureBox, hardwareStatus);
            }
            else if (hardware == Hardware.SorterConveyor)
            {
                this.ChangeSorterConveyorPictureBox(pictureBox, hardwareStatus);
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
                case Hardware.TrayVerifierCamera: return this.imgAssemblerCamera.Name;
                case Hardware.SorterConveyor: return this.imgSorterConveyor.Name;
                case Hardware.AssemblyConveyor: return this.imgAssemblyConveyor.Name;
                default: return "";
            }
        }

        private void ChangeRobotPictureBox(PictureBox pictureBox, HardwareStatus hardwareStatus)
        {
            switch (hardwareStatus)
            {
                case HardwareStatus.Offline: pictureBox.Image = Properties.Resources.robot_off; 
                    return;
                case HardwareStatus.Operational: pictureBox.Image = Properties.Resources.robot_on;
                    return;
                case HardwareStatus.Failed: pictureBox.Image = Properties.Resources.robot_error;
                    return;
            }
        }

        private void ChangeCameraPictureBox(PictureBox pictureBox, HardwareStatus hardwareStatus)
        {
            switch (hardwareStatus)
            {
                case HardwareStatus.Offline: pictureBox.Image = Properties.Resources.camera_off;
                    return;
                case HardwareStatus.Operational: pictureBox.Image = Properties.Resources.camera_on;
                    return;
                case HardwareStatus.Failed: pictureBox.Image = Properties.Resources.camera_error;
                    return;
            }
        }

        private void ChangeAssemblyConveyorPictureBox(PictureBox pictureBox, HardwareStatus hardwareStatus)
        {
            switch (hardwareStatus)
            {
                case HardwareStatus.Offline: pictureBox.Image = Properties.Resources.assembly_conveyor_off;
                    return;
                case HardwareStatus.Operational: pictureBox.Image = Properties.Resources.assembly_conveyor_on;
                    return;
                case HardwareStatus.Failed: pictureBox.Image = Properties.Resources.assembly_conveyor_error;
                    return;
            }
        }

        private void ChangeSorterConveyorPictureBox(PictureBox pictureBox, HardwareStatus hardwareStatus)
        {
            switch (hardwareStatus)
            {
                case HardwareStatus.Offline: pictureBox.Image = Properties.Resources.sorter_conveyor_off;
                    return;
                case HardwareStatus.Operational: pictureBox.Image = Properties.Resources.sorter_conveyor_on;
                    return;
                case HardwareStatus.Failed: pictureBox.Image = Properties.Resources.sorter_conveyor_error;
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
