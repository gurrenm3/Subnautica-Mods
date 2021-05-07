using Oculus.Newtonsoft.Json;
using System.IO;

namespace Respawning_Creatures.Extensions
{
    public static class SettingsExtensions
    {
        public static void Save(this Settings settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(Settings.settingsPath, json);
        }

        public static Settings Load(this Settings settings)
        {
            if (File.Exists(Settings.settingsPath))
            {
                var json = File.ReadAllText(Settings.settingsPath);
                return JsonConvert.DeserializeObject<Settings>(json);
            }
            else
            {
                settings.Save();
                return settings;
            }
        }
    }
}
