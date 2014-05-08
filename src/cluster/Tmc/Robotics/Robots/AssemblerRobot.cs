using System;
using System.Collections.Generic;
using System.IO;
using Tmc.Common;

namespace Tmc.Robotics
{
    public class AssemblerRobot : BaseRobot
    {
        private string[] _magazinePoints;
        private string[] _magazineHeights;
        private string[] _dropPositions;

        protected AssemblerRobot() : base()
        {
            var directory = Directory.GetCurrentDirectory() + "\\mod\\";
            var magazinePoints = directory + "AssemblerMagazinePoints.mod";
            var magazineHeight = directory + "MagazineHeights.mod";
            var dropPositions = directory + "TrayDropPositions.mod";

            if (!File.Exists(magazinePoints))
            {
                throw new Exception(string.Format("Could not find {0}", magazinePoints));
            }

            if (!File.Exists(magazineHeight))
            {
                throw new Exception(string.Format("Could not find {0}", magazineHeight));
            }

            if (!File.Exists(dropPositions))
            {
                throw new Exception(string.Format("Could not find {0}", dropPositions));
            }

            _magazinePoints = File.ReadAllLines(magazinePoints);
            _magazineHeights = File.ReadAllLines(magazineHeight);
            _dropPositions = File.ReadAllLines(dropPositions);
        }

        public void GetMagazine()
        {
            this.RunRapidProgram("AssemblerGetMagazine.mod");
        }

        public void ReturnMagazine()
        {
            this.RunRapidProgram("AssemblerReturnMagazine.mod");
        }

        /// <summary>
        /// Moves a tablet from the tablet magazine to the tray.
        /// </summary>
        /// <param name="magazineIndex">Index of slot within the tablet magazine (As labeled)</param>
        /// <param name="chipDepth">Current amount of chips in the specified magazine slot. (1 to 10)</param>
        /// <param name="trayIndex">Index of the tray in which to place the tablet.</param>
        public void PlaceTablet(int magazineIndex, int chipDepth, int trayIndex)
        {
            if(magazineIndex < 0 || magazineIndex > 7)
            {
                throw new ArgumentException("Magazine Index must be between 0 and 7 inclusive");
            }

            if(chipDepth < 1 || chipDepth > 10)
            {
                throw new ArgumentException("Chip depth must be between 1 and 10 inclusive");
            }

            if(trayIndex < 0 || trayIndex > 8)
            {
                throw new ArgumentException("Tray index must be between 0 and 8 inclusive");
            }

            var magazineHover = _magazinePoints[magazineIndex].Replace("%ZCoord%", _magazineHeights[0]);
            var tabletTouch = _magazinePoints[magazineIndex].Replace("%ZCoord%", _magazineHeights[chipDepth]);
            var tabletDrop = _dropPositions[trayIndex];

            var dict = new Dictionary<string, string>();
            dict.Add("%MagazineHover%", magazineHover);
            dict.Add("%Tablet%", tabletTouch);
            dict.Add("%TabletDrop%", tabletDrop);

            this.RunRapidProgram("PlaceTablet.mod", dict);
        }
    }
}
