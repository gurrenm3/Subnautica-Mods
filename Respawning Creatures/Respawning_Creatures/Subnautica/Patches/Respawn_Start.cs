using HarmonyLib;
using UWE;

namespace Respawning_Creatures.Patches
{
    [HarmonyPatch(typeof(Respawn), nameof(Respawn.Start))]
    internal class Respawn_Start
	{
        [HarmonyPrefix]
        internal static bool Prefix(Respawn __instance)
        {
			return true;
        }

        [HarmonyPostfix]
        internal static void PostFix(Respawn __instance)
        {
			if (__instance?.gameObject == null) // this should be null if respawn already took place
				return;

			CoroutineHost.StartCoroutine(Respawner.RespawnCoroutine(__instance));
        }
    }
}