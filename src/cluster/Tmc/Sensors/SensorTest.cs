using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Sensors
{
    class SensorTest
    {
        public static void Main()
        {

            try
            {
                /*
                Sensor test = new Sensor();
                test.Name = "TemperatureSensor";
                test.Channel = "temperature";
                test.IPAddress = "172.19.200.66";
                test.PortName = "9000";
            
                test.Initialise(); //connection 1
                float result = test.getData();
                Console.WriteLine("Temperature Result " + result);

                //test.Shutdown();
                
                Sensor test1 = new Sensor();
                test1.Name = "LightSensor";
                test1.Channel = "ambience";
                test1.IPAddress = "172.19.200.66";
                test1.PortName = "9000";

                test1.Initialise(); //connection 2
                float result1 = test1.getData();
                Console.WriteLine("Light Result " + result1);

                //test1.Shutdown();
                */
                
                Sensor test2 = new Sensor();
                test2.Name = "LightSensor";
                test2.Channel = "ambience";
                test2.IPAddress = "172.19.200.66";
                test2.PortName = "9001";
                Sensor test = new Sensor();
                test.Name = "TemperatureSensor";
                test.Channel = "temperature";
                test.IPAddress = "172.19.200.66";
                test.PortName = "9001";
                Sensor test1 = new Sensor();
                test1.Name = "SoundSensor";
                test1.Channel = "sound";
                test1.IPAddress = "172.19.200.66";
                test1.PortName = "9001";
                Sensor test3 = new Sensor();
                test3.Name = "DustSensor";
                test3.Channel = "dust";
                test3.IPAddress = "172.19.200.66";
                test3.PortName = "9001";

                test2.Initialise(); //connection 2
                test.Initialise(); //connection 1
                test1.Initialise(); //connection 1
                test3.Initialise(); //connection 1
                

                while(true)
                {
                    float result2 = test2.getData();
                    Console.WriteLine("Light Result " + result2);
                    float result = test.getData();
                    Console.WriteLine("Temperature Result " + result);
                    float result1 = test1.getData();
                    Console.WriteLine("Sound Result " + result1);
                    float result3 = test3.getData();
                    Console.WriteLine("Dust Result " + result3);
                    Console.ReadLine();
                }

                
                //test2.Shutdown();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }








            Console.ReadLine();
        }
    }
}
