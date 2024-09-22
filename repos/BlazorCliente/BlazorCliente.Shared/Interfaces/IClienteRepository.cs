using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorCliente.Shared.Entities;

namespace BlazorCliente.Shared.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> AddClienteAsync(Cliente model);
        Task<Cliente> UpdateClienteAsync(Cliente model);
        Task<Cliente> DeleteClienteAsync(int clientId);
        Task<List<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int clienteId);    
    }
}
