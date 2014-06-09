#include <wiringPi.h>  
#include <wiringPiI2C.h>
#include <stdio.h>  
#include <stdlib.h>  
#include <stdint.h>
#include <sstream>
#include <iostream>
#include <cmath>
#include <math.h>
#include <sys/time.h>
#include <time.h>
#include <string>
#include "mcp3302.h"

// Humidity sensor
#define MAX_TIME 85  	// Waiting time before the acquiring is expire
#define DHT11PIN 7		// Pin hard-wiring number: Equivalent to GPIO4
#define I2C_TEMP_REGISTER_ADDRESS 0x00
#define I2C_TEMP_ADDRESS 0x48

class HumiditySensor
{
	public:
		int DHT22_value[5];
		float humidity;
		float humidity_prev;
		HumiditySensor();
		std::string getData();
	private:
		void obtainData();
};

class DustSensor
{
	public:
		float concentration;
		int isComplete;
		DustSensor();
		std::string getData();
		void obtainData();
		//std::string getConcentration();
	private:
		int pulseIn(int pin, int level, int timeout);
};

class AmbienceSensor
{
	public:
		AmbienceSensor();	
		std::string getData(mcp3302 a2d);
	//private:
	//	mcp3302 a2d("/dev/spidev0.0", SPI_MODE_0, 1250000, 8);
};

class SoundSensor
{
	public:
		SoundSensor();	
		std::string getData(mcp3302 a2d);

	private:
		int obtainData(mcp3302 a2d);
		//mcp3302 a2d("/dev/spidev0.0", SPI_MODE_0, 1250000, 8);
};

class TemperatureSensor 
{
	public:
		TemperatureSensor();	// constructor; initialize the temperature sensor
		std::string getData(int i2c_fd);
};