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
                _tcpClient = new TcpClient(this.IPAddress, Convert.ToInt32(this.PortName)); 
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
                throw new InvalidOperationException("No connection IP address passed to sensors");
            }






            if (parameters.TryGetValue("IPAddress", out s))
            {
                this.ConnectionIP = s;
            }
            else
            {
                throw new InvalidOperationException("No connection IP address passed to sensors");
            }

            if (parameters.TryGetValue("ConnectionPort", out s))
            {
                this.ConnectionPort = Convert.ToInt32(s); // make sure use try parse
            }
            else
            {
                throw new InvalidOperationException("No connection Port number passed to sensors");
            }
            
        }
/*
        public float getData(SensorDevices sensors)
        {
            float data = -100;                                          //Arbitary large negative float to indicate an error from sensory devices
            try
            {   
                switch(sensors)
                {
                    case SensorDevices.Temperature:
                        streamWriter.WriteLine("temperature");
                        break;
                    case SensorDevices.Ambience:
                        streamWriter.WriteLine("ambience");
                        break;
                    case SensorDevices.DustParticle:
                        streamWriter.WriteLine("dust");
                        break;
                    case SensorDevices.Humidity:
                        streamWriter.WriteLine("humidity");
                        break;
                    case SensorDevices.Sound:
                        streamWriter.WriteLine("sound");
                        break;
                    default:
                        streamWriter.WriteLine("unknown");              
                        return data;
                        
                }

                streamWriter.Flush();                               // Flush WriterStream data to network stream (i.e. sending to Raspberry Pi)        

                string result = null;
                result = streamReader.ReadLine();                   // Reading input data from Raspberry Pi

                if (result != null)                                 
                {
                    data = Convert.ToSingle(result);                
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
                Console.WriteLine("Error recieved: " + e);
                return data;
            }

        }
 * */
    }
}


