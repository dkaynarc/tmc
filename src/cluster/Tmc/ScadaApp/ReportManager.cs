using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Scada.App
{
    class ReportManager
    {
        private ScadaWCFServer _wcfServer;
        private MainForm _mainForm;

        public ReportManager(ScadaWCFServer _wcfServer, MainForm _mainForm)
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

        public void createReport()
        {
            //launch new order form
            ReportForm report = new ReportForm();
        }

        public void deleteReport()
        {
            //delete order record
            _wcfServer.deleteReport();
        }
    }
}
