#include <wiringPi.h>  
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

// Humidity sensor
#define MAX_TIME 85  	// Waiting time before the acquiring is expire
#define DHT11PIN 7		// Pin hard-wiring number: Equivalent to GPIO4

class HumiditySensor
{
	public:
		int DHT22_value[5];
		HumiditySensor();
		std::string getData();
	private:
		int obtainData();
};

class DustSensor
{
	public:
		DustSensor();
		std::string getData();
	private:
		int pulseIn(int pin, int level, int timeout);
};

class AmbienceSensor 
{
	public:
		AmbienceSensor();	
		std::string getData();
};

class SoundSensor 
{
	public:
		SoundSensor();	
		std::string getData();

	private:
		int obtainData();
};

class TemperatureSensor 
{
	public:
		TemperatureSensor();	// constructor; initialize the temperature sensor
		std::string getData();
		
	private:
		uint16_t get_i2c_temperature(int address);
		std::string getTemperature(double temperature);			
};