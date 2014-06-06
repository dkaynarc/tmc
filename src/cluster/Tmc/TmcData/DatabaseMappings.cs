using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmcData
{
    public enum EnvironmentType
    {
        Humidity = 1,
        Temperature = 2,
        Light =3,
        Sound =4,
        Dust =5
    }

    public enum Source
    {
        RobotA = 1,
        RobotB =2,
        RobotC = 3,
        RobotD = 4, 
        ConveyorMagazine = 5,
        ConveyorTray = 6,
        Temperature = 7,
        Humidity = 8,
        Light = 9,
        Sound = 10,
        Dust = 11,
        System = 12
    }
}
