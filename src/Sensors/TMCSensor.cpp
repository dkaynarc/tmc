#include "TMCSensor.h"


HumiditySensor::HumiditySensor() {humidity = 0;}
DustSensor::DustSensor() {concentration = 0;}
AmbienceSensor::AmbienceSensor() {};
SoundSensor::SoundSensor() {};
TemperatureSensor::TemperatureSensor() {};

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

std::string DustSensor::getData()
{
	//wiringPiSetup();
	int pin = 0; // GPIO pin 17 (Wiring Pi #0)
	unsigned long duration;
	unsigned long starttime;
	unsigned long sampletime_ms = 15000;
	unsigned long lowpulseoccupancy = 0;
	float ratio = 0;
	float concentration = 0;
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
			std::string dataString;
			std::stringstream ss;
			
			ss << concentration;
			ss >> dataString;
	
			return dataString;
			//break;
			//lowpulseoccupancy = 0;
			//starttime = millis();
		}	
	}
	//return 0;
}

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
		
	else
		return;
	
	//	printf("Invalid Data!!\n");
}

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
