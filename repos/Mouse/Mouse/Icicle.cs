using System.Runtime.InteropServices;

namespace Frost
{
    public abstract class Mouse
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);
        [DllImport("user32.dll")]
        private static extern UIntPtr GetMessageExtraInfo();

        #region
        [StructLayout(LayoutKind.Sequential)]
        public struct Input
        {
            internal uint type;
            internal InputUnion U;
            internal static int Size { get { return Marshal.SizeOf(typeof(Input)); } }
        }


        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)]
            internal MouseInput mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MouseInput
        {
            internal int dx;
            internal int dy;
            internal int MouseData;
            internal MouseEVENTF dwFlags;
            internal uint time;
            internal UIntPtr dwExtraInfo;
        }
        #endregion
        #region flags
        [Flags]
        internal enum MouseEVENTF : uint
        {
            ABSOLUTE = 0x8000,
            HWHEEL = 0x01000,
            MOVE = 0x0001,
            MOVE_NOCOALESCE = 0x2000,
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            VIRTUALDESK = 0x4000,
            WHEEL = 0x0800,
            XDOWN = 0x0080,
            XUP = 0x0100
        }
        #endregion
        public static void SimMouse()
        {
            Input[] Inputs = new Input[]
            {
                new Input
                {
                   type = 0,
                   U = new InputUnion
                   {
                       mi = new MouseInput
                       {
                           dx = 100,
                           dy = 100,
                           dwFlags = (MouseEVENTF.MOVE | MouseEVENTF.LEFTDOWN),
                           dwExtraInfo = GetMessageExtraInfo()
                       }
                   }
                },
                new Input
                {
                    type = 0,
                    U = new InputUnion
                    {
                        mi = new MouseInput
                        {
                            dwFlags = MouseEVENTF.LEFTUP,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                }
            };
            SendInput((uint)Inputs.Length, Inputs, Marshal.SizeOf(typeof(Input)));
        }
        public static void MouseClick()
        {
            Input[] Inputs =
            {
                new Input
                {
                    type = 0,
                    U = new InputUnion
                    {
                        mi = new MouseInput
                        {
                            dwFlags = MouseEVENTF.LEFTDOWN | MouseEVENTF.LEFTUP,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                }
            };
            SendInput((uint)Inputs.Length, Inputs, Marshal.SizeOf(typeof(Input)));
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);
    }
}



