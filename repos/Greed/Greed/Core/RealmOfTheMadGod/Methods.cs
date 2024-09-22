using Swed64;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Greed.Core
{
    public class Methods(Swed swed)
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        const int keyVk_R = 0x52;
        const int keyScan_R = 0x19;
        const int keyVk_F = 0x46;
        const int keyScan_F = 0x33;

        const uint keyDown = 0x0;
        const uint keyUp = 0x2;

        private readonly Swed mem = swed;

        public void Aimbot(IntPtr moduleBase, bool enabled)
        {
            if (enabled)
            {
                mem.WriteInt(mem.ReadPointer(moduleBase, 0x1CD5980, 0x160, 0x80, 0x88) + 0x5C, 1);
            }
            else
            {
                mem.WriteInt(mem.ReadPointer(moduleBase, 0x1CD5980, 0x160, 0x80, 0x88) + 0x5C, 0);
            }
        }
        public void AutoNexus(IntPtr moduleBase, double healthState, int lastDamage, int currentHealth, int numOfPotions, bool checkbox)
        {
            if (checkbox && healthState <= 0.2 || lastDamage >= currentHealth)
            {
                keybd_event(keyVk_R, keyScan_R, keyDown, 0);
                keybd_event(keyVk_R, keyScan_R, keyUp, 0);
                Thread.Sleep(10000);
            }
        }
        public void AutoPot(IntPtr moduleBase, double healthState, int numOfPotions, bool checkbox)
        {
            if (checkbox && healthState > 0.2 && healthState <= 0.5 && numOfPotions > 0)
            {
                keybd_event(keyVk_F, keyScan_F, keyDown, 0);
                keybd_event(keyVk_F, keyScan_F, keyUp, 0);
                Thread.Sleep(400);
            }
        }
    }
}
