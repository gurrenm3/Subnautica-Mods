using HarmonyLib;
using QModManager.API.ModLoading;
using Respawning_Creatures.Extensions;
using System;
using System.Reflection;

namespace Respawning_Creatures
{
    [QModCore]
    public static class QModPatcher
    {
        public static string modsDir = $"{Environment.CurrentDirectory}\\QMods\\{Assembly.GetExecutingAssembly().GetName().Name}";
        public const string versionNumber = "1.0.2";

        [QModPatch]
        public static void Patch()
        {
            var assembly = Assembly.GetExecutingAssembly();
            new Harmony($"GurrenM4_{assembly.GetName().Name}").PatchAll(assembly);
            Settings.instance = Settings.instance.Load();
            Settings.instance.Save(); //saving to save any new Properties that are made
        }
    }
}
