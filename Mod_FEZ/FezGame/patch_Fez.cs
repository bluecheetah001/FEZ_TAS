using FezTas;
using Microsoft.Xna.Framework;
using System;

namespace FezGame
{
    public class patch_Fez : Fez
    {
        private TimeSpan TrueTime = TimeSpan.Zero;
        private TimeSpan TrueEllapse = TimeSpan.Zero;
        private TimeSpan AccumulatedTime = TimeSpan.Zero;
        private TimeSpan LastGameTime = TimeSpan.Zero;

        private extern void orig_Initialize();
        protected override void Initialize()
        {
            Version += " T0.1";

            orig_Initialize();
            Window.Title = "FEZ TAS";
            Tas.Initialize(this);
        }

        private extern void orig_Update(GameTime gameTime);
        protected override void Update(GameTime gameTime)
        {
            Tas.Update();

            double millis = (gameTime.TotalGameTime - LastGameTime).TotalMilliseconds;
            LastGameTime = gameTime.TotalGameTime;
            TrueEllapse = TimeSpan.Zero;

            if (Tas.PlaySpeed <= 0)
            {
                if (Tas.ForceFrame)
                {
                    Tas.ForceFrame = false;
                    TrueUpdate();
                }
            }
            else
            {
                AccumulatedTime += TimeSpan.FromMilliseconds(millis * Tas.PlaySpeed);
                while (AccumulatedTime >= TargetElapsedTime)
                {
                    AccumulatedTime -= TargetElapsedTime;
                    TrueUpdate();
                    if (Tas.ForceFrame)
                    {
                        Tas.ForceFrame = false;
                        AccumulatedTime = TimeSpan.Zero;
                        break;
                    }
                }
            }
        }

        private void TrueUpdate()
        {
            Tas.PreFrame();
            TrueTime += TargetElapsedTime;
            TrueEllapse += TargetElapsedTime;
            orig_Update(new GameTime(TrueTime, TargetTimeStep));//yes this is a different value
            Tas.PostFrame();
        }

        private extern void orig_Draw(GameTime gameTime);
        protected override void Draw(GameTime gameTime)
        {
            orig_Draw(new GameTime(TrueTime, TrueEllapse));
            TrueEllapse = TimeSpan.Zero;
            Tas.Draw(GraphicsDevice);
        }

        // allow game to run in background
        public new bool IsActive
        {
            [MonoMod.MonoModReplace]
            get
            {
                return true;
            }
            [MonoMod.MonoModReplace]
            set
            {

            }
        }
    }
}
