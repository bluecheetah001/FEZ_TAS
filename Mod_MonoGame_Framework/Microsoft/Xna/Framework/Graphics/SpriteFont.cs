using MonoMod;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
    public class SpriteFont
    {
        [MonoModIgnore]
        public extern SpriteFont(Texture2D texture, List<Rectangle> glyphBounds, List<Rectangle> cropping, List<char> characters, int lineSpacing, float spacing, List<Vector3> kerning, char? defaultCharacter);

        // duplicates the font, but makes all the numbers have the same width by using known hardcoded valuse
        public SpriteFont createTasFont()
        {
            List<char> characters = new List<char>();
            List<Rectangle> glyphBounds = new List<Rectangle>();
            List<Rectangle> cropping = new List<Rectangle>();
            List<Vector3> kerning = new List<Vector3>();

            foreach (Glyph glyph in _glyphs.Values)
            {
                characters.Add(glyph.Character);
                glyphBounds.Add(glyph.BoundsInTexture);
                cropping.Add(glyph.Cropping);
                if (glyph.Character == '1')
                {
                    kerning.Add(new Vector3(2, 4, 4));
                }
                else if (glyph.Character == '7')
                {
                    kerning.Add(new Vector3(0, 8, 2));
                }
                else
                {
                    kerning.Add(new Vector3(glyph.LeftSideBearing, glyph.Width, glyph.RightSideBearing));
                }
            }

            return new SpriteFont(_texture, glyphBounds, cropping, characters, 18, 1, kerning, '?');
        }

        // the fields we need access to
        [MonoModIgnore]
        public readonly Dictionary<char, Glyph> _glyphs;

        [MonoModIgnore]
        public readonly Texture2D _texture;

        [MonoModIgnore]
        public struct Glyph
        {
            public char Character;
            public Rectangle BoundsInTexture;
            public Rectangle Cropping;
            public float LeftSideBearing;
            public float RightSideBearing;
            public float Width;
        }
    }
}
