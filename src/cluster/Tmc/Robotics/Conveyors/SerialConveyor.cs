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
        private string _portName;
        private int _position;

        public HardwareStatus GetStatus()
        {
            throw new NotImplementedException();
        }

        public void Initialise()
        {
            _position = 0;

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
            switch(_position)
            {
                case 0:
                    Conveyor.rightToMiddle();
                    _position++;
                    break;
                case 1:
                    Conveyor.middleToLeft();
                    _position++;
                    break;
                case 2:
                    throw new Exception("Conveyor is at its most forward position");
            }
        }

        public void MoveBackward()
        {
            switch(_position)
            {
                case 0:
                    throw new Exception("Conveyor is at its most backward position");
                case 1:
                    Conveyor.middleToRight();
                    _position--;
                    break;
                case 2:
                    Conveyor.leftToMiddle();
                    _position--;
                    break;
            }
        }
    }
}
