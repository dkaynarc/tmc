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
                
                Sensor light = new Sensor();
                light.Name = "LightSensor";
                light.Channel = "ambience";
                light.IPAddress = "192.168.1.6";
                light.PortName = "9000";
                Sensor temperature = new Sensor();
                temperature.Name = "TemperatureSensor";
                temperature.Channel = "temperature";
                temperature.IPAddress = "192.168.1.6";
                temperature.PortName = "9000";
                Sensor sound = new Sensor();
                sound.Name = "SoundSensor";
                sound.Channel = "sound";
                sound.IPAddress = "192.168.1.6";
                sound.PortName = "9000";
                Sensor humidity = new Sensor();
                humidity.Name = "HumiditySensor";
                humidity.Channel = "humidity";
                humidity.IPAddress = "192.168.1.6";
                humidity.PortName = "9000";
                Sensor dust = new Sensor();
                dust.Name = "DustSensor";
                dust.Channel = "dust";
                dust.IPAddress = "192.168.1.6";
                dust.PortName = "9000";

                light.Initialise(); //connection 2
                temperature.Initialise(); //connection 1
                humidity.Initialise(); //connection 1
                sound.Initialise(); //connection 1
                dust.Initialise();
                

                while(true)
                {
                   Console.Write("Light: " + light.getData());
                   Console.Write(", Tempe: " + temperature.getData());
                   Console.Write(", Sound: " + sound.getData());
                   Console.Write(", Humidity: " + humidity.getData());
                   Console.WriteLine(", Dust: " + dust.getData());
                   // Console.WriteLine(temperature.GetStatus());
                   // Console.Write("Dust " + dust.getData() + " ");
                   // Console.WriteLine(dust.GetStatus());
                    //Console.ReadLine();
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
