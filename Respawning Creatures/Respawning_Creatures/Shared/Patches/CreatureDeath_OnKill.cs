using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace Respawning_Creatures.Patches
{
    [HarmonyPatch(typeof(CreatureDeath), nameof(CreatureDeath.OnKill))]
    internal class CreatureDeath_OnKill
    {
        [HarmonyPrefix]
        internal static bool Prefix(CreatureDeath __instance)
        {
            if (!__instance.respawn || __instance.respawnOnlyIfKilledByCreature) // trigger respawner if it normally wouldn't trigger
                __instance.SpawnRespawner();

            return true;
        }
    }
}