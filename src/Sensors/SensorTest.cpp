#include <stdlib.h>
#include "TemperatureSensor.cpp"
#include <iostream>
#include <stdlib.h>
#include <stdio.h>
#include <sstream>
#include <stdint.h>
#include <unistd.h>

using namespace std;

int main()
{
	TemperatureSensor temperature;
	string data = temperature.getData();
	cout << data << endl;
	
	return 0;
}