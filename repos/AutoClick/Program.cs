using System.Runtime.InteropServices;

namespace AutoClick
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, UIntPtr dwExtraInfo);
        [DllImport("user32.dll")]
        static extern UIntPtr GetMessageExtraInfo();

        static void Main(string[] args)
        {
            while (true)
            {
                if (GetAsyncKeyState(0x5) < 0)
                {
                    mouse_event(0x4, GetMessageExtraInfo()); //up
                    mouse_event(0x2, GetMessageExtraInfo()); //down
                }
                Thread.Sleep(20);
            }
        }
    }
}
