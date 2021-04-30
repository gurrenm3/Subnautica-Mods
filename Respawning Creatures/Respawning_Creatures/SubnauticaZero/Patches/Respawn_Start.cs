using HarmonyLib;
using System;
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
			if (__instance?.gameObject == null)
				return;

			
        }
    }
}