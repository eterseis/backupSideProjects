namespace EntityManager.Entity_Structures
{
    public class Entity
    {
        public IntPtr BaseAddress { get; set; }
        public int Health { get; set; }
        public string Name { get; set; } = String.Empty;
    }
}
