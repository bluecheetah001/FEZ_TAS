using MonoMod;
using TasRules;

namespace FezEngine.Tools
{
    // load settings from FEZ_TAS/Settings instead of FEZ/Settings
    public static class SettingsManager
    {
        [ReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        static extern SettingsManager();

        [ReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public static extern void Save();
    }
}
