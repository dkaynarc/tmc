#include "TMCSensor.h"


HumiditySensor::HumiditySensor() {humidity = 0;}
DustSensor::DustSensor() {concentration = 1;}
AmbienceSensor::AmbienceSensor() {};
SoundSensor::SoundSensor() {};
TemperatureSensor::TemperatureSensor() {};


/**
 *  \brief Take measurement of the humidity of environment
 *  
 *  \return String of the measured relative humidity
 *  
 *  \details 	This function will attempt to measure the humidity and return in string format.
 *  			The measurement unit is in percentage, from 0 up to 100 percentage.
 */
std::string HumiditySensor::getData()
{
	//float humidity = 50;
	//int temperature;
	//while (!obtainData());
	//humidity = (float)(DHT22_value[0] * 256 + DHT22_value[1]) / 10.0;
	//printf("Humidity: %.2f%\n", humidity);
	//temperature = (DHT22_value[2] & 0x7F) * 256 + DHT22_value[3];
	//wiringPiSetup();
	obtainData();
	std::string dataString;
	std::stringstream ss;
	
	ss << humidity;
	ss >> dataString;
	
	return dataString;
}

/**
 *  \brief Attempt to return the latest result of the dust concentration of environment.
 *  
 *  \return String of the measured dust concentration.
 *  
 *  \details This function will attempt to retrieve the latest measured result and return it in string format.
 */

std::string DustSensor::getData()
{
	std::string dataString;
	std::stringstream ss;
	
	if (concentration <= 0.62)
		concentration = -1;
		
	ss << concentration;
	ss >> dataString;

	return dataString;
}

/**
 *  \brief Take the measurement of the dust concentration of the environment.
 *  
 *  \return No return value.
 *  
 *  \details	This function will constantly measure the dust concentration of the environment
 *  			and store it to the "concentration" buffer. The measurement unit is in number of
 *  			particles in cubic meter.
 */

void DustSensor::obtainData()
{
	//wiringPiSetup();
	int pin = 0; // GPIO pin 17 (Wiring Pi #0)
	unsigned long duration;
	unsigned long starttime;
	unsigned long sampletime_ms = 15000;
	unsigned long lowpulseoccupancy = 0;
	float ratio = 0;
	//float concentration = 0;
	starttime = millis();
	//std::cout<<"Start time: "<<starttime << std::endl;
	pinMode(0,INPUT);

	while(true)
	{
		duration = (long)pulseIn(pin, LOW, 1000000);
		//cout<<"duration "<<duration<<endl;
		lowpulseoccupancy = lowpulseoccupancy+duration;

		if ((millis()-starttime) > sampletime_ms)
		{
			ratio = lowpulseoccupancy/(sampletime_ms*10.0);  // Integer percentage 0=>100
			concentration = 1.1*pow(ratio,3)-3.8*pow(ratio,2)+520*ratio+0.62; // using spec sheet curve
			
			//cout <<  lowpulseoccupancy << endl;
			//cout <<  ratio << endl;
			//std::cout <<  concentration << std::endl;
			// std::string dataString;
			// std::stringstream ss;
			
			// ss << concentration;
			// ss >> dataString;
	
			// return dataString;
			//break;
			lowpulseoccupancy = 0;
			starttime = millis();
		}	
	}
	//return 0;
}
/**
 *  \brief Take the measurement of the light intensity of the environment.
 *  
 *  \param [in] a2d Object of ADC configuration setting.
 *  \return String of the light intensity of the environment.
 *  
 *  \details 	This function will take the measurement of the light intensity of environment. The measurement unit
 *  			is in luminosity.
 */
