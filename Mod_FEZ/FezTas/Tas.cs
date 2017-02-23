using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Text;

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

        public static void Draw(GraphicsDevice graphics)
        {
        }
    }
}
