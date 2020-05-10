using ColdIt.API.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColdIt.API.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoController : ControllerBase
    {        

        private readonly ColdItDbContext _context;

        public ProductoController(ColdItDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet()]        
        public ActionResult<IEnumerable<Producto>> GetProductos()
        {
            var productos = _context.Productos.ToList<Producto>();
            return Ok(productos);
        }

        [HttpGet("{productoId}", Name = "GetProducto")]
        public IActionResult GetProducto(Guid productoId)
        {
            if (productoId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productoId));
            }

            var producto = _context.Productos.FirstOrDefault(a => a.Id == productoId);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        [HttpPost]
        public ActionResult<Producto> CreateProducto([FromForm] Producto producto)
        {
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto));
            }

            // the repository fills the id (instead of using identity columns)
            producto.Id = Guid.NewGuid();

            _context.Productos.Add(producto);

            _context.SaveChanges();

            return Ok(producto);
        }

        [HttpPut("{productoId}")]
        public IActionResult UpdateProducto(
            Guid productoId,
            [FromForm] Producto producto)
        {
            if (productoId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productoId));
            }

            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto));
            }

            var productoEditar = _context.Productos.FirstOrDefault(a => a.Id == productoId);

            if (productoEditar == null)
            {
                return NotFound();
            }

            productoEditar.Codigo = producto.Codigo;
            productoEditar.Nombre = producto.Nombre;
            productoEditar.Valor = producto.Valor;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{productoId}")]
        public ActionResult DeleteProducto(Guid productoId)
        {
            if (productoId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productoId));
            }

            var producto = _context.Productos.FirstOrDefault(a => a.Id == productoId);

            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("calcular")]
        public ActionResult<Factura> Calcular(Factura factura)
        {
            if (factura == null)
            {
                throw new ArgumentNullException(nameof(factura));
            }            

            // the repository fills the id (instead of using identity columns)
            factura.Id = Guid.NewGuid();

            Factura facturaNew = new Factura() { Id = factura.Id, Cliente = factura.Cliente, ValorTotal = 0 };
            List<Item> itemsNew = new List<Item>();

            foreach (var item in factura.Items) {
                var producto = _context.Productos.FirstOrDefault(a => a.Codigo == item.Producto.Codigo);
                item.Id = Guid.NewGuid();
                item.ProductoId = producto.Id;
                item.Producto.Id = producto.Id;
                item.ValorTotal = item.Cartidad * item.Producto.Valor;
                item.FacturaId = factura.Id;

                itemsNew.Add(new Item() { Id = item.Id, ProductoId = item.ProductoId, Cartidad = item.Cartidad, ValorTotal = item.ValorTotal, FacturaId = item.FacturaId });
            }

            factura.ValorTotal = factura.Items.Sum(f => f.ValorTotal);


            facturaNew.ValorTotal = factura.ValorTotal;
            _context.Facturas.Add(facturaNew);
            foreach (var item in itemsNew)
            {
                _context.Items.Add(item);
            }

            _context.SaveChanges();

            return Ok(factura);
        }

    }
}
