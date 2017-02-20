using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FezEngine.Tools
{
    // just for public read access
    [MonoMod.MonoModIgnore]
    public class DisplayModeEqualityComparer : IEqualityComparer<DisplayMode>
    {
        public static readonly DisplayModeEqualityComparer Default;

        public extern bool Equals(DisplayMode x, DisplayMode y);

        public extern int GetHashCode(DisplayMode obj);
    }
}
