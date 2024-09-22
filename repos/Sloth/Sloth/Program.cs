using Sloth.Core;
using Sloth.Core.Enums;
using Swed32;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;


namespace Sloth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Swed mem = new Swed("ac_client");
            //IntPtr moduleBase = mem.GetModuleBase(".exe");
            //var eServices = new EntityServices(mem, moduleBase);
            //Entity myself;
            //HashSet<Entity> entities;
            //while (true)
            //{
            //    myself = eServices.GetLocalPlayer();
            //    entities = eServices.GetEntities(myself);

            //    mem.WriteInt(myself.Address, (int)Offsets.AssaultAmmo, 120);
            //    mem.WriteInt(myself.Address, (int)Offsets.AssaultFireWait, 30);
            //    mem.WriteInt(myself.Address, (int)Offsets.Health, 100);

            //    eServices.Aimbot(eServices.ClosestEnemy(entities, myself), myself);
            //    Thread.Sleep(0);
            //}
            Thread imgui = new Thread(Render);
            imgui.Start();
        }
        public static void Render()
        {
            RenderClass render = new RenderClass();
            render.Start().Wait();
        }
    }
}
