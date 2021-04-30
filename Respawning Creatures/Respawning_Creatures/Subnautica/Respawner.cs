using System.Collections;
using UnityEngine;

namespace Respawning_Creatures
{
    public static class Respawner
    {
        public static IEnumerator RespawnCoroutine(Respawn respawn)
        {
            float timePassed = (float)DayNightCycle.main.timePassed;
            float waitTime = (float)System.Math.Round(respawn.spawnTime - timePassed);

            yield return new WaitForSecondsRealtime(waitTime);

            respawn.Spawn();
            UnityEngine.Object.Destroy(respawn.gameObject);
        }
    }
}
