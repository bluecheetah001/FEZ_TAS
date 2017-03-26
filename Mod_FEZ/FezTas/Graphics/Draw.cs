using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FezTas.Graphics
{
    class Draw
    {
        private static Texture2D Texture;
        public static Color Color;

        public static void Initialize(Game game)
        {
            Texture = new Texture2D(game.GraphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });
            Color = Color.Black;
        }

        public static void Rectangle(Vector2 position, Vector2 size)
        {
            Graphics.DrawTexture(Texture, Color, size, position);
        }

        public static void Line(Vector2 start, Vector2 end)
        {
            float length = (end - start).Length();
            float rotation = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            Graphics.DrawTexture(Texture, Color, rotation, Vector2.Zero, new Vector2(length, 1), start);
        }
    }
}
