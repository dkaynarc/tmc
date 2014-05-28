using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Robotics
{
    internal static class FileNames
    {
        internal static class Base
        {
            internal static string TempFile     = "_temp.mod";
            internal static string HomePosition = "BaseRobotHomePosition.mod";

        }

        internal static class Sorter
        {
            internal static string Shake            = "SorterShake.mod";
            internal static string SortChip         = "SorterSortChip.mod";
            internal static string GetMagazine      = "SorterGetMagazine.mod";
            internal static string MagazinePoints   = "SorterMagazinePoints.mod";
            internal static string ReturnMagazine   = "SorterReturnMagazine.mod";
            internal static string MagazineHeights  = "SorterMagazineHeights.mod";
        }

        internal static class Assembler
        {
            internal static string GetMagazine     = "AssemblerGetMagazine.mod";
            internal static string PlaceTablet     = "AssemblerPlaceTablet.mod";
            internal static string TrayPositions   = "AssemblerTrayPositions.mod";
            internal static string MagazinePoints  = "AssemblerMagazinePoints.mod";
            internal static string ReturnMagazine  = "AssemblerReturnMagazine.mod";
            internal static string MagazineHeights = "AssemblerMagazineHeights.mod";
        }

        internal static class Loader
        {
            internal static string GrabTray  = "LoaderGrabTray{0}.mod";
            internal static string Palletise = "";
        }

        internal static class Palletiser
        {
            internal static string PalletiseSurplus   = "";
            internal static string PalletiseAccepted  = "";
            internal static string PalletiseRejected  = "";
            internal static string PalletiseThrowAway = "";
        }
    }
}
