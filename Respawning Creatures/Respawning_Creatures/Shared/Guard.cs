using UnityEngine;

namespace Respawning_Creatures
{
    public static class Guard
    {
        public static bool IsGamePaused() => Time.timeScale == 0;
    }
}
