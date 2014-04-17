
namespace Tmc.Scada.Core.Sequencing
{
    public enum State
    {
        // Global States
        Stopped,
        Startup,
        Shutdown,
        Idle,
        Running,
        // Assembly Process States
        LoadingTray,
        AssemblyConveyorMovingForward,
        VerifyingTray,
        Assembling,
        AssemblyConveyorMovingBackward,
        PlacingTrayInBuffer,
        Palletising,
        //Sorting Process States
        Sorting,
        PlacingTabletMagazineInSortingBuffer,
        PlacingTabletMagazineInAssemblyBuffer,
        PlacingTabletMagazineOnSortingConveyorFromSorter,
        PlacingTabletMagazineOnSortingConveyorFromAssembler,
        SortingConveyorMovingForward,
        SortingConveyorMovingBackward
    }
}
