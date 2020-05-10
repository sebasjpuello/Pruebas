using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ColdIt.API.Data
{
    public class Producto
    {
        [Key]
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public double Valor { get; set; }
        public string Nombre { get; set; }
    }
}
