using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaltaDataAccess.Models
{
    internal struct Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
        public string Summary { get; set; } = String.Empty;
        public int Order { get; set; }
        public string Description { get; set; } = String.Empty;
        public bool Featured { get; set; }

        public Category() { }
    }
}
