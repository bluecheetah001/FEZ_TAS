using MonoMod;

namespace Microsoft.Xna.Framework.Audio
{
    public class OpenALSoundController
    {
        [MonoModReplaceString("\\FEZ\\Debug Log.txt", "\\FEZ_TAS\\Debug Log.txt")]
        [MonoModIgnore]
        extern private static void Log(string message);
    }
}
