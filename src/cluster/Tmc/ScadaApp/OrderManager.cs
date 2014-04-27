using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.App
{
    class OrderManager
    {
        private ScadaWCFServer _wcfServer;
        private MainForm _mainForm;

        public OrderManager(ScadaWCFServer _wcfServer, MainForm _mainForm)
        {
            this._wcfServer = _wcfServer;
            this._mainForm = _mainForm;
        }

        public void update()
        {
            //update hardware controls on mainform
            _wcfServer.getOrders();
            _mainForm.Update();
        }

        public void createOrder()
        {
            //launch new order form
            OrderForm order = new OrderForm();
        }

        public void deleteOrder()
        {
            //delete order record
            _wcfServer.deleteOrder();
        }
    }
}
