using FezEngine.Services;
using FezEngine.Structure.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoMod;
using System;

namespace FezEngine.Components
{
    // fixes some weird issues with going back and forth with inputs coming from keyboard and gamepad
    public class InputManager
    {
        private static FezButtonState UseGamepadState = FezButtonState.Down;
        public static bool UseGamepad
        {
            get
            {
                return UseGamepadState.IsDown();
            }
            set
            {
                UseGamepadState = UseGamepadState.NextState(value);
            }
        }

        // replace to make the transitions between playback and record much smoother
        [MonoModReplace]
        public void Update(GameTime gameTime)
        {
            // keyboard
            KeyboardState.Update(Keyboard.GetState(), gameTime);
            Back = KeyboardState.OpenMap;
            Start = KeyboardState.Pause;
            Jump = KeyboardState.Jump;
            GrabThrow = KeyboardState.GrabThrow;
            CancelTalk = KeyboardState.CancelTalk;
            Down = KeyboardState.Down;
            Up = KeyboardState.Up;
            Left = KeyboardState.Left;
            Right = KeyboardState.Right;
            OpenInventory = KeyboardState.OpenInventory;
            RotateLeft = KeyboardState.RotateLeft;
            RotateRight = KeyboardState.RotateRight;
            MapZoomIn = KeyboardState.MapZoomIn;
            MapZoomOut = KeyboardState.MapZoomOut;
            FpsToggle = KeyboardState.FpViewToggle;
            ClampLook = KeyboardState.ClampLook;

            FreeLook = new Vector2(KeyboardState.LookRight.IsDown() ? 1f : (KeyboardState.LookLeft.IsDown() ? -1f : 0.0f), KeyboardState.LookUp.IsDown() ? 1f : (KeyboardState.LookDown.IsDown() ? -1f : 0.0f));

            if (gamepad && UseGamepadState != FezButtonState.Up)
            {
                PlayerIndex[] players = ControllerIndex.Any.GetPlayers();
                for (int index = 0; index < players.Length; ++index)
                {
                    GamepadState gamepadState = GamepadsManager[players[index]];
                    if (!gamepadState.Connected)
                    {
                        if (gamepadState.NewlyDisconnected && ActiveControllerDisconnected != null)
                        {
                            ActiveControllerDisconnected(players[index]);
                        }
                    }
                    else
                    {
                        ClampLook = Coalesce(ClampLook, Transition(gamepadState.RightStick.Clicked.State));
                        FpsToggle = Coalesce(FpsToggle, Transition(gamepadState.LeftStick.Clicked.State));
                        Back = Coalesce(Back, Transition(gamepadState.Back));
                        Start = Coalesce(Start, Transition(gamepadState.Start));
                        Jump = Coalesce(Jump, Transition(gamepadState.A.State));
                        GrabThrow = Coalesce(GrabThrow, Transition(gamepadState.X.State));
                        CancelTalk = Coalesce(CancelTalk, Transition(gamepadState.B.State));
                        OpenInventory = Coalesce(OpenInventory, Transition(gamepadState.Y.State));
                        Up = Coalesce(Up, Coalesce(Transition(gamepadState.DPad.Up.State), Transition(gamepadState.LeftStick.Up.State)));
                        Down = Coalesce(Down, Coalesce(Transition(gamepadState.DPad.Down.State), Transition(gamepadState.LeftStick.Down.State)));
                        Left = Coalesce(Left, Coalesce(Transition(gamepadState.DPad.Left.State), Transition(gamepadState.LeftStick.Left.State)));
                        Right = Coalesce(Right, Coalesce(Transition(gamepadState.DPad.Right.State), Transition(gamepadState.LeftStick.Right.State)));
                        MapZoomIn = Coalesce(MapZoomIn, Transition(gamepadState.RightShoulder.State));
                        MapZoomOut = Coalesce(MapZoomOut, Transition(gamepadState.LeftShoulder.State));

                        // for input recording consistency, we always assume StrictRotation
                        RotateLeft = Coalesce(RotateLeft, Transition(gamepadState.LeftTrigger.State));
                        RotateRight = Coalesce(RotateRight, Transition(gamepadState.RightTrigger.State));

                        // special handling of FreeLook since buttons are not defined on gamepad
                        // but we must emulate keyboard buttons
                        bool right = FreeLook.X > 0 || gamepadState.RightStick.Right.State.IsDown();
                        bool left = FreeLook.X < 0 || gamepadState.RightStick.Left.State.IsDown();
                        bool up = FreeLook.Y > 0 || gamepadState.RightStick.Up.State.IsDown();
                        bool down = FreeLook.Y < 0 || gamepadState.RightStick.Down.State.IsDown();
                        FreeLook = new Vector2(right ? 1f : (left ? -1f : 0.0f), up ? 1f : (down ? -1f : 0.0f));
                    }
                }
            }

            // keyboard and gamepadhave different ways to calculate exact up, so force keyboard calculation
            ExactUp = Up;
            // calculate movement after gamepad so corrected coalesce applies to movement as well
            Movement = new Vector2(Right.IsDown() ? 1f : (Left.IsDown() ? -1f : 0.0f), Up.IsDown() ? 1f : (Down.IsDown() ? -1f : 0.0f));

            // mouse is disabled in FEZ TAS, because we might add TAS controls based on mouse

            //finish enabling/disabling gamepad
            UseGamepadState = UseGamepadState.IsDown() ? FezButtonState.Down : FezButtonState.Up;
        }

