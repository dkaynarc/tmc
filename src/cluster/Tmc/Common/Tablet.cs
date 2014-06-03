using System;
using System.Drawing;

namespace Tmc.Common
{
    public enum TabletColors
    {
        Black,
        White,
        Red,
        Green,
        Blue,
        Unknown
    }
    public class Tablet
    {
        public TabletColors Color { get; set; }
        public PointF LocationPoint { get; set; }

        public Tablet()
        {
            Color = TabletColors.Unknown;
            LocationPoint = new PointF(0, 0);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var other = obj as Tablet;
            if (other == null)
            {
                return false;
            }

            return (this.Color == other.Color);
        }
    }
}
