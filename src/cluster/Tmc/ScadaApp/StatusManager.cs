using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Scada.Core;

namespace Tmc.Scada.App
{
    class StatusManager
    {
        private ScadaEngine _engine;
        private MainForm _mainForm;

        public StatusManager(ScadaEngine _engine, MainForm _mainForm)
        {
            this._engine = _engine;
            this._mainForm = _mainForm;
        }

        public void update()
        {
            //update hardware controls on mainform
            _engine.GetStatus();
            _mainForm.Update();

        }
    }
}
