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
    }
}
