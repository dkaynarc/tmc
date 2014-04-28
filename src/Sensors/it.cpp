#include <iostream>
#include <string>
#include <boost/asio.hpp>

using namespace std;

int main(int argc, char* argv[])
{
    try
    {
		if (argc != 2)
		{
			std::cerr << "Usage: TCP_echo_server <port>\n";
			return -1;
		}
        boost::asio::io_service io_service;
        boost::asio::ip::tcp::endpoint endpoint(boost::asio::ip::tcp::v4(), std::atoi(argv[1]));
        boost::asio::ip::tcp::acceptor acceptor(io_service, endpoint);
        boost::asio::ip::tcp::socket socket(io_service);
 
        std::cout << "Server is ready" << std::endl;
		acceptor.accept(socket);
        for(;;) // Change to infinite loop later on
        {
            //acceptor.accept(socket);
			
			char data_in[100];
			boost::system::error_code error;
			size_t length = socket.read_some(boost::asio::buffer(data_in), error);
			if (error == boost::asio::error::eof)
				break; // Connection closed by user
			else if (error)
				throw boost::system::system_error(error); // Give some error message
			std::string sdata_in(data_in);
			std::cout << sdata_in << std:: endl;	//boost::asio::write(socket, boost::asio::buffer(data_in, length));
            std::string message("Yep I got your message\n");
            boost::asio::write(socket, boost::asio::buffer(message));
            //socket.close();
        }
    }
    catch(std::exception& e)
    {
        std::cerr << "Exception: " << e.what() << std::endl;
    }
	return 0;
}
