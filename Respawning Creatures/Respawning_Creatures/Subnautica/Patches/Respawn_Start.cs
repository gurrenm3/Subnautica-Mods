using HarmonyLib;
using System.Collections;
using UnityEngine;
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
            if (DayNightCycle.main.timePassed >= (double)__instance.spawnTime) // if this is true respawn would have already happened in method
                return;

            CoroutineHost.StartCoroutine(RespawnCoroutine(__instance));
        }

        private static IEnumerator RespawnCoroutine(Respawn respawn)
        {
            float timePassed = (float)DayNightCycle.main.timePassed;
            float waitTime = (float)System.Math.Round(respawn.spawnTime - timePassed);

            yield return new WaitForSecondsRealtime(waitTime);

            respawn.Spawn();
            UnityEngine.Object.Destroy(respawn.gameObject);
        }
    }
}