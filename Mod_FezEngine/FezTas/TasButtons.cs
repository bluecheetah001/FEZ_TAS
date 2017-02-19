using System;
using System.Text;

namespace FezTas
{
    // the buttons that can be programatically input by the TAS
    public struct TasButtons
    {
        public static readonly TasButtons NONE       = new TasButtons(_NONE      );
        public static readonly TasButtons UP         = new TasButtons(_UP        );
        public static readonly TasButtons DOWN       = new TasButtons(_DOWN      );
        public static readonly TasButtons LEFT       = new TasButtons(_LEFT      );
        public static readonly TasButtons RIGHT      = new TasButtons(_RIGHT     );
        public static readonly TasButtons ROT_LEFT   = new TasButtons(_ROT_LEFT  );
        public static readonly TasButtons ROT_RIGHT  = new TasButtons(_ROT_RIGHT );
        public static readonly TasButtons JUMP       = new TasButtons(_JUMP      );
        public static readonly TasButtons GRAB       = new TasButtons(_GRAB      );
        public static readonly TasButtons TALK       = new TasButtons(_TALK      );
        public static readonly TasButtons PAUSE      = new TasButtons(_PAUSE     );
        public static readonly TasButtons MAP        = new TasButtons(_MAP       );
        public static readonly TasButtons INVENTORY  = new TasButtons(_INVENTORY );
        public static readonly TasButtons ZOOM_IN    = new TasButtons(_ZOOM_IN   );
        public static readonly TasButtons ZOOM_OUT   = new TasButtons(_ZOOM_OUT  );
        public static readonly TasButtons FP_VIEW    = new TasButtons(_FP_VIEW   );
        public static readonly TasButtons CLAMP_VIEW = new TasButtons(_CLAMP_VIEW);
        public static readonly TasButtons VIEW_UP    = new TasButtons(_VIEW_UP   );
        public static readonly TasButtons VIEW_DOWN  = new TasButtons(_VIEW_DOWN );
        public static readonly TasButtons VIEW_LEFT  = new TasButtons(_VIEW_LEFT );
        public static readonly TasButtons VIEW_RIGHT = new TasButtons(_VIEW_RIGHT);

        public bool Up        { get { return (Value & _UP        ) == _UP        ; } }
        public bool Down      { get { return (Value & _DOWN      ) == _DOWN      ; } }
        public bool Left      { get { return (Value & _LEFT      ) == _LEFT      ; } }
        public bool Right     { get { return (Value & _RIGHT     ) == _RIGHT     ; } }
        public bool RotLeft   { get { return (Value & _ROT_LEFT  ) == _ROT_LEFT  ; } }
        public bool RotRight  { get { return (Value & _ROT_RIGHT ) == _ROT_RIGHT ; } }
        public bool Jump      { get { return (Value & _JUMP      ) == _JUMP      ; } }
        public bool Grab      { get { return (Value & _GRAB      ) == _GRAB      ; } }
        public bool Talk      { get { return (Value & _TALK      ) == _TALK      ; } }
        public bool Pause     { get { return (Value & _PAUSE     ) == _PAUSE     ; } }
        public bool Map       { get { return (Value & _MAP       ) == _MAP       ; } }
        public bool Inventory { get { return (Value & _INVENTORY ) == _INVENTORY ; } }
        public bool ZoomIn    { get { return (Value & _ZOOM_IN   ) == _ZOOM_IN   ; } }
        public bool ZoomOut   { get { return (Value & _ZOOM_OUT  ) == _ZOOM_OUT  ; } }
        public bool FpView    { get { return (Value & _FP_VIEW   ) == _FP_VIEW   ; } }
        public bool ClampView { get { return (Value & _CLAMP_VIEW) == _CLAMP_VIEW; } }
        public bool ViewUp    { get { return (Value & _VIEW_UP   ) == _VIEW_UP   ; } }
        public bool ViewDown  { get { return (Value & _VIEW_DOWN ) == _VIEW_DOWN ; } }
        public bool ViewLeft  { get { return (Value & _VIEW_LEFT ) == _VIEW_LEFT ; } }
        public bool ViewRight { get { return (Value & _VIEW_RIGHT) == _VIEW_RIGHT; } }

        private readonly int Value;

        public TasButtons(int value)
        {
            Value = value;
        }

        public TasButtons(char c)
        {
            Value = ToValue(c);
        }

