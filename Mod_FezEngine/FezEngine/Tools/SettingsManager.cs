using ContentSerialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FezEngine.Tools
{
    // load settings from FEZ_TAS/Settings instead of FEZ/Settings
    public static class patch_SettingsManager
    {
        // static non-constant initializers get placed into static constructor
        // so we have to rewrite the initializer since the static constructor is getting replaced
        [MonoMod.MonoModIgnore]
        public static readonly List<DisplayMode> Resolutions = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.Distinct(DisplayModeEqualityComparer.Default).Where((x =>
        {
            if (x.Width < 1280)
                return x.Height >= 720;
            return true;
        })).OrderBy((x => x.Width)).ThenBy((x => x.Height)).ToList();

        [MonoMod.MonoModIgnore]
        public static readonly DisplayMode NativeResolution = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

        [MonoMod.MonoModIgnore]
        public static readonly Settings Settings;

        // this is an exact copy from original, except that "FEZ" is replaced with "FES_TAS"
        [MonoMod.MonoModReplace]
        [MonoMod.MonoModConstructor]
        static patch_SettingsManager()
        {
            if (SettingsManager.Resolutions.Any(x =>
            {
                if (x.Width >= 1280)
                    return x.Height >= 720;
                return false;
            }))
            {
                SettingsManager.Resolutions.RemoveAll(x =>
                {
                    if (x.Width < 1280)
                        return true;
                    if (x.Height < 720)
                        return x != SettingsManager.NativeResolution;
                    return false;
                });
            }
            string str1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FEZ_TAS");
            if (!Directory.Exists(str1))
            {
                Directory.CreateDirectory(str1);
            }
            string str2 = Path.Combine(str1, "Settings");
            SettingsManager.FirstOpen = !File.Exists(str2);
            if (SettingsManager.FirstOpen)
            {
                Settings = new Settings();
            }
            else
            {
                try
                {
                    Settings = SdlSerializer.Deserialize<Settings>(str2);
                }
                catch (Exception)
                {
                    Settings = new Settings();
                }
            }
            SdlGamePad.GetState(PlayerIndex.One);
            Settings.ApplyGamepadMapping();
            Culture.Language = SettingsManager.Settings.Language;
            SettingsManager.Save();
        }

        public static void Save()
        {
            SdlSerializer.Serialize(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FEZ_TAS"), "Settings"), SettingsManager.Settings);
        }
    }
}
