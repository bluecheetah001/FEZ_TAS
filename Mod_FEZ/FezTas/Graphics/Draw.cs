using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FezTas.Graphics
{
    public static class Draw
    {
        private static GraphicsDevice GraphicsDevice;
        private static SpriteBatch Batch;

        private static SpriteFont Font;
        public static int LineSpacing { get { return Font.LineSpacing; } set { Font.LineSpacing = value; } }
        public static int CharSpacing { get { return (int)Font.Spacing; } set { Font.Spacing = value; } }

        public static Color Foreground;
        public static Color Shadow;
        public static Vector2 DropShadow;

        public static void Initialize(Game game)
        {
            GraphicsDevice = game.GraphicsDevice;
            Batch = new SpriteBatch(GraphicsDevice);
            Font = Inspection.Load<SpriteFont>("Fonts/Latin Small").createTasFont();

            Foreground = Color.White;
            Shadow = Color.Black;
            DropShadow = new Vector2(1, 1);
        }

        // only called from Tas.Draw()
        public static void Start()
        {
            Batch.Begin();
        }

        // only called from Tas.Draw()
        public static void End()
        {
            Batch.End();
        }

        public static void Text(int x, int y, string text)
        {
            Vector2 pos = new Vector2(x, y-8);
            if (DropShadow != Vector2.Zero)
            {
                Batch.DrawString(Font, text, pos + DropShadow, Shadow);
            }
            Batch.DrawString(Font, text, pos, Foreground);
        }
    }
}
