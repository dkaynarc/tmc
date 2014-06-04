using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Common
{
    public class Tray<T> where T : class
    {
        public const int MaxCells = 9;
        // The middle cell is used to pick up the tray and therefore cannot be used
        // to hold tablets
        public const int MaxUsableCells = MaxCells - 1;
        public const int MiddleCellIndex = 4;

        public List<T> Cells { get; set; }
        public double Angle { get; set; }

        public Tray()
        {
            Cells = new List<T>(MaxCells);
            for (int i = 0; i < MaxCells; i++)
            {
                Cells.Add(null);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as Tray<T>;
            if (other == null)
            {
                return false;
            }

            return this.Cells.SequenceEqual(other.Cells);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Cells.GetHashCode();
            return hash;
        }
    }
}
