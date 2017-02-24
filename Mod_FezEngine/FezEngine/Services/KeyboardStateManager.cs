using Microsoft.Xna.Framework.Input;
using FezEngine.Structure.Input;
using System.Collections.Generic;
using FezTas;
using Microsoft.Xna.Framework;
using MonoMod;

namespace FezEngine.Services
{
    // disable standard keyboard input, and replace it with programatic input
    public class KeyboardStateManager
    {
        // set this at the begining of every frame, and it will be read at the correct point within the frame
        public static TasButtons NextButtons;

        private const Keys KEY_UP         = Keys.Up;
        private const Keys KEY_DOWN       = Keys.Down;
        private const Keys KEY_LEFT       = Keys.Left;
        private const Keys KEY_RIGHT      = Keys.Right;
        private const Keys KEY_ROT_LEFT   = Keys.A;
        private const Keys KEY_ROT_RIGHT  = Keys.D;
        private const Keys KEY_JUMP       = Keys.Space;
        private const Keys KEY_GRAB       = Keys.LeftControl;
        private const Keys KEY_TALK       = Keys.LeftShift;
        private const Keys KEY_PAUSE      = Keys.Enter;
        private const Keys KEY_MAP        = Keys.Escape;
        private const Keys KEY_INVENTORY  = Keys.Tab;
        private const Keys KEY_ZOOM_IN    = Keys.W;
        private const Keys KEY_ZOOM_OUT   = Keys.S;
        private const Keys KEY_FP_VIEW    = Keys.RightAlt;
        private const Keys KEY_CLAMP_VIEW = Keys.RightShift;
        private const Keys KEY_VIEW_UP    = Keys.I;
        private const Keys KEY_VIEW_DOWN  = Keys.K;
        private const Keys KEY_VIEW_LEFT  = Keys.J;
        private const Keys KEY_VIEW_RIGHT = Keys.L;

        private static Dictionary<Keys, FezButtonState> TasKeys = new Dictionary<Keys, FezButtonState>()
        {
            [KEY_UP        ] = FezButtonState.Up,
            [KEY_DOWN      ] = FezButtonState.Up,
            [KEY_LEFT      ] = FezButtonState.Up,
            [KEY_RIGHT     ] = FezButtonState.Up,
            [KEY_ROT_LEFT  ] = FezButtonState.Up,
            [KEY_ROT_RIGHT ] = FezButtonState.Up,
            [KEY_JUMP      ] = FezButtonState.Up,
            [KEY_GRAB      ] = FezButtonState.Up,
            [KEY_TALK      ] = FezButtonState.Up,
            [KEY_PAUSE     ] = FezButtonState.Up,
            [KEY_MAP       ] = FezButtonState.Up,
            [KEY_INVENTORY ] = FezButtonState.Up,
            [KEY_ZOOM_IN   ] = FezButtonState.Up,
            [KEY_ZOOM_OUT  ] = FezButtonState.Up,
            [KEY_FP_VIEW   ] = FezButtonState.Up,
            [KEY_CLAMP_VIEW] = FezButtonState.Up,
            [KEY_VIEW_UP   ] = FezButtonState.Up,
            [KEY_VIEW_DOWN ] = FezButtonState.Up,
            [KEY_VIEW_LEFT ] = FezButtonState.Up,
            [KEY_VIEW_RIGHT] = FezButtonState.Up,
        };

        // for known TasButton -> Keys, use that. Otherwise the key will never be pressed
        [MonoModReplace]
        public FezButtonState GetKeyState(Keys key)
        {
            if (TasKeys.ContainsKey(key))
            {
                return TasKeys[key];
            }
            return FezButtonState.Up;
        }

        // read the 'NextButtons' keyboard instead of the given KeyboardState
        [MonoModReplace]
        public void Update(KeyboardState state, GameTime time)
        {
            StepKey(KEY_UP        , NextButtons.Up       );
            StepKey(KEY_DOWN      , NextButtons.Down     );
            StepKey(KEY_LEFT      , NextButtons.Left     );
            StepKey(KEY_RIGHT     , NextButtons.Right    );
            StepKey(KEY_ROT_LEFT  , NextButtons.RotLeft  );
            StepKey(KEY_ROT_RIGHT , NextButtons.RotRight );
            StepKey(KEY_JUMP      , NextButtons.Jump     );
            StepKey(KEY_GRAB      , NextButtons.Grab     );
            StepKey(KEY_TALK      , NextButtons.Talk     );
            StepKey(KEY_PAUSE     , NextButtons.Pause    );
            StepKey(KEY_MAP       , NextButtons.Map      );
            StepKey(KEY_INVENTORY , NextButtons.Inventory);
            StepKey(KEY_ZOOM_IN   , NextButtons.ZoomIn   );
            StepKey(KEY_ZOOM_OUT  , NextButtons.ZoomOut  );
            StepKey(KEY_FP_VIEW   , NextButtons.FpView   );
            StepKey(KEY_CLAMP_VIEW, NextButtons.ClampView);
            StepKey(KEY_VIEW_UP   , NextButtons.ViewUp   );
            StepKey(KEY_VIEW_DOWN , NextButtons.ViewDown );
            StepKey(KEY_VIEW_LEFT , NextButtons.ViewLeft );
            StepKey(KEY_VIEW_RIGHT, NextButtons.ViewRight);

            NextButtons = TasButtons.NONE;
        }

        private static void StepKey(Keys key, bool pressed)
        {
            TasKeys[key] = TasKeys[key].NextState(pressed);
        }
    }
}
