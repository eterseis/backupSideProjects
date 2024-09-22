using EntityManager.Entity_Structures;
using Swed32;

namespace EntityManager.Entity_Handling
{
    internal class RBLXEntityManager : EntityManager
    {
        private Swed swed;
        private IntPtr mainModule;

        public RBLXEntityManager(Swed swed, IntPtr mainModule)
        {
            this.swed = swed;
            this.mainModule = mainModule;
        }

        public override void UpdateEntities()
        {
            throw new NotImplementedException();
        }

        public override void UpdateEntity(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override void UpdateLocalPlayer()
        {

        }
    }
}
