using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;

namespace Tmc.Scada.Core
{
    /// <summary>
    /// Currently implemented as a placeholder (hence why it looks alot like TabletMagazine)
    /// </summary>
    public class OrderConfiguration
    {
        public Dictionary<TabletColors, int> Tablets { get; private set; }

        public OrderConfiguration()
        {
            this.Tablets = new Dictionary<TabletColors, int>();

            foreach (var value in (TabletColors[])Enum.GetValues(typeof(TabletColors)))
            {
                Tablets.Add(value, 0);
            }
        }

        public void AddTablet(TabletColors color, int count = 1)
        {
            Tablets[color] += count;
        }

        public void RemoveTablet(TabletColors color, int count = 1)
        {
            if ((Tablets[color] - count) > -1)
            {
                Tablets[color] -= count;
            }
            else
            {
                throw new InvalidOperationException("Tablet count cannot be less than zero");
            }
        }
    }
}