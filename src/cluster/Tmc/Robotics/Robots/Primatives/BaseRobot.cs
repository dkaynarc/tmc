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
    public abstract class BaseRobot : IRobot
    {
        public string Name { get; set; }
        public IPAddress RobotIPAddress { get; set; }

        protected Task Task;
        protected Controller Controller;
        protected Mastership Mastership;

        private EventWaitHandle _eventWait = new AutoResetEvent(false);

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

        public void Shutdown()
        {

        }

        public void SetParameters(Dictionary<string, string> parameters)
        {
            string param;
            IPAddress ip;

            if(!parameters.TryGetValue("IPAddress", out param))
            {
                throw new Exception("Robots require an IPAddress parameter");
            }

            if(!IPAddress.TryParse(param, out ip))
            {
                throw new ArgumentException("Invalid IP Address for robot");
            }

            if(parameters.TryGetValue("Name", out param))
            {
                this.Name = param;
            }

            this.RobotIPAddress = ip;
        }

        protected void RunRapidProgram(string filename)
        {
            if (this.Controller.OperatingMode != ControllerOperatingMode.Auto)
            {
                throw new Exception(string.Format("Robot {0} is not in autonomous mode", this.RobotIPAddress));
            }

            var filePath = Controller.FileSystem.LocalDirectory + "\\mod\\" + filename;

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

            this._eventWait.WaitOne();

            this.Controller.FileSystem.RemoveFile(filename);
        }

        protected void RunRapidProgram(string filename, IDictionary<string, string> parameters)
        {
            var directory = Directory.GetCurrentDirectory() + "\\mod\\";
            var text = File.ReadAllText(directory + filename);

            foreach(var parameter in parameters)
            {
                text = text.Replace(parameter.Key, parameter.Value);
            }

            File.WriteAllText(directory + "_temp.mod", text);

            this.RunRapidProgram("_temp.mod");
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
                this._eventWait.Set();
            }
        }
    }
}
