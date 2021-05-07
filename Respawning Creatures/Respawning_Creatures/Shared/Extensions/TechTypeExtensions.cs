namespace Respawning_Creatures.Extensions
{
    public static class TechTypeExtensions
    {
        public static bool IsLeviathan(this TechType techType)
        {
            return techType.ToString().Contains("Leviathan");
        }
    }
}
