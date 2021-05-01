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
			if (DayNightCycle.main.timePassed >= (double)__instance.spawnTime) // if this is true respawn would have already happened in method
				return;

			CoroutineHost.StartCoroutine(RespawnCoroutine(__instance));
		}

        private static IEnumerator RespawnCoroutine(Respawn __instance)
        {
			float timePassed = (float)DayNightCycle.main.timePassed;
			float waitTime = (float)System.Math.Round(__instance.spawnTime - timePassed);

			yield return new WaitForSecondsRealtime(waitTime);



			////
			// Game's original method
			////
			int num = UWE.Utils.OverlapSphereIntoSharedBuffer(__instance.transform.position, 1.5f, -1, QueryTriggerInteraction.UseGlobal);
			for (int i = 0; i < num; i++)
			{
				if (UWE.Utils.sharedColliderBuffer[i].GetComponentInParent<Base>() != null)
				{
					UnityEngine.Object.Destroy(__instance.gameObject);
					yield break;
				}
			}
			TaskResult<GameObject> result = new TaskResult<GameObject>();
			IEnumerator enumerator = CraftData.InstantiateFromPrefabAsync(__instance.techType, result, false);
			yield return enumerator;
			GameObject gameObject = result.Get();

			gameObject.transform.SetPositionAndRotation(__instance.transform.position, __instance.transform.rotation);

			for (int j = 0; j < __instance.addComponents.Count; j++)
			{
				Type type = Type.GetType(__instance.addComponents[j]);
				if (type != null)
				{
					gameObject.AddComponent(type);
				}
			}
			gameObject.SetActive(true);
			if (__instance.transform.parent == null || __instance.transform.parent.GetComponentInParent<LargeWorldEntity>() == null)
			{
				if (LargeWorldStreamer.main)
				{
					LargeWorldStreamer.main.cellManager.RegisterEntity(gameObject);
				}
			}
			else
			{
				if (LargeWorldStreamer.main)
				{
					LargeWorldStreamer.main.cellManager.UnregisterEntity(gameObject);
				}
				gameObject.transform.parent = __instance.transform.parent;
			}

			UnityEngine.Object.Destroy(__instance.gameObject);
			result = null;
		}
    }
}