using EntityManager.Entity_Structures;

namespace EntityManager.Entity_Handling
{
    public abstract class EntityManager
    {
        protected Entity localPlayer = new Entity();
        protected List<Entity> entities = new List<Entity>();

        public abstract void UpdateEntity(Entity entity);
        public abstract void UpdateLocalPlayer();
        public abstract void UpdateEntities();

        public List<Entity> GetEntities()
        {
            return entities;
        }

        public Entity GetLocalPlayer()
        {
            return localPlayer;
        }


    }
}
