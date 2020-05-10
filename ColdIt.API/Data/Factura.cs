using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ColdIt.API.Data
{
    public class Factura
    {
        [Key]
        public Guid Id { get; set; }
        public string Cliente { get; set; }
        public ICollection<Item> Items { get; set; }
             = new List<Item>();
        public double ValorTotal { get; set; }
    }
}
