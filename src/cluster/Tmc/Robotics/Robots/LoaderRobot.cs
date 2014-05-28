using System;
using System.Collections.Generic;

namespace Tmc.Robotics
{
    public class LoaderRobot : BaseRobot
    {
        public void GetTray(int trayNumber)
        {
            if(trayNumber < 1 || trayNumber > 6)
            {
                throw new ArgumentException("Tray Number must be between 1 and 6 inclusive");
            }

            var fileName = string.Format(FileNames.Loader.GrabTray, trayNumber);
            this.RunRapidProgram(fileName);
        }

        public void Palletise()
        {
            this.RunRapidProgram(FileNames.Loader.Palletise);
        }
    }
}