std::string AmbienceSensor::getData(mcp3302 a2d) 
{
    //mcp3302 a2d("/dev/spidev0.0", SPI_MODE_0, 1250000, 8);
	int a2dVal = 0;
    int a2dChannel = 0;
	double lux;
	double vOut;
    unsigned char data[3];
	
	data[0] = 0x0C;  //  first byte transmitted -> start bit
	data[1] = 0x80;
	data[2] = 0;
	//data[1] = 0b10010000 |( ((a2dChannel & 7) << 4)); // second byte transmitted -> (SGL/DIF = 1, D2=D1=D0=0)
	//data[2] = 0; // third byte transmitted....don't care

	a2d.spiWriteRead(data, sizeof(data) );
	a2dVal = 0;
	a2dVal = (data[1] << 8) & 0b111100000000;
	//a2dVal = (data[1]<< 8) & 0b1100000000; //merge data[1] & data[2] to get result
	a2dVal |=  (data[2] & 0xff);

	//vOut = a2dVal * 5.0 / 512.0;
	vOut = a2dVal * 4.9 / 4096.0;
	lux = ((2500 / vOut - 500) / 3.3) / 2;

	std::string dataString;
	
	std::stringstream ss;
	
	ss<<lux;
	ss>>dataString;
	
	return dataString;
    
}

/**
 *  \brief Take the measurement of the sound intensity of environment.
 *  
 *  \param [in] a2d Object of ADC configuration setting.
 *  \return The sound intensity in string format. The measurement unit is in decibel.
 *  
 *  \details This function will attempt to take the measurement of the sound intensity of environment.
 */

std::string SoundSensor::getData(mcp3302 a2d)
{
	int value = obtainData(a2d);
	while (value == 0)
		value = obtainData(a2d);
	double vOut = value * 4.9 / 4096.0;
	double sound_dB = 20 * log10(vOut * 191) + 26;
	std::string dataString;
	
	std::stringstream ss;
	
	ss << sound_dB;
	ss >> dataString;
	
	return dataString;
}

/**
 *  \brief Take the measurement of the temperature of environment.
 *  
 *  \param [in] i2c_fd I2C file descriptor to the TMP102.
 *  \return String of temperature of environment. The measurement is in Celsius (*C)
 *  
 *  \details The function will take the measurement of the temperature of environment.
 */

std::string TemperatureSensor::getData(int i2c_fd)
{
	//int i2c_fd = wiringPiI2CSetup(I2C_TEMP_ADDRESS);
	int celcius = wiringPiI2CReadReg16(i2c_fd, I2C_TEMP_REGISTER_ADDRESS);
	
	std::stringstream ss;
	std::string dataString;
	ss << celcius;
	ss >> dataString;
	
	return dataString;
}

/**
 *  \brief Reads a pulse (either HIGH or LOW) on a pin
 *  
 *  \param [in] pin Pin number on which you want to read the pulse.
 *  \param [in] level Type of pulse to read: either HIGH or LOW.
 *  \param [in] timeout Number of microseconds to wait for the pulse to start.
 *  \return the length of the pulse (in microseconds) or 0 if no pulse started before the time-out.
 *  
 *  \details 	Reads a pulse (either HIGH or LOW) on a pin. For example, if value is HIGH, pulseIn() 
 *  			waits for the pin to go HIGH, starts timing, then waits for the pin to go LOW and stops timing. 
 *  			Returns the length of the pulse in microseconds. Gives up and returns 0 if no pulse starts within 
 *  			a specified time out.
 */

int DustSensor::pulseIn(int pin, int level, int timeout)
{
   struct timeval tn, t0, t1;

   long micros;

   gettimeofday(&t0, NULL);

   micros = 0;

   while (digitalRead(pin) != level)
   {
      gettimeofday(&tn, NULL);

      if (tn.tv_sec > t0.tv_sec) 
		micros = 1000000L; 
	  else 
		micros = 0;
      micros += (tn.tv_usec - t0.tv_usec);

      if (micros > timeout) 
		return 0;
   }

   gettimeofday(&t1, NULL);

   while (digitalRead(pin) == level)
   {
      gettimeofday(&tn, NULL);

      if (tn.tv_sec > t0.tv_sec) 
		micros = 1000000L; 
	  else micros = 0;
		micros = micros + (tn.tv_usec - t0.tv_usec);

      if (micros > timeout) 
		return 0;
   }

   if (tn.tv_sec > t1.tv_sec) 
	micros = 1000000L;
   else 
	micros = 0;
   micros = micros + (tn.tv_usec - t1.tv_usec);

   return micros;
}

