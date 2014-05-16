#include <iostream>
#include <stdlib.h>
#include <stdio.h>
#include <sstream>
#include <stdint.h>
#include <unistd.h>

class TemperatureSensor {
	public:
		TemperatureSensor();	// constructor; initialize the temperature sensor
		std::string getData();
		
	private:
		uint16_t get_i2c_temperature(int address);
		std::string getTemperature(double temperature);
				
};