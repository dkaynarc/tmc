#include <iostream>
#include <stdlib.h>
#include <stdio.h>
#include <sstream>
#include <stdint.h>
#include <unistd.h>

using namespace std;

uint16_t get_i2c_temperature(int address);
string getTemperature(double temperature);

int main()
{
	for (;;)
	{
		double a = get_i2c_temperature(0x48);
		//string s = getTemperature(a);
		//cout << a << endl;
		cout << a * 0.0626 << endl;
		//cout << s << endl;
		//sleep(1);
	}
	
	return 0;
}

string getTemperature(double temperature)
{
	temperature *= 0.0625;
	ostringstream strs;
	strs << temperature;
	string str = strs.str();
	return str;
}

uint16_t get_i2c_temperature(int address)
{
	// Setup the command to access i2c port
	stringstream ss;
	ss << address <<' ';
	string addr;
	ss >> addr;
	string command = "i2cget -y 1 " + addr + " 0x00 w";
	
	//Issue the command to obtain the temperature data
	FILE *sys = popen(command.c_str(),"r");
	char buff[10];
	char *sensor = fgets(buff, sizeof(buff), sys);
	ss << hex << buff;
	uint16_t abc;
	ss >> abc;
	
	// Swap the MSB and LSB
	uint16_t cba = ((abc >> 8) & 0x00ff) | ((abc << 8) & 0xff00) >> 4;

	// Close the device pointer
	pclose(sys);
	return cba;
}
