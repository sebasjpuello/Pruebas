using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ColdIt.API.Data
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }
        public int Cartidad { get; set; }
        public double ValorTotal { get; set; }
        public Guid FacturaId { get; set; }
        [ForeignKey("FacturaId")]
        public Factura Factura { get; set; }
    }
}
