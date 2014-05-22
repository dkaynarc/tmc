using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmcData
{
    public enum EnvironmentType
    {
        Temperature,
        Humidity,
        Light,
        Sound,
        Dust
    }

    public enum Source
    {
        RobotA = 1,
        RobotB =2,
        RobotC = 3,
        RobotD = 4, 
        ConveyorMagazine = 5,
        ConveyorTray = 6,
        SensorA = 7,
        SensorB = 8
    }
}
