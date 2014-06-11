CC = g++
#CFLAGS = -Wall
LDFLAGS	= -lwiringPi
INCL	= -I
BIN_DIR = .

help:
	@echo "## Command for building sensor unit testing program ##"
	@echo "make sensor_unit_testing"
	@echo "## Command for building TCP/IP sensor server unit testing program ##"
	@echo "make server_unit_testing"
	@echo "## Command for cleaning up the compilation files"
	@echo "make clean"
	@echo "## Command for building both sensor and TCP/IP sensor server program ##"
	@echo "make tmc_sensor"

ServerTest:  mcp3302.o TMCSensor.o ServerTest.o 
	$(CC) $? $(LDFLAGS) -lboost_system -lboost_thread -o  $@

SensorTest:  mcp3302.o SensorTest.o TMCSensor.o
	$(CC) $? $(LDFLAGS) -o  $@

TMCSensor.o: TMCSensor.cpp TMCSensor.h
	$(CC) -c TMCSensor.cpp $(LDFLAGS) $(INCL)/usr/local/wiringPi -o $@	

SensorTest.o: SensorTest.cpp TMCSensor.h
	$(CC) -c SensorTest.cpp $(LDFLAGS) $(INCL)/usr/local/wiringPi -o $@  
	
ServerTest.o: TMCSnesorServer.cpp TMCSensor.h
	$(CC) -c TMCSnesorServer.cpp $(LDFLAGS) -lboost_system -lboost_thread $(INCL)/usr/local/wiringPi $(INCL)/home/pi/boost_1_55_0 -o $@
	
mcp3302.o: mcp3302.cpp mcp3302.h
	$(CC) -c mcp3302.cpp -o $@
	
clean:
	rm -rf *~ *.o core SensorTest ServerTest

tmc_sensor: clean SensorTest ServerTest
	
sensor_unit_testing: clean SensorTest

server_unit_testing: clean ServerTest
