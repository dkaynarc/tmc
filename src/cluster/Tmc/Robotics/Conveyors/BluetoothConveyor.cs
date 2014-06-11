using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using Tmc.Common;

namespace Tmc.Robotics
{
    public class BluetoothConveyor : IConveyor
    {
        private struct Protocol
        {
            public const string MoveForward = "s";
            public const string MoveBackward = "a";
        }

        public string Name { get; set; }
        public string PortName { get; set; }
        public int WaitTime { get; set; }
        public ConveyorPosition Position { get; set; }

        private HardwareStatus _status;

        private SerialPort _serialPort;

        public BluetoothConveyor()
        {
            _serialPort = new SerialPort();
            _status = HardwareStatus.Offline;
            this.Position = ConveyorPosition.Right;
        }

        public HardwareStatus GetStatus()
        {
            return _status;
        }

        public void Initialise()
        {
            if (SerialPort.GetPortNames().Contains(PortName))
            {
                _serialPort.PortName = PortName;
                if (!_serialPort.IsOpen)
                {
                    try
                    {
                        _serialPort.Open();
                        _status = HardwareStatus.Operational;
                    }
                    catch (Exception ex)
                    {
                        _status = HardwareStatus.Failed;
                        throw ex;
                    }
                }
            }
            else
            {
                _status = HardwareStatus.Failed;
                throw new InvalidOperationException("Serial port name " + PortName + "does not exist");
            }
        }

        public void Shutdown()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            _status = HardwareStatus.Offline;
        }

        public void EmergencyStop()
        {

        }

        public void MoveForward()
        {
            _serialPort.Write(Protocol.MoveForward);
            _serialPort.Write(Protocol.MoveForward);
            Thread.Sleep(WaitTime);
            this.Position = ConveyorPosition.Left;
        }

        public void MoveBackward()
        {
            _serialPort.Write(Protocol.MoveBackward);
            _serialPort.Write(Protocol.MoveBackward);
            Thread.Sleep(WaitTime);
            this.Position = ConveyorPosition.Right;
        }

        public void SetParameters(Dictionary<string, string> parameters)
        {
            string s = "";
            if (parameters.TryGetValue("Name", out s))
            {
                this.Name = s;
            }
            if (parameters.TryGetValue("PortName", out s))
            {
                this.PortName = s;
            }
            int waitTime = 0;
            if (parameters.TryGetValue("WaitTime", out s))
            {
                if (int.TryParse(s, out waitTime))
                {
                    this.WaitTime = waitTime;
                }
            }
        }

        
    }
}
