#include <iostream>
#include <string>
#include <boost/asio.hpp>
#include <boost/thread.hpp>
#include <boost/make_shared.hpp>
#include <algorithm>
#include "TMCSensor.h"

//using namespace std;

AmbienceSensor ambience;
TemperatureSensor temperature;
HumiditySensor humidity;
SoundSensor	sound;
DustSensor dust;
mcp3302	a2d;
int i2c_fd = wiringPiI2CSetup(I2C_TEMP_ADDRESS);
std::vector<boost::shared_ptr<boost::asio::ip::tcp::socket> > established_connections;

/**
 *  \brief Removes socket from <vector> established_connections before close()
 *  
 *  \param [in] socket file descriptor of the socket connection.
 *  \return No return value.
 *  
 *  \details This function will remove the socket from established_connections vector.
 */

void removeSocket(boost::shared_ptr<boost::asio::ip::tcp::socket> socket)
{
	established_connections.erase(std::remove(established_connections.begin(), established_connections.end(), socket),established_connections.end());
	(*socket).close();
}

/**
 *  \brief Worker method to handle each independent socket from listener_loop. 
 *  
 *  \param [in] socket socket file descriptor of the socket connection.
 *  \return No return value.
 *  
 *  \details This method handles all communication data between the hardware sensors within the Raspberry Pi
 */

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
				
				//DustSensor dust;
				std::string data = dust.getData() + "\n";
				message = data;
				
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

/**
 *  \brief Worker thread to handle the dust sensor measurement.
 *  
 *  \return No return value.
 *  
 *  \details This function will handle the measurement of dust concentration.
 */

void updateMeasurement()
{
	//HumiditySensor humidity;
	dust.obtainData();
	
}

/**
 *  \brief Creates a TCP listener based on the input port number value from main(). 
 *  
 *  \param [in] port_number Port number
 *  \return Return_Description
 *  
 *  \details Manages multiple socket connections via a vector<shared_ptr<sockets> > 
 */

void listener_loop(int port_number)
{
	boost::thread workerThread3(updateMeasurement);
	

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


/**
 *  \brief The main() function
 *  
 *  \param [in] argc Port number
 *  \return Return_Description
 *  
 *  \details Runs the TCP listener loop to handle connections
 */

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
