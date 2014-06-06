#include <iostream>
#include "TMCSensor.h"

// Sensor Data Array
std::string 	SensorData;

// Sensor Testing Objects
HumiditySensor 		humidity;
DustSensor			dust;
AmbienceSensor		ambience;
SoundSensor			sound;
TemperatureSensor	temperature;
mcp3302				a2d;
//mcp3302 a2d("/dev/spidev0.0", SPI_MODE_0, 1250000, 8);
int main(void)
{
	//a2d("/dev/spidev0.0", SPI_MODE_0, 1250000, 8);
	int i2c_fd = wiringPiI2CSetup(I2C_TEMP_ADDRESS);
	if (wiringPiSetup() == -1)
		exit(1);
	for (;;)
	{
		 //SensorData = humidity.getData();
		 std::cout 	<< "{" <<	"Humidity: " << humidity.getData() << " %, " <<\
								"Light: " << ambience.getData(a2d) << " lux, " <<\
								"Sound: " << sound.getData(a2d) << " dB, " <<\
								"Temperature: " <<temperature.getData(i2c_fd) << "*C, " <<\
								"Dust: " << "0"  <<\
						"}" << std::endl;
		delay(10);
	}
	
	return 0;
}