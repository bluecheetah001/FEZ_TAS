using MonoMod;
using TasRules;

namespace Microsoft.Xna.Framework.Graphics
{
    public static class patch_GraphicsExtensions
    {
        [ReplaceString("\\FEZ\\Debug Log.txt", "\\FEZ_TAS\\Debug Log.txt")]
        [MonoModIgnore]
        extern private static void LogToFile(GraphicsExtensions.LogSeverity severity, string message);
    }
}
