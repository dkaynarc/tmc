using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASADTCPLib;

namespace PLC_prototype
{
    class Program 
    {
        static void Main(string[] args)
        {
            try
            {
                ASADTCPLib._DAsadtcp plc = new Asadtcp();

               // plc.AddressType = "192.168.1.5";
                    
                    
                    //plc.Function = enumAsadtcpFunction.ASADTCP_FUNC_READ;
                
                //plc.
                //plc.AutoPollEnabled = false;

                //plc.AddressType
                plc.NodeAddress = "192.168.1.5";

                
                
               // plc.MemStart = "Y0";
               // plc.Function = enumAsadtcpFunction.ASADTCP_FUNC_READ;

  //txtMemStartReadD.Text = AsadtcpReadD.MemStart
 // txtMemQtyReadD.Text = AsadtcpReadD.MemQty
 // txtNodeAddressReadD.Text = AsadtcpReadD.NodeAddress
 // txtAutoPollIntervalReadD.Text = AsadtcpReadD.AutoPollInterval
 // txtTimeoutTransReadD.Text = AsadtcpReadD.TimeoutTrans
 // NumberLabels lblReadDiscretes, AsadtcpReadD.MemStart
                //plc.AsyncRefresh();
                Console.WriteLine("connected!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }




        }
    }
}