        // symetric coalescing that prefers the pressed state, unlike the original coalescing
        private static FezButtonState Coalesce(FezButtonState first, FezButtonState second)
        {
            if (first == FezButtonState.Up)
            {
                return second;
            }
            if (second == FezButtonState.Up)
            {
                return first;
            }
            if (first == second)
            {
                return first;
            }
            return FezButtonState.Down;
        }

        // we transition gamepad buttons here because it involves a lot more injection to do it in GamepadsManager
        private FezButtonState Transition(FezButtonState state)
        {
            if (UseGamepadState == FezButtonState.Down)
            {
                return state;
            }
            if (state == FezButtonState.Down || state == UseGamepadState)
            {
                return UseGamepadState;
            }

            return FezButtonState.Up;
        }

        // fields we need access to
        [MonoModIgnore]
        private bool gamepad;

        [MonoModIgnore]
        public IKeyboardStateManager KeyboardState { private get; set; }

        [MonoModIgnore]
        public IGamepadsManager GamepadsManager { private get; set; }

        [MonoModIgnore]
        public event Action<PlayerIndex> ActiveControllerDisconnected;


        // the buttons
        [MonoModIgnore]
        public FezButtonState GrabThrow { get; private set; }

        [MonoModIgnore]
        public Vector2 Movement { get; private set; }

        [MonoModIgnore]
        public Vector2 FreeLook { get; private set; }

        [MonoModIgnore]
        public FezButtonState Jump { get; private set; }

        [MonoModIgnore]
        public FezButtonState Back { get; private set; }

        [MonoModIgnore]
        public FezButtonState OpenInventory { get; private set; }

        [MonoModIgnore]
        public FezButtonState Start { get; private set; }

        [MonoModIgnore]
        public FezButtonState RotateLeft { get; private set; }

        [MonoModIgnore]
        public FezButtonState RotateRight { get; private set; }

        [MonoModIgnore]
        public FezButtonState CancelTalk { get; private set; }

        [MonoModIgnore]
        public FezButtonState Up { get; private set; }

        [MonoModIgnore]
        public FezButtonState Down { get; private set; }

        [MonoModIgnore]
        public FezButtonState Left { get; private set; }

        [MonoModIgnore]
        public FezButtonState Right { get; private set; }

        [MonoModIgnore]
        public FezButtonState ClampLook { get; private set; }

        [MonoModIgnore]
        public FezButtonState FpsToggle { get; private set; }

        [MonoModIgnore]
        public FezButtonState ExactUp { get; private set; }

        [MonoModIgnore]
        public FezButtonState MapZoomIn { get; private set; }

        [MonoModIgnore]
        public FezButtonState MapZoomOut { get; private set; }
    }
}
