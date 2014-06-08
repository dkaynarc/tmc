using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tmc.Common;

namespace Tmc.Scada.Core
{
    public class Slot : INotifyPropertyChanged
    {
        private TabletColors _color;
        private int _tabletCount;

        public TabletColors Color
        {
            get
            {
                return this._color;
            }
            set
            {
                if (value != this._color)
                {
                    this._color = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int TabletCount
        {
            get
            {
                return this._tabletCount;
            }
            set
            {
                if (value != this._tabletCount)
                {
                    this._tabletCount = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Slot(TabletColors color = TabletColors.Unknown, int count = 0)
        {
            this.Color = color;
            this.TabletCount = count;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class TabletMagazine
    {
        public int SlotCapacity { get; set; }
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
                Slots.Add(new Slot(value));
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
            foreach (var s in Slots)
            {
                isFull &= (s.TabletCount == SlotCapacity);
            }
            return isFull;
        }

        public bool IsSlotFull(TabletColors slotColor)
        {
            return (GetSlotByColor(slotColor).TabletCount == SlotCapacity);
        }

        //TODO Check whether this method is required anywhere
        public bool IsSlotEmpty(TabletColors slotColor)
        {
            return (GetSlotByColor(slotColor).TabletCount == 0);
        }

        public List<TabletColors> GetFullSlots()
        {
            return Slots.Where(x => x.TabletCount == SlotCapacity).Select(y => y.Color).ToList();
        }

        private Slot GetSlotByColor(TabletColors slotColor)
        {
            return Slots[_slotIndexMap[slotColor]];
        }
    }
}