        public TasButtons(string str)
        {
            int v = 0;
            foreach (char c in str)
            {
                v |= ToValue(c);
            }
            Value = v;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (Up       ) builder.Append(CHAR_UP        );
            if (Down     ) builder.Append(CHAR_DOWN      );
            if (Left     ) builder.Append(CHAR_LEFT      );
            if (Right    ) builder.Append(CHAR_RIGHT     );
            if (RotLeft  ) builder.Append(CHAR_ROT_LEFT  );
            if (RotRight ) builder.Append(CHAR_ROT_RIGHT );
            if (Jump     ) builder.Append(CHAR_JUMP      );
            if (Grab     ) builder.Append(CHAR_GRAB      );
            if (Talk     ) builder.Append(CHAR_TALK      );
            if (Pause    ) builder.Append(CHAR_PAUSE     );
            if (Map      ) builder.Append(CHAR_MAP       );
            if (Inventory) builder.Append(CHAR_INVENTORY );
            if (ZoomIn   ) builder.Append(CHAR_ZOOM_IN   );
            if (ZoomOut  ) builder.Append(CHAR_ZOOM_OUT  );
            if (FpView   ) builder.Append(CHAR_FP_VIEW   );
            if (ClampView) builder.Append(CHAR_CLAMP_VIEW);
            if (ViewUp   ) builder.Append(CHAR_VIEW_UP   );
            if (ViewDown ) builder.Append(CHAR_VIEW_DOWN );
            if (ViewLeft ) builder.Append(CHAR_VIEW_LEFT );
            if (ViewRight) builder.Append(CHAR_VIEW_RIGHT);
            return builder.ToString();
        }

        public static bool operator ==(TasButtons a, TasButtons b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(TasButtons a, TasButtons b)
        {
            return a.Value != b.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Value == ((TasButtons)obj).Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static TasButtons operator |(TasButtons a, TasButtons b)
        {
            return new TasButtons(a.Value | b.Value);
        }

        public static TasButtons operator &(TasButtons a, TasButtons b)
        {
            return new TasButtons(a.Value & b.Value);
        }

        private static int ToValue(char c)
        {
            switch (c)
            {
                case CHAR_UP         : return _UP        ;
                case CHAR_DOWN       : return _DOWN      ;
                case CHAR_LEFT       : return _LEFT      ;
                case CHAR_RIGHT      : return _RIGHT     ;
                case CHAR_ROT_LEFT   : return _ROT_LEFT  ;
                case CHAR_ROT_RIGHT  : return _ROT_RIGHT ;
                case CHAR_JUMP       : return _JUMP      ;
                case CHAR_GRAB       : return _GRAB      ;
                case CHAR_TALK       : return _TALK      ;
                case CHAR_PAUSE      : return _PAUSE     ;
                case CHAR_MAP        : return _MAP       ;
                case CHAR_INVENTORY  : return _INVENTORY ;
                case CHAR_ZOOM_IN    : return _ZOOM_IN   ;
                case CHAR_ZOOM_OUT   : return _ZOOM_OUT  ;
                case CHAR_FP_VIEW    : return _FP_VIEW   ;
                case CHAR_CLAMP_VIEW : return _CLAMP_VIEW;
                case CHAR_VIEW_UP    : return _VIEW_UP   ;
                case CHAR_VIEW_DOWN  : return _VIEW_DOWN ;
                case CHAR_VIEW_LEFT  : return _VIEW_LEFT ;
                case CHAR_VIEW_RIGHT : return _VIEW_RIGHT;
                default : throw new ArgumentException("Unknown Button Character " + c);
            }
        }

        private const int _NONE       = 0x00000;
        private const int _UP         = 0x00001;
        private const int _DOWN       = 0x00002;
        private const int _LEFT       = 0x00004;
        private const int _RIGHT      = 0x00008;
        private const int _ROT_LEFT   = 0x00010;
        private const int _ROT_RIGHT  = 0x00020;
        private const int _JUMP       = 0x00040;
        private const int _GRAB       = 0x00080;
        private const int _TALK       = 0x00100;
        private const int _PAUSE      = 0x00200;
        private const int _MAP        = 0x00400;
        private const int _INVENTORY  = 0x00800;
        private const int _ZOOM_IN    = 0x01000;
        private const int _ZOOM_OUT   = 0x02000;
        private const int _FP_VIEW    = 0x04000;
        private const int _CLAMP_VIEW = 0x08000;
        private const int _VIEW_UP    = 0x10000;
        private const int _VIEW_DOWN  = 0x20000;
        private const int _VIEW_LEFT  = 0x40000;
        private const int _VIEW_RIGHT = 0x80000;

        private const char CHAR_UP         = 'u';
        private const char CHAR_DOWN       = 'd';
        private const char CHAR_LEFT       = 'l';
        private const char CHAR_RIGHT      = 'r';
        private const char CHAR_ROT_LEFT   = 'L';
        private const char CHAR_ROT_RIGHT  = 'R';
        private const char CHAR_JUMP       = 'J';
        private const char CHAR_GRAB       = 'G';
        private const char CHAR_TALK       = 'T';
        private const char CHAR_PAUSE      = 'p';
        private const char CHAR_MAP        = 'm';
        private const char CHAR_INVENTORY  = 'i';
        private const char CHAR_ZOOM_IN    = 'Z';
        private const char CHAR_ZOOM_OUT   = 'z';
        private const char CHAR_FP_VIEW    = 'f';
        private const char CHAR_CLAMP_VIEW = 'c';
        private const char CHAR_VIEW_UP    = '^';
        private const char CHAR_VIEW_DOWN  = 'v';
        private const char CHAR_VIEW_LEFT  = '<';
        private const char CHAR_VIEW_RIGHT = '>';
    }
}
