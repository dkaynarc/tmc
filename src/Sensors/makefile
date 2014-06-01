CC = g++
#CFLAGS = -Wall
LDFLAGS	= -lwiringPi
INCL	= -I
BIN_DIR = .
SENSORS_TEST = TMCSensorTest
SERVER_TEST = TMCSensorServerTest


buildServerTest:  mcp3302.o TMCSensor.o ServerTest.o 
	$(CC) $? $(LDFLAGS) -lboost_system -lboost_thread -o  $(SERVER_TEST)

buildSensorTest:  mcp3302.o SensorTest.o TMCSensor.o
	$(CC) $? $(LDFLAGS) -o  $(SENSORS_TEST)

TMCSensor.o: TMCSensor.cpp TMCSensor.h
	$(CC) -c TMCSensor.cpp $(LDFLAGS) $(INCL)/usr/local/wiringPi -o $@	

SensorTest.o: SensorTest.cpp TMCSensor.h
	$(CC) -c SensorTest.cpp $(LDFLAGS) $(INCL)/usr/local/wiringPi -o $@  
	
ServerTest.o: TMCSnesorServer.cpp TMCSensor.h
	$(CC) -c TMCSnesorServer.cpp $(LDFLAGS) -lboost_system -lboost_thread $(INCL)/usr/local/wiringPi $(INCL)/home/pi/boost_1_55_0 -o $@
	
mcp3302.o: mcp3302.cpp mcp3302.h
	$(CC) -c mcp3302.cpp -o $@
	
clean:
	rm -rf *~ *.o core $(SENSORS_TEST) $(SERVER_TEST)

rebuildSensorTest: clean buildSensorTest

rebuildServerTest: clean buildServerTest
