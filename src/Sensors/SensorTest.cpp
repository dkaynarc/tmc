#include <iostream>
#include "TMCSensor.h"

// Sensor Data Array
std::string 	SensorData[5];

// Sensor Testing Objects
HumiditySensor 		humidity;
DustSensor			dust;
AmbienceSensor		ambience;
SoundSensor			sound;
TemperatureSensor	temperature;
int main(void)
{
	
	if (wiringPiSetup() == -1)
		exit(1);
	for (;;)
	{
		SensorData[0] = humidity.getData();
		std::cout << "Humidity: " << SensorData[0] << " %"<< std::endl;
		//SensorData[1] = dust.getData();
		SensorData[2] = ambience.getData();
		std::cout << "Light: " << SensorData[2] << " lux" << std::endl;
		SensorData[3] = sound.getData();
		std::cout << "Sound: " << SensorData[3] << " dB" << std::endl;
		//std::cout << SensorData[3] << std::endl;
		//SensorData[4] = temperature.getData();
		//for (int i = 0; i < 3; i++)
			//std::cout << SensorData[0] << std::endl;
		
		delay(1000);
		std::cout << "--------------------------------------------" << std::endl;
	}
	
	return 0;
}