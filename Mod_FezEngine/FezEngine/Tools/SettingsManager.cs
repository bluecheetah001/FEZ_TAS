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

        public static extern void orig_Apply();
        public static void Apply()
        {
            orig_Apply();
            ServiceHelper.Game.IsMouseVisible = true;
        }
    }
}
