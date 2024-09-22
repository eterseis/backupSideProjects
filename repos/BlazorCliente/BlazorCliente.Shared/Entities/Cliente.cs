using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCliente.Shared.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public int Idade { get; set; }
    }
}
