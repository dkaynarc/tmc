#include "AmbienceSensor.h"
#include <sstream>

using namespace std;

AmbienceSensor::AmbienceSensor() {};

// Ambience sensor is picking up data from CH1 from MCP3302

string AmbienceSensor::getData() 
{
    mcp3008Spi a2d("/dev/spidev0.0", SPI_MODE_0, 1000000, 8);
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
	vOut = a2dVal * 5.0 / 4096.0;
	lux = ((2500 / vOut - 500) / 3.3) / 2;

	string dataString;
	
	stringstream ss;
	
	ss<<vOut;
	ss>>dataString;
	
	return dataString;
    
}


