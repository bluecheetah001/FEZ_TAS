using FezTas.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace FezTas
{
    public static class Tas
    {
        public static readonly string Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FEZ_TAS");

        public static int GlobalFrames = 0;

        // Play Speed Variables
        public static bool ForceFrame = false; // should only be set to false by patch_Fez.Update()
        private static double _PlaySpeed = 1;
        public static double PlaySpeed
        {
            get
            {
                return _PlaySpeed;
            }
            set
            {
                _PlaySpeed = value;
                ForceFrame = true;
            }
        }

        public static void Initialize(Game game)
        {
            Inspection.Initialize(game);
            Text.Initialize(game);
            Graphics.Graphics.Initialize(game);

            Text.DropShadow = Vector2.Zero;
            Graphics.Draw.Color = new Color(0, 0, 0, .5f);
            Text.Scale = 1;
        }

        public static void Update()
        {
        }

        public static void PreFrame()
        {
        }

        public static void PostFrame()
        {
            GlobalFrames++;
        }

        public static void Draw()
        {
            Graphics.Graphics.Start();
            try
            {
                string str = $"TAS v0.1\n{GlobalFrames}";
                Graphics.Draw.Rectangle(new Vector2(0, 0), Text.Measure(str) + new Vector2(1, 1));
                Text.Draw(new Vector2(1, 1), str);
            }
            finally
            {
                Graphics.Graphics.End();
            }
        }
    }
}
