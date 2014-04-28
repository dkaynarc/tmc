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
        public enum SensorDevices
        {
            Temperature,
            Ambience,
            Sound,
            DustParticle,
            Humidity
        }
        public string Name { get; set; }
        public string ConnectionIP { get; set; }
        public int ConnectionPort { get; set; }

        private HardwareStatus _hardwareStatus;

        TcpClient tcpClient;
        NetworkStream networkStream;
        StreamWriter streamWriter;
        StreamReader streamReader;

        public Sensor()
        {
            _hardwareStatus = HardwareStatus.Offline;
        }


        public void Shutdown()
        {
            try
            {
                tcpClient.Close();
                _hardwareStatus = HardwareStatus.Offline;
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
                _hardwareStatus = HardwareStatus.Failed;
            }
        }

        public HardwareStatus GetStatus()
        {
            return _hardwareStatus;
        }

        public void Initialise()
        {
            try
            {
                tcpClient = new TcpClient(this.ConnectionIP, this.ConnectionPort); 
                networkStream = tcpClient.GetStream();
                streamWriter = new StreamWriter(networkStream);
                streamReader = new StreamReader(networkStream);
                _hardwareStatus = HardwareStatus.Operational;
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
                _hardwareStatus = HardwareStatus.Failed;
            }
        }

        public void SetParameters(Dictionary<string, string> parameters)
        {
            
            string s = "";

            if (parameters.TryGetValue("Name", out s))
            {
                this.Name = s;
            }

            if (parameters.TryGetValue("ConnectionIP", out s))
            {
                this.ConnectionIP = s;
            }
            else
            {
                throw new InvalidOperationException("No connection IP address passed to sensors");
            }

            if (parameters.TryGetValue("ConnectionPort", out s))
            {
                this.ConnectionPort = Convert.ToInt32(s);
            }
            else
            {
                throw new InvalidOperationException("No connection Port number passed to sensors");
            }
            
        }

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
    }
}