/**
 *  \brief Take the measurement of humidity in environment.
 *  
 *  \return No return value.
 *  
 *  \details This function will take the measurement of humidity.
 */

void HumiditySensor::obtainData()
{
	// Some variables to be used to take measurement
	//wiringPiSetup();
	uint8_t last_state = HIGH;  
	uint8_t counter = 0;  
	uint8_t j = 0;
	uint8_t i;   
	//static float humidity_prev = 0;
	//delay(MAX_TIME);
	// Initialise DHT22 Rx array
	//for (i = 0; i <= sizeof(DHT22_value)/DHT22_value[0]; i++)
	//	DHT22_value[i] = 0;
	for (i = 0; i < 5; i++)
		 DHT22_value[i] = 0;
	
	// Generate the START condition signal
	pinMode(DHT11PIN, OUTPUT);  
	digitalWrite(DHT11PIN, LOW);  
	delay(10);  
	digitalWrite(DHT11PIN, HIGH);  
	delayMicroseconds(40);  
	pinMode(DHT11PIN, INPUT);
	
	for (i=0;i<MAX_TIME;i++)  
	{  
		counter = 0;  
		while (digitalRead(DHT11PIN) == last_state)
		{  
		  counter++;  
		  delayMicroseconds(1);  
		  if (counter == 255)  
			break;  
		}  
		last_state = digitalRead(DHT11PIN);
		if(counter == 255)  
		   break;

		// Top 3 transitions are ignored  
		if ((i >= 4) && (i%2 == 0))
		{  
		  DHT22_value[j/8] <<= 1;  
		  if (counter > 16)  
			DHT22_value[j/8] |= 1; 
			
		  j++;  
		}  
	}
	// Verify checksum and print the verified data  
	if ((j >= 40) && (DHT22_value[4] == ((DHT22_value[0] + DHT22_value[1] + DHT22_value[2] + DHT22_value[3])& 0xFF)))
	{
		humidity_prev = (float)(DHT22_value[0] * 256 + DHT22_value[1]) / 10.0;
		if (humidity_prev >= 1)
			humidity = humidity_prev;
	}
		
	// else
		// return;
	
	//	printf("Invalid Data!!\n");
}


/**
 *  \brief Take the measurement of sound intensity.
 *  
 *  \param [in] a2d Object of ADC configuration setting.
 *  \return Raw measured result of sound intensity of environment.
 *  
 *  \details This function will take attempt to take the measurement of the sound intensity of environment.
 */
int SoundSensor::obtainData(mcp3302 a2d) 
{
    //mcp3302 a2d("/dev/spidev0.0", SPI_MODE_0, 1250000, 8);
	int a2dVal = 0;
    //int a2dChannel = 0;
    unsigned char data[3];
	
	data[0] = 0x0C;  //  first byte transmitted -> start bit
	data[1] = 0x00;
	//data[1] = 0b10000000 |( ((a2dChannel & 7) << 4)); // second byte transmitted -> (SGL/DIF = 1, D2=D1=D0=0)
	data[2] = 0; // third byte transmitted....don't care

	a2d.spiWriteRead(data, sizeof(data) );
	a2dVal = 0;
	a2dVal = (data[1] << 8) & 0b111100000000;
	//a2dVal = (data[1]<< 8) & 0b1100000000; //merge data[1] & data[2] to get result
	a2dVal |=  (data[2] & 0xff);
/*
	double vOut = a2dVal * 4.9 / 4096.0;
	double sound_dB = 20 * log10(vOut * 191) + 26;
	string dataString;
	
	stringstream ss;
	
	ss<<a2dVal;
	ss>>dataString;
	*/
	return a2dVal;
    
}
