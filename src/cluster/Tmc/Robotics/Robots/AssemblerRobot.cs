using System;
using System.Collections.Generic;

namespace Tmc.Robotics
{
    public class AssemblerRobot : BaseRobot
    {
        public void GetMagazine()
        {
            this.RunRapidProgram("this is a stub");
        }

        public void ReturnMagazine()
        {
            this.RunRapidProgram("this is a stub");
        }

        public void PlaceTablet(int magazineIndex, int trayIndex)
        {
            this.RunRapidProgram("this is a stub");
        }
    }
}
