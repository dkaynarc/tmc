using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Tmc.Common;

namespace Tmc.Robotics
{
    internal class BaseRobot : IRobot
    {
        public string Name { get; set; }
        public IPAddress RobotIPAddress { get; set; }

        protected Task Task;
        protected Controller Controller;
        protected Mastership Mastership;

        private EventWaitHandle eventWait = new AutoResetEvent(false);

        public BaseRobot() { }

        public HardwareStatus GetStatus()
        {
            throw new NotImplementedException();
        }

        public void Initialise()
        {
            var networkScanner = new NetworkScanner();

            for(int i = 0; i < 5; i++)
            {
                Thread.Sleep(250);
                networkScanner.Scan();
            }

            foreach(ControllerInfo controller in networkScanner.Controllers)
            {
                if(controller.IPAddress.Equals(this.RobotIPAddress))
                {
                    this.Controller = ControllerFactory.CreateFrom(controller);
                    this.Controller.Rapid.ExecutionStatusChanged += new EventHandler<ExecutionStatusChangedEventArgs>(RapidExecutionStatusChanged);
                    return;
                }
            }

            throw new Exception(string.Format("Robot at {0} could not be found.", this.RobotIPAddress));
        }

        public void SetParameters(Dictionary<string, string> parameters)
        {
            IPAddress ip;
            if(IPAddress.TryParse(parameters["IPAddress"], out ip))
            {
                this.RobotIPAddress = ip;
                return;
            }

            throw new ArgumentException("Invalid IP Address for robot");
        }

        protected void RunRapidProgram(string filename)
        {
            if (this.Controller.OperatingMode != ControllerOperatingMode.Auto)
            {
                throw new Exception(string.Format("Robot {0} is not in autonomous mode", this.RobotIPAddress));
            }

            var filePath = Controller.FileSystem.LocalDirectory + "\\modFiles\\" + filename;

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("{0} does not exist.", filePath));
            }

            this.BeginControl();

            this.Controller.FileSystem.PutFile(filePath, filename, true);
            this.Task.DeleteProgram();
            this.Task.LoadModuleFromFile(filename, RapidLoadMode.Replace);
            this.Task.ResetProgramPointer();
            this.Task.Start();

            this.EndControl();

            this.eventWait.WaitOne();

            this.Controller.FileSystem.RemoveFile(filename);
        }

        private void BeginControl()
        {
            Controller.Logon(UserInfo.DefaultUser);
            Mastership = Mastership.Request(Controller.Rapid);
            Task = Controller.Rapid.GetTask("T_ROB1");
            Controller.FileSystem.RemoteDirectory = "(HOME)$";
        }

        private void EndControl()
        {
            Mastership.Dispose();
            Controller.Logoff();
        }

        private void RapidExecutionStatusChanged(object sender, ExecutionStatusChangedEventArgs args)
        {
            if (args.Status == ExecutionStatus.Stopped)
            {
                this.eventWait.Set();
            }
        }
    }
}
