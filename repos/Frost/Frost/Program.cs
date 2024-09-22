using System.Runtime.InteropServices;

namespace Frost
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(5000);
                Console.Clear();
                GetWindowThreadProcessId(GetForegroundWindow(), out uint a);
                Console.WriteLine(a);
            }
        }
    }
}

