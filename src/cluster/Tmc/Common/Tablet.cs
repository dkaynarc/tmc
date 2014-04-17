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
        public Point LocationPoint { get; set; }
    }
}
