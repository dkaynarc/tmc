#include "AmbienceSensor.h"
#include "SoundSensor.h"


using namespace std;

int main() {
	AmbienceSensor light;
	SoundSensor sound;
	string data[2];
	while (true) 
	{

		data[0] = light.getData();
		data[1] = sound.getData();
		cout << data[1] << "\n";
		sleep(1);
		//fflush(data);
	}

	return 0;
}

