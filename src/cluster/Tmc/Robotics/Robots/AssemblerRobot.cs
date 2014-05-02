using System;
using System.Collections.Generic;
using Tmc.Common;

namespace Tmc.Robotics
{
    [Name("AssemblerRobot")]
    public class AssemblerRobot : BaseRobot
    {
        public void GetMagazine()
        {
            this.RunRapidProgram("AssemblerGetMagazine.mod");
        }

        public void ReturnMagazine()
        {
            this.RunRapidProgram("AssemblerReturnMagazine.mod");
        }

        public void PlaceTablet(int magazineIndex, int trayIndex)
        {
            this.RunRapidProgram("this is a stub");
        }
    }
}
