using Greed.Core;
using Greed.Core.Enums;
using Swed64;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Greed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Swed mem = new Swed("RotMG Exalt");
            IntPtr moduleBase = mem.GetModuleBase("UnityPlayer.dll");
            Methods methods = new Methods(mem);

            var renderer = new Renderer();
            renderer.Start().Wait();
            while (true)
            {
                IntPtr pLocal = mem.ReadPointer(moduleBase, 0x1CD5980, 0x160, 0x80, 0x210, 0x38);

                var maxHealth = mem.ReadInt(pLocal, (int)Offsets.MaxHealth);
                var currentHealth = mem.ReadInt(pLocal, (int)Offsets.Health);
                var healthState = currentHealth / (double)maxHealth;

                var numOfPotions = mem.ReadInt(mem.ReadPointer(
                    moduleBase, 0x1CB7C98, 0xB8, 0x10, 0x298, 0x58, 0x20) + 0x1B4);

                var currentSpeed = mem.ReadFloat(pLocal, (int)Offsets.Speed);
                var lastDamage = maxHealth - currentHealth;

                renderer.speed = currentSpeed;
                renderer.maxHealth = maxHealth;
                renderer.health = currentHealth;
                renderer.potions = numOfPotions;
                renderer.healthState = healthState;
                renderer.lastDamage = lastDamage;

                methods.AutoNexus(moduleBase, healthState, lastDamage, currentHealth, numOfPotions, renderer.autoNexus);
                methods.AutoPot(moduleBase, healthState, numOfPotions, renderer.autoPot);
                methods.Aimbot(moduleBase, renderer.aimbot);

                mem.WriteFloat(pLocal, (int)Offsets.Speed, renderer.speed);
            }
        }
    }
}
