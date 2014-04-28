using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;

namespace Tmc.Scada.Core
{
    public class TabletMagazine
    {
        public int SlotCapacity { get; set; }
        public Dictionary<TabletColors, int> Slots { get; private set; }
        private Dictionary<TabletColors, int> _slotIndexMap;
        public TabletMagazine()
        {
            this.Slots = new Dictionary<TabletColors, int>();
            this._slotIndexMap = new Dictionary<TabletColors,int>();
            this.SlotCapacity = 5;

            int slotIndex = 0;

            foreach (var value in (TabletColors[])Enum.GetValues(typeof(TabletColors)))
            {
                Slots.Add(value, 0);
                Slots.Add(value, slotIndex++);
            }
        }

        public void AddTablet(TabletColors slot, int count = 1)
        {
            Slots[slot] += count;
        }
        
        public int GetSlotIndex(TabletColors slotColor)
        {
            return _slotIndexMap[slotColor];
        }

        public bool IsFull()
        {
            bool isFull = true;
            Slots.Values.ToList().ForEach(x => isFull = (x == SlotCapacity));
            return isFull;
        }

        public List<TabletColors> GetFullSlots()
        {
            return Slots.Where(x => x.Value == SlotCapacity).Select(y => y.Key).ToList();
        }
    }
}
