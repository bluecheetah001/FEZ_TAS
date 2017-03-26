using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Common;
using System;
using Microsoft.Xna.Framework.Graphics.Localization;

namespace FezTas.Graphics
{
    public static class Text
    {
        private static SpriteFont Font;
        private static int _Horizontal;
        private static int _Vertical;
        public static int HorizontalSpacing
        {
            get { return _Horizontal; }
            set
            {
                Font.Spacing = value / ScaleF;
                _Horizontal = value;
            }
        }
        public static int VerticalSpacing
        {
            get { return _Vertical; }
            set
            {
                Font.LineSpacing = SpriteFont.TasFontHeight + (int)(value / ScaleF);
                _Vertical = value;
            }
        }

        public static Color Foreground;
        public static Color Shadow;
        public static Vector2 DropShadow;

        private static int _Scale;
        private static float ScaleF { get { return _Scale / 2f; } }
        public static int Scale
        {
            get { return _Scale; }
            set
            {
                _Scale = value;
                VerticalSpacing = VerticalSpacing;
                HorizontalSpacing = HorizontalSpacing;
            }
        }

        public static void Initialize(Game game)
        {
            Font = Inspection.Load<SpriteFont>("Fonts/Latin Small").createTasFont();
            WordWrap.SetCallback(TasCharWidth, null);

            Foreground = Color.White;
            Shadow = Color.Black;
            DropShadow = new Vector2(1, 1);
            HorizontalSpacing = 1;
            VerticalSpacing = 1;
            Scale = 2;
        }

        public static void Draw(Vector2 topLeft, string text)
        {
            if (DropShadow != Vector2.Zero)
            {
                Graphics.DrawText(Font, text, Shadow, ScaleF, topLeft + DropShadow);
            }
            Graphics.DrawText(Font, text, Foreground, ScaleF, topLeft);
        }

        public static string Wrap(string text, int maxWidth)
        {
            return WordWrap.Split(text, Font, maxWidth);
        }

        public static Vector2 Measure(string text)
        {
            // this includes VerticalSpacing after the last line, not sure if I want to remove it yet or not
            return Font.MeasureString(text) * ScaleF;
        }

        // custom CharWidth that accounts for scale when called with TAS's font
        private static uint TasCharWidth(SpriteFont spriteFont, char c)
        {
            if (spriteFont == Font)
            {
                return (uint)(spriteFont.MeasureString(c.ToString()).X * ScaleF);
            }
            else
            {
                return (uint)(spriteFont.MeasureString(c.ToString()).X);
            }
        }
    }
}
