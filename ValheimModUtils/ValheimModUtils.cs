// ValheimModUtils
//
// A Valheim mod that provides general helper code and utilities for any mods developed by
// MilkWyzardStudios.
// 
// File:    ValheimModUtils.cs
// Project: ValheimModUtils

using BepInEx;
using Jotunn.Utils;

namespace ValheimModUtils
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class ValheimModUtils : BaseUnityPlugin
    {
        public const string PluginGUID = "com.milkwyzardstudios.ValheimModUtils";
        public const string PluginName = "MilkWyzardStudios.ValheimModUtils";
        public const string PluginVersion = "1.0.0";

        private void Awake()
        {
        }
    }
}

