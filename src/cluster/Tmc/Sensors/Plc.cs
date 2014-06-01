using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASADTCPLib;
using Tmc.Common;
using AxASADTCPLib;

namespace Tmc.Sensors
{
    public sealed class Plc : IPlc, IDisposable
    {
        private HardwareStatus _hwStatus;
        private string _nodeIpAddress;
        private AxAsadtcp _plc;
        private Dictionary<PlcAttachedSwitch, bool> _switchStates;
        private const string DemoWindowName = "Automated Solutions Demo";

        public Plc()
        {
            _switchStates = new Dictionary<PlcAttachedSwitch,bool>();
            foreach (var item in (PlcAttachedSwitch[])Enum.GetValues(typeof(PlcAttachedSwitch)))
            {
                _switchStates.Add(item, false);
            }
        }

        ~Plc()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && _plc != null)
            {
                _plc.Dispose();
            }
        }

        public string Name { get; set; }

        public HardwareStatus GetStatus()
        {
            return this._hwStatus;
        }

        public void Initialise()
        {
            try
            {
                this._plc = CreatePlcControl();
            }
            catch (Exception ex)
            {
                this._hwStatus = HardwareStatus.Failed;
                throw new Exception("PLC initialisation failed", ex);
            }
            this._hwStatus = HardwareStatus.Operational;
        }

        public void Shutdown()
        {
            this._plc.Disconnect();
            this._hwStatus = HardwareStatus.Offline;
        }

        public void SetParameters(Dictionary<string, string> parameters)
        {
            string param;
            if (!parameters.TryGetValue("IPAddress", out param))
            {
                throw new Exception("PLC requires an IPAddress parameter");
            }

            this._nodeIpAddress = param;

            if (parameters.TryGetValue("Name", out param))
            {
                this.Name = param;
            }
        }

        public IDictionary<PlcAttachedSwitch, bool> GetSwitchStates()
        {
            UpdateAllSwitchStates();
            return this._switchStates;
        }

        private void UpdateAllSwitchStates()
        {
            this._plc.Function = enumAsadtcpFunction.ASADTCP_FUNC_READ;
            this._plc.MemStart = "Y0";
            this._plc.MemQty = 1;
            this._plc.SyncRefresh();

            if (this._plc.Result == 0)
            {
                var switchKeysArray = _switchStates.Keys.ToArray();
                for (int i = 0; i < switchKeysArray.Count(); i++)
                {
                    var switchKey = switchKeysArray[i];
                    _switchStates[switchKey] = this._plc.GetDataBitM((short)switchKey);
                }
            }
            else
            {
                this._hwStatus = HardwareStatus.Failed;
                throw new Exception("Unable to retrieve state from PLC");
            }
        }

        private AxAsadtcp CreatePlcControl()
        {
            var control = new AxAsadtcp();
            var windowDestroyer = new WindowDestroyer(DemoWindowName, 100, 5);

            ((System.ComponentModel.ISupportInitialize)(control)).BeginInit();
            
            // Close the demo notification window
            windowDestroyer.DestroyWindow();

            control.CreateControl();

            control.Visible = false;
            control.NodeAddress = _nodeIpAddress;
            ((System.ComponentModel.ISupportInitialize)(control)).EndInit();
            
            return control;
        }
    }
}
