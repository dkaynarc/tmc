
namespace Tmc.Scada.Core.Sequencing
{
    public enum StateEvent
    {
        // Global commands
        Shutdown,
        Stop,
        Completed,
        Failed,
        Start,
        Resume,

        // State specific commands
        // Verification
        TrayVerificationCompleted,
        ProductVerificationCompleted,
    }
}
