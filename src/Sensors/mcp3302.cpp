/**
 *  \file mcp3302.cpp
 *  \brief This class contain the basic functionality to work with MCP3302 over SPI channel on Raspberry Pi
 */

#include "mcp3302.h"

using namespace std;

/**
 *  \brief Initiate the the MCP3302 on SPI.
 *  
 *  \param [in] devspi : A string of the directory to SPI device
 *  \return File descriptor that indicate the SPI device.
 *  
 *  \details	This function does the initial configuration of the MCP3302
 *  			by setting up the SPI mode, W/R mode and speed and size.
 */
int mcp3302::spiOpen(std::string devspi)
{
    int statusVal = -1;
    this->spifd = open(devspi.c_str(), O_RDWR);
    if(this->spifd < 0)
	{
        perror("could not open SPI device");
        exit(1);
    }
 
    statusVal = ioctl (this->spifd, SPI_IOC_WR_MODE, &(this->mode));
    if(statusVal < 0)
	{
        perror("Could not set SPIMode (WR)...ioctl fail");
        exit(1);
    }
 
    statusVal = ioctl (this->spifd, SPI_IOC_RD_MODE, &(this->mode));
    if(statusVal < 0) 
	{
      perror("Could not set SPIMode (RD)...ioctl fail");
      exit(1);
    }
 
    statusVal = ioctl (this->spifd, SPI_IOC_WR_BITS_PER_WORD, &(this->bitsPerWord));
    if(statusVal < 0) 
	{
      perror("Could not set SPI bitsPerWord (WR)...ioctl fail");
      exit(1);
    }
 
    statusVal = ioctl (this->spifd, SPI_IOC_RD_BITS_PER_WORD, &(this->bitsPerWord));
    if(statusVal < 0) 
	{
      perror("Could not set SPI bitsPerWord(RD)...ioctl fail");
      exit(1);
    }  
 
    statusVal = ioctl (this->spifd, SPI_IOC_WR_MAX_SPEED_HZ, &(this->speed));    
    if(statusVal < 0) 
	{
      perror("Could not set SPI speed (WR)...ioctl fail");
      exit(1);
    }
 
    statusVal = ioctl (this->spifd, SPI_IOC_RD_MAX_SPEED_HZ, &(this->speed));    
    if(statusVal < 0) 
	{
      perror("Could not set SPI speed (RD)...ioctl fail");
      exit(1);
    }
    return statusVal;
}
 
/**
 *  \brief Close the MCP3302 from SPI.
 *  
 *  \return 0 if the SPI is closed successfully.
 *  
 *  \details This function will close the MCP3302 device from SPI channel.
 */ 

int mcp3302::spiClose()
{
    int statusVal = -1;
    statusVal = close(this->spifd);
	if(statusVal < 0) 
	{
      perror("Could not close SPI device");
      exit(1);
    }
    return statusVal;
}

/**
 *  \brief Send command to MCP3302 over SPI channel
 *  
 *  \param [in] data data to be transfer to MCP3302 for ADC conversion.
 *  \param [in] length number of byte in transmitted data size.
 *  \return 0 if MCP3302 successfully received the command, otherwise 1.
 *  
 *  \details	This function will take the command and sample data and transmit it to MCP3302 over SPI channel to 
 *  			perform ADC conversion.
 */

int mcp3302::spiWriteRead(unsigned char *data, int length)
{
	struct spi_ioc_transfer spi[length];
	int i = 0;
	int retVal = -1; 

	// One SPI transfer for each byte

	for (i = 0 ; i < length ; i++)
	{
		spi[i].tx_buf        = (unsigned long)(data + i); 	// transmit from "data"
		spi[i].rx_buf        = (unsigned long)(data + i) ; 	// receive into "data"
		spi[i].len           = sizeof(*(data + i)) ;
		spi[i].delay_usecs   = 0 ;
		spi[i].speed_hz      = this->speed ;
		spi[i].bits_per_word = this->bitsPerWord ;
		spi[i].cs_change = 0;
	}

	retVal = ioctl (this->spifd, SPI_IOC_MESSAGE(length), &spi) ;

	if(retVal < 0)
	{
		perror("Problem transmitting spi data..ioctl");
		exit(1);
	}
	return retVal;
}

mcp3302::mcp3302()
{
	this->mode = SPI_MODE_0 ;
	this->bitsPerWord = 8;
	this->speed = 1250000;
	this->spifd = -1;

	this->spiOpen(std::string("/dev/spidev0.0"));
}
 
mcp3302::mcp3302(std::string devspi, unsigned char spiMode, unsigned int spiSpeed, unsigned char spibitsPerWord)
{
	this->mode = spiMode ;
	this->bitsPerWord = spibitsPerWord;
	this->speed = spiSpeed;
	this->spifd = -1;
	this->spiOpen(devspi);
}
/*
mcp3302::~mcp3302(){
    this->spiClose();
}*/

