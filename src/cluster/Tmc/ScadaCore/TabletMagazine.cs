using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;

namespace Tmc.Scada.Core
{
    public class Slot
    {
        public TabletColors Color { get; set; }
        public int TabletCount { get; set; }

        public Slot(TabletColors color = TabletColors.Unknown, int count = 0)
        {
            this.Color = color;
            this.TabletCount = count;
        }
    }
    public class TabletMagazine
    {
        public int SlotCapacity { get; set; }
        //public Dictionary<TabletColors, int> Slots { get; private set; }
        public ObservableCollection<Slot> Slots { get; set; }
        private Dictionary<TabletColors, int> _slotIndexMap;
        public TabletMagazine()
        {
            this.Slots = new ObservableCollection<Slot>();
            this._slotIndexMap = new Dictionary<TabletColors,int>();
            this.SlotCapacity = 5;

            int slotIndex = 0;

            foreach (var value in (TabletColors[])Enum.GetValues(typeof(TabletColors)))
            {
                Slots.Add(new Slot());
                _slotIndexMap.Add(value, slotIndex++);
            }
        }

        public void AddTablet(TabletColors slotColor, int count = 1)
        {
            GetSlotByColor(slotColor).TabletCount += count;
        }

        public void RemoveTablet(TabletColors slotColor, int count = 1)
        {
            if ((GetSlotByColor(slotColor).TabletCount - count) > -1)
            {
                GetSlotByColor(slotColor).TabletCount -= count;
            }
            else
            {
                throw new InvalidOperationException("Tablet count cannot be less than zero");
            }
        }
        
        public int GetSlotIndex(TabletColors slotColor)
        {
            return _slotIndexMap[slotColor];
        }

        public int GetSlotDepth(TabletColors slotColor)
        {
            return GetSlotByColor(slotColor).TabletCount;
        }
        public bool IsFull()
        {
            bool isFull = true;
            Slots.ToList().ForEach(x => isFull &= (x == SlotCapacity));
            return isFull;
        }

        public bool IsSlotFull(TabletColors slotColor)
        {
            return (Slots[slotColor] == SlotCapacity);
        }

        //TODO Check whether this method is required anywhere
        public bool IsSlotEmpty(TabletColors slotColor)
        {
            return (Slots[slotColor] == 0);
        }

        public List<TabletColors> GetFullSlots()
        {
            return Slots.Where(x => x.Value == SlotCapacity).Select(y => y.Key).ToList();
        }

        private Slot GetSlotByColor(TabletColors slotColor)
        {
            return Slots[_slotIndexMap[slotColor]];
        }
    }
}
