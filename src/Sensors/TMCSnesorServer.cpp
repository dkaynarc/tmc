#include <iostream>
#include <string>
#include <boost/asio.hpp>
#include <boost/thread.hpp>
#include <boost/make_shared.hpp>
#include <algorithm>
#include "TMCSensor.h"

//using namespace std;

std::string _dustData = "N/A";
AmbienceSensor ambience;
TemperatureSensor temperature;
HumiditySensor humidity;
SoundSensor	sound;
mcp3302	a2d;
int i2c_fd = wiringPiI2CSetup(I2C_TEMP_ADDRESS);
std::vector<boost::shared_ptr<boost::asio::ip::tcp::socket> > established_connections;

/// <summary>
/// Removes socket from <vector> established_connections before close()
/// </summary>


void removeSocket(boost::shared_ptr<boost::asio::ip::tcp::socket> socket)
{
	established_connections.erase(std::remove(established_connections.begin(), established_connections.end(), socket),established_connections.end());
	(*socket).close();
}

/// <summary>
/// Worker method to handle each independent socket from listener_loop. 
/// This method handles all communication data between the hardware sensors within the Raspberry Pi
///
/// TO-DO: implement and include all hardware sensors into this method + header file to include all the sensor classes
/// </summary>

void worker(boost::shared_ptr<boost::asio::ip::tcp::socket> socket)
{	try
	{
        for(;;) 
        {
            		
			char data_in[100];
			boost::system::error_code error;
			size_t length = (*socket).read_some(boost::asio::buffer(data_in), error);
			std::string connections;
			if (error == boost::asio::error::eof)
			{
				break; // Connection closed by user
			}
			else if (error)
			{
				throw boost::system::system_error(error); // Give some error message (DONALD: need to handle this error back to SCADA)
			}
				
			std::string sdata_in(data_in);
			
			std::string message = "unknown\n";
			
			if (sdata_in == "temperature")
			{
				// TemperatureSensor temperature;
				std::string data = temperature.getData(i2c_fd) + "\n";
				message = data;
			}
			
			if(sdata_in == "ambience")
			{
				// AmbienceSensor ambience;
				std::string data = ambience.getData(a2d) + "\n";
				message = data;
			}
			
			if(sdata_in == "sound")
			{
				// SoundSensor	sound;
				std::string data = sound.getData(a2d) + "\n";
				message = data;
			}
			
			if(sdata_in == "humidity")
			{
				// HumiditySensor humidity;
				std::string data = humidity.getData() + "\n";
				message = data;
				//message = _humidityData;
				//HumiditySensor humidity;
				//_humidityData = humidity.getData() + "\n";
				
			}
			
			if(sdata_in == "dust")
			{
				message = _dustData;
				//DustSensor dust;
				//_dustData = dust.getData() + "\n";
				
			}
	
            boost::asio::write(*socket, boost::asio::buffer(message));
            
        }
		
		removeSocket(socket);
	} 
	catch(std::exception& e)
	{
		//std::cerr << "Exception: " << e.what() << std::endl;
		removeSocket(socket);
	}
}


void updateMeasurement()
{
	//HumiditySensor humidity;
	DustSensor dust;
	SoundSensor	sound;
	HumiditySensor humidity;
	
	
	for (;;)
	{
		//std::cout << "I am measuring!!" << std::endl; 
		//std::string hData = humidity.getData() + "\n";
		//std::string dData = dust.getData() + "\n";
		//_dustData = dust.getData() + "\n";
		
		//std::string data = sound.getData() + "\n";
		std::cout << humidity.getData() << std::endl; 
		//std::cout <<  hData << std::endl;
		//delay(1000);
	}
}

/// <summary>
/// Creates a TCP listener based on the input port number value from main(). 
/// Manages multiple socket connections via a vector<shared_ptr<sockets> > 
///
/// TO-DO: clean up vector on closing workerThread2
/// </summary>

void listener_loop(int port_number)
{
	//boost::thread workerThread3(updateMeasurement);
	

	boost::asio::io_service io_service;
	boost::asio::ip::tcp::endpoint endpoint(boost::asio::ip::tcp::v4(), port_number);
	boost::asio::ip::tcp::acceptor acceptor(io_service, endpoint);

	
	while(true)
	{
		try
		{

			boost::shared_ptr<boost::asio::ip::tcp::socket> socket = boost::make_shared<boost::asio::ip::tcp::socket>(boost::ref(io_service));
			established_connections.push_back(socket);
			std::cout << "TCP server ready and listening ... " << std::endl;
			acceptor.accept(*established_connections.back());
			std::cout << "Number of established client connections: ";
			std::cout << established_connections.size() << std::endl;
			
			boost::thread workerThread2(worker,established_connections.back());
			
		}
		catch(std::exception& e)
		{
			std::cerr << "Exception: " << e.what() << std::endl;
		}
	}
}




/// <summary>
/// Runs the TCP listener loop to handle connections
/// Input argument: Port number
/// </summary>

int main(int argc, char* argv[])
{
	if (argc != 2)
	{
		std::cerr << "Usage: TCP_echo_server <port>\n";
		return -1;
	}
	wiringPiSetup();
	wiringPiI2CSetup(I2C_TEMP_ADDRESS);
	boost::thread workerThread(listener_loop, std::atoi(argv[1])); // Creates a thread that loops to accept sockets

	workerThread.join();
	
	return 0;
}
