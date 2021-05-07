namespace Respawning_Creatures
{
    public class Settings
    {
        public static readonly string settingsPath = $"{QModPatcher.modsDir}\\settings.json";
        public static Settings instance = new Settings();

        public bool leviathansRespawn { get; set; } = true;
    }
}