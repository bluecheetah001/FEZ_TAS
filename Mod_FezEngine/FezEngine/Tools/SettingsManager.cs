using MonoMod;

namespace FezEngine.Tools
{
    // load settings from FEZ_TAS/Settings instead of FEZ/Settings
    public static class SettingsManager
    {
        [MonoModReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        static extern SettingsManager();

        [MonoModReplaceString("FEZ", "FEZ_TAS")]
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
