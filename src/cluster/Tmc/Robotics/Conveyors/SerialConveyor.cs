using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tmc.Common;

namespace Tmc.Robotics
{
    [Name("SerialConveyor")]
    public class SerialConveyor : IConveyor
    {
        public string Name { get; set; }
        public ConveyorPosition Position { get; private set; }
        private string _portName;

        public HardwareStatus GetStatus()
        {
            throw new NotImplementedException();
        }

        public void Initialise()
        {
            Position = ConveyorPosition.Right;

            Conveyor.initialisePLC();
            Thread.Sleep(500);
            Conveyor.start(_portName);
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public void SetParameters(Dictionary<string, string> parameters)
        {
            string s = "";
            if (parameters.TryGetValue("Name", out s))
            {
                this.Name = s;
            }

            if(!parameters.TryGetValue("PortName", out s))
            {
                throw new Exception("Serial Conveyor requires a portname.");
            }
            else
            {
                _portName = s;
            }
        }

        public void MoveForward()
        {
            switch(Position)
            {
                case ConveyorPosition.Right:
                    Conveyor.rightToMiddle();
                    Position++;
                    break;
                case ConveyorPosition.Middle:
                    Conveyor.middleToLeft();
                    Position++;
                    break;
                case ConveyorPosition.Left:
                    throw new Exception("Conveyor is at its most forward position");
            }
        }

        public void MoveBackward()
        {
            switch(Position)
            {
                case ConveyorPosition.Right:
                    throw new Exception("Conveyor is at its most backward position");
                case ConveyorPosition.Middle:
                    Conveyor.middleToRight();
                    Position--;
                    break;
                case ConveyorPosition.Left:
                    Conveyor.leftToMiddle();
                    Position--;
                    break;
            }
        }
    }
}
