using Microsoft.EntityFrameworkCore;
using BlazorCliente.Shared.Entities;

namespace BlazorCliente.Context
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; } = null!;
    }
}
