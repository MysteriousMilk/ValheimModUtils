namespace MilkWyzardStudios.Valheim
{
    /// <summary>
    /// Provides a standard interface for overriding Valheim scripts using
    /// the ScriptHookManager.
    /// </summary>
    public interface IOverridableScript
    {
        /// <summary>
        /// Called to hook functions from the script that this class is overriding.
        /// </summary>
        void Hook();
    }
}
