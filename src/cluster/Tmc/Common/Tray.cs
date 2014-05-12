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

        public List<T> Cells { get; set; }

        public Tray()
        {
            Cells = new List<T>(MaxCells);
            for (int i = 0; i < MaxCells; i++)
            {
                Cells.Add(null);
            }
        }
    }
}
