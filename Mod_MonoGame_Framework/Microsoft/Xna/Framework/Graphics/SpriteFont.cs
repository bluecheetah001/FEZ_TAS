using MonoMod;
using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
    public class SpriteFont
    {
        public const int TasFontHeight = 14;

        [MonoModIgnore]
        public extern SpriteFont(Texture2D texture, List<Rectangle> glyphBounds, List<Rectangle> cropping, List<char> characters, int lineSpacing, float spacing, List<Vector3> kerning, char? defaultCharacter);

        // duplicates the font
        // makes all the numbers have the same width
        // mves all brackets down so they do not extend above every other character
        // fixed the height so there is not a lot of extra space
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


                if ("[](){}|$".IndexOf(glyph.Character) >= 0)
                {
                    cropping.Add(new Rectangle(0, 0, glyph.Cropping.Width, TasFontHeight));
                }
                else
                {
                    cropping.Add(new Rectangle(0, glyph.Cropping.Y - 8, glyph.Cropping.Width, TasFontHeight));
                }

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

            return new SpriteFont(_texture, glyphBounds, cropping, characters, TasFontHeight, 1, kerning, '?');
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
