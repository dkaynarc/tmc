#include "HumiditySensor.h"
#include <sstream>

using namespace std;

HumiditySensor::HumiditySensor() {};

// Humidity Sensor  is picking up data from CH1 from MCP3302

string HumiditySensor::getData() 
{
    mcp3008Spi a2d("/dev/spidev0.0", SPI_MODE_0, 1000000, 8);
	int a2dVal = 0;
    int a2dChannel = 0;
    unsigned char data[3];
	
	data[0] = 1;  //  first byte transmitted -> start bit
	
	data[1] = 0b10100000 |( ((a2dChannel & 7) << 4)); // second byte transmitted -> (SGL/DIF = 1, D2=D1=D0=0)
	data[2] = 0; // third byte transmitted....don't care

	a2d.spiWriteRead(data, sizeof(data) );
	a2dVal = 0;
	a2dVal = (data[1]<< 8) & 0b1100000000; //merge data[1] & data[2] to get result
	a2dVal |=  (data[2] & 0xff);


	string dataString;
	
	stringstream ss;
	
	ss<<a2dVal;
	ss>>dataString;
	
	return dataString;
    
}


