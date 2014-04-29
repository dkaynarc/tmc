using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmcData
{
    class How_To_Access_The_Database
    {
        /*
         *  Tables in the database cannot be accessed directly
         *  All communication to the database is done through Views and Store Procedures
         *  All methods can be found in the TmcRepository class
         *  App.Config file containing the connection string MUST be inside your project.
         */

        public void Examples()
        {
            IList<OrderList> OrderList = TmcRepository.OrderInfo();

            TmcRepository.AddNewOrder(1, 2, 2, 2, 2, 2);
        }
    }
}
