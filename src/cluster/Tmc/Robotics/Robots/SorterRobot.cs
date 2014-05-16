using System;
using System.Collections.Generic;
using System.IO;
namespace Tmc.Robotics
{
    public class SorterRobot : BaseRobot
    {
        private string[] _magazinePoints;
        private string[] _magazineHeights;

        internal SorterRobot() : base()
        {
            var directory = Directory.GetCurrentDirectory() + "\\mod\\";
            var magazinePoints = directory + FileNames.Sorter.MagazinePoints;
            var magazineHeight = directory + FileNames.Sorter.MagazineHeights;

            if (!File.Exists(magazinePoints))
            {
                throw new Exception(string.Format("Could not find {0}", magazinePoints));
            }

            if (!File.Exists(magazineHeight))
            {
                throw new Exception(string.Format("Could not find {0}", magazineHeight));
            }

            _magazinePoints = File.ReadAllLines(magazinePoints);
            _magazineHeights = File.ReadAllLines(magazineHeight);
        }

        public void GetMagazine()
        {
            this.RunRapidProgram(FileNames.Sorter.GetMagazine);
        }

        public void ReturnMagazine()
        {
            this.RunRapidProgram(FileNames.Sorter.ReturnMagazine);
        }

        /// <summary>
        /// Moves tablet from X,Y Coordinates in the stack into the tablet magazine.
        /// </summary>
        /// <param name="XCoord">XCoord of the tablet.</param>
        /// <param name="YCoord">YCoord of the tablet.</param>
        /// <param name="magazineIndex">Index of slot within the tablet magazine (As labeled)</param>
        public void GetTablet(int XCoord, int YCoord, int magazineIndex)
        {
            if (magazineIndex < 0 || magazineIndex > 7)
            {
                throw new ArgumentException("Magazine Index must be between 0 and 7 inclusive");
            }
            //TODO: Defensively test coordinate boundaries

            var hoverPoint = _magazinePoints[magazineIndex].Replace("%ZCoord%", _magazineHeights[0]);
            var dropPoint = _magazinePoints[magazineIndex].Replace("%ZCoord%", _magazineHeights[1]);

            var dict = new Dictionary<string, string>();
            dict.Add("%XCoord%", XCoord.ToString());
            dict.Add("%YCoord%", YCoord.ToString());
            dict.Add("%MagazineHover%", hoverPoint);
            dict.Add("%MagazineDrop%", dropPoint);

            this.RunRapidProgram(FileNames.Sorter.SortChip, dict);
        }

        public void Shake()
        {
            this.RunRapidProgram(FileNames.Sorter.Shake);
        }
    }
}
