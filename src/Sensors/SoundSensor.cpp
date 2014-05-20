#include "SoundSensor.h"
#include <sstream>

using namespace std;

SoundSensor::SoundSensor() {};

// Sound sensor is picking up data from CH0 from MCP3302

string SoundSensor::getData() 
{
    mcp3008Spi a2d("/dev/spidev0.0", SPI_MODE_0, 1000000, 8);
	int a2dVal = 0;
    int a2dChannel = 0;
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

	double vOut = a2dVal * 5.0 / 4096.0;
	double sound_dB = 20 * log (vOut * 238);
	string dataString;
	
	stringstream ss;
	
	ss<<sound_dB;
	ss>>dataString;
	
	return dataString;
    
}


