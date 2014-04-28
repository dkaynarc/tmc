using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Tmc.Sensors
{
    class Sensor : ISensor
    {
        public string Name { get; set; }
        public string Channel { get; set; }
        public string IPAddress { get; set; }
        public string PortName { get; set; }

        private HardwareStatus _hardwareStatus;

        TcpClient _tcpClient;

        NetworkStream _networkStream;

        StreamWriter _streamWriter;

        StreamReader _streamReader;

        /// <summary>
        ///  Creates a new Sensor class with hardware status to be 'Offline' as default
        /// </summary>
        public Sensor()
        {
            _hardwareStatus = HardwareStatus.Offline;
        }

        /// <summary>
        /// Shutdown TCP client connections with the Raspberry Pi
        /// </summary>
        public void Shutdown()
        {
            try
            {
                _tcpClient.Close();
                _hardwareStatus = HardwareStatus.Offline;
            }
            catch (SocketException e)
            {
                _hardwareStatus = HardwareStatus.Failed;
                throw new Exception("Error: Unable to shutdown TCP client: " + e);
            }
        }
        /// <summary>
        /// Return hardware status of the connectivity between SCADA and Raspberry Pi
        /// </summary>
        /// <returns>HardwareStatus as either Operational, Offline or Failed</returns>
        public HardwareStatus GetStatus()
        {
            return _hardwareStatus;
        }

        /// <summary>
        /// Initialises the network connection between SCADA and Raspberry Pi server with the 
        /// </summary>
        public void Initialise()
        {
            try
            {
                int number;
                if (Int32.TryParse(this.PortName, out number))
                {
                    _tcpClient = new TcpClient(this.IPAddress, number); 
                }
                else
                {
                    throw new Exception("Error: Portname is not an integer on initialise");
                }
                _networkStream = _tcpClient.GetStream();
                _streamWriter = new StreamWriter(_networkStream);
                _streamReader = new StreamReader(_networkStream);
                _hardwareStatus = HardwareStatus.Operational;
            }
            catch (SocketException ex)
            {
                _hardwareStatus = HardwareStatus.Failed;
                throw new Exception("Error: Unable to initialise TCP client: " + ex);
            }
        }

        /// <summary>
        /// Parse sensor configurations to sensor name, channel, IP address and Port number 
        /// </summary>
        /// <param name="parameters"></param>
        public void SetParameters(Dictionary<string, string> parameters)
        {
            
            string s = "";

            if (parameters.TryGetValue("Name", out s))
            {
                this.Name = s;
            }

            if (parameters.TryGetValue("Channel", out s))
            {
                this.Channel = s;
            }
            else
            {
                throw new InvalidOperationException("Error: No sensor channel passed to sensors");
            }

            if (parameters.TryGetValue("IPAddress", out s))
            {
                this.IPAddress = s;
            }
            else
            {
                throw new InvalidOperationException("Error: No connection IP address passed to sensors");
            }

            if (parameters.TryGetValue("ConnectionPort", out s))
            {
                this.PortName = s; 
            }
            else
            {
                throw new InvalidOperationException("Error: No connection Port number passed to sensors");
            }
            
        }

        /// <summary>
        /// Communicate with the TCP server (Raspberry Pi) to obtain sensory data information.
        /// The result is specific to the channel of the sensor as defined in setparameters()
        /// </summary>
        /// <returns>float: sensor data</returns>

        public float getData()
        {
            float data = 0;                                       
            try
            {   
                _streamWriter.Write(this.Channel);
                _streamWriter.Flush();                               // Flush WriterStream data to network stream (i.e. sending to Raspberry Pi)        

                string result = null;
                result = _streamReader.ReadLine();                   

                if (result != null)                                 
                {
                    float number;
                    if (Single.TryParse(result, out number))
                    {
                        data = number; 
                    }
                    else
                    {
                        throw new Exception("Error: The result is not a float");
                    }

                    _hardwareStatus = HardwareStatus.Operational;
                    return data;
                }
                else
                {
                    _hardwareStatus = HardwareStatus.Failed;
                    return data;

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Exception in getting data:" + e);
                return data;
            }

        }

    }
}


