using System.Collections.Generic;

namespace MilkWyzardStudios.Valheim.Managers
{
    /// <summary>
    /// Manager class for keeping track of hook scripts via a standard interface.
    /// </summary>
    public class ScriptHookManager
    {
        #region Singleton Instance
        private static ScriptHookManager _instance;

        /// <summary>
        /// <see cref="ScriptHookManager"/> as a singleton instance.
        /// </summary>
        public static ScriptHookManager Instance
        {
            get
            {
                if (_instance == null) _instance = new ScriptHookManager();
                return _instance;
            }
        }
        #endregion

        private List<IOverridableScript> scriptList;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ScriptHookManager()
        {
            scriptList = new List<IOverridableScript>();
        }

        /// <summary>
        /// Adds a script to <see cref="ScriptHookManager"/> and initiates the "Hook".
        /// </summary>
        /// <param name="script">The script override object.</param>
        public void AddScript(IOverridableScript script)
        {
            scriptList.Add(script);
            script.Hook();
        }
    }
}
