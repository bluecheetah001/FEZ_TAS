using FezEngine.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FezTas.Graphics
{
    class Graphics
    {
        private static GraphicsDevice GraphicsDevice;
        private static SpriteBatch Batch;
        public static int Width { get { return GraphicsDevice.Viewport.Width; } }
        public static int Height { get { return GraphicsDevice.Viewport.Height; } }

        public static void Initialize(Game game)
        {
            GraphicsDevice = game.GraphicsDevice;
            Batch = new SpriteBatch(GraphicsDevice);
            Draw.Initialize(game);
            Text.Initialize(game);
        }

        // only called from Tas.Draw()
        public static void Start()
        {
            Batch.BeginPoint();
        }

        // only called from Tas.Draw()
        public static void End()
        {
            Batch.End();
        }

        // SpriteBatch.Draw replacements
        public static void DrawTexture(Texture2D texture, Rectangle? source, Color color, float rotation, Vector2 origin, Vector2 scale, Vector2 position)
        {
            Batch.Draw(texture, position, source, color, rotation, origin, scale, SpriteEffects.None, 0);
        }

        public static void DrawTexture(Texture2D texture, Color color, float rotation, Vector2 origin, Vector2 scale, Vector2 position)
        {
            DrawTexture(texture, null, color, rotation, origin, scale, position);
        }

        public static void DrawTexture(Texture2D texture, Color color, Vector2 scale, Vector2 position)
        {
            DrawTexture(texture, null, color, 0, Vector2.Zero, scale, position);
        }

        // SpriteBatch.DrawString replacements
        public static void DrawText(SpriteFont font, string text, Color color, float rotation, Vector2 origin, Vector2 scale, Vector2 position)
        {
            Batch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0);
        }

        public static void DrawText(SpriteFont font, string text, Color color, Vector2 scale, Vector2 position)
        {
            DrawText(font, text, color, 0, Vector2.Zero, scale, position);
        }

        public static void DrawText(SpriteFont font, string text, Color color, float scale, Vector2 position)
        {
            DrawText(font, text, color, 0, Vector2.Zero, new Vector2(scale, scale), position);
        }
    }
}
