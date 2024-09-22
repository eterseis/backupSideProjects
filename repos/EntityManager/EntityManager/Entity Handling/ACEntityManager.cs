using EntityManager.Entity_Structures;
using EntityManager.Game_Offsets;
using Swed32;
using System.Text;

namespace EntityManager.Entity_Handling
{
    public class ACEntityManager : EntityManager
    {
        private Swed swed;
        private IntPtr mainModule;

        public ACEntityManager(Swed swedInstance, IntPtr mainModule)
        {
            this.swed = swedInstance;
            this.mainModule = mainModule;
        }

        public override void UpdateEntities()
        {
            entities.Clear();

            for (var i = 0; i < 4; i++)
            {
                IntPtr entityAddress = swed.ReadPointer(mainModule, Offsets.entityList, i * 0x4);

                if (entityAddress == IntPtr.Zero)
                    continue;

                var entity = new Entity();
                entity.BaseAddress = entityAddress;

                UpdateEntity(entity);
                entities.Add(entity);
            }
        }

        public override void UpdateEntity(Entity entity)
        {
            entity.Health = swed.ReadInt(entity.BaseAddress, Offsets.health);
            entity.Name = ASCIIEncoding.UTF8.GetString(swed.ReadBytes(entity.BaseAddress, 0x205, 16));
        }

        public override void UpdateLocalPlayer()
        {
            localPlayer.BaseAddress = swed.ReadPointer(mainModule, Offsets.localPlayer);
            UpdateEntity(localPlayer);
        }
    }
}
