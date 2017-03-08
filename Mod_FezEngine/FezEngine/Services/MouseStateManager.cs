using FezEngine.Structure.Input;
using Microsoft.Xna.Framework;
using MonoMod;

namespace FezEngine.Services
{
    // replace with a no-op class so the mouse cannot be meaningfully used in game
    public class MouseStateManager
    {
        private static MouseButtonState Idle = new MouseButtonState();

        // [MonoModReplace]
        public void Update(GameTime time) { }

        [MonoModReplace]
        MouseButtonState LeftButton { get { return Idle; } }

        [MonoModReplace]
        MouseButtonState MiddleButton { get { return Idle; } }

        [MonoModReplace]
        MouseButtonState RightButton { get { return Idle; } }

        [MonoModReplace]
        int WheelTurns { get { return 0; } }

        [MonoModReplace]
        FezButtonState WheelTurnedUp { get { return FezButtonState.Up; } }

        [MonoModReplace]
        FezButtonState WheelTurnedDown { get { return FezButtonState.Up; } }

        [MonoModReplace]
        Point Position { get { return Point.Zero; } }

        [MonoModReplace]
        Point Movement { get { return Point.Zero; } }
    }
}
