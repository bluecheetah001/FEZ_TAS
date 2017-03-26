using MonoMod;

namespace Microsoft.Xna.Framework.Graphics
{
    public static class patch_GraphicsExtensions
    {
        [MonoModReplaceString("\\FEZ\\Debug Log.txt", "\\FEZ_TAS\\Debug Log.txt")]
        [MonoModIgnore]
        extern private static void LogToFile(GraphicsExtensions.LogSeverity severity, string message);
    }
}
