
namespace Tmc.Scada.Core.Sequencing
{
    public enum Trigger
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
        Valid,
        Invalid
    }
}
