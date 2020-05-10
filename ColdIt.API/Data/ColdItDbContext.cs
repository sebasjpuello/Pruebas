using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColdIt.API.Data
{
    //Heredamos de DbContext nuestro contexto
    public class ColdItDbContext : DbContext
    {
        //Constructor sin parametros
        public ColdItDbContext()
        {
        }

        //Constructor con parametros para la configuracion
        public ColdItDbContext(DbContextOptions<ColdItDbContext> options)
        : base(options)
        {
        }

        //Sobreescribimos el metodo OnConfiguring para hacer los ajustes que queramos en caso de
        //llamar al constructor sin parametros
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //En caso de que el contexto no este configurado, lo configuramos mediante la cadena de conexion
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=ColdItDb;Trusted_Connection=True;");
            }
        }

        //Tablas de datos
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Producto>().HasData(
                new Producto()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Codigo = "001",
                    Valor = 30000,
                    Nombre = "Memoria"
                },
                new Producto()
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    Codigo = "002",
                    Valor = 1500000,
                    Nombre = "Portatil"
                },
                new Producto()
                {
                    Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                    Codigo = "003",
                    Valor = 700000,
                    Nombre = "Celular"
                },
                new Producto()
                {
                    Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                    Codigo = "004",
                    Valor = 20000,
                    Nombre = "Teclado"
                },
                new Producto()
                {
                    Id = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
                    Codigo = "005",
                    Valor = 10000,
                    Nombre = "Mouse"
                }
                );

            modelBuilder.Entity<Factura>().HasData(
               new Factura
               {
                   Id = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
                   Cliente = "Mr Splinter",                   
                   ValorTotal = 4560000
               }
               );

            modelBuilder.Entity<Item>().HasData(
               new Item
               {
                   Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                   ProductoId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                   Cartidad = 2,
                   ValorTotal = 60000,
                   FacturaId = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
               },
               new Item
               {
                   Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                   ProductoId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                   Cartidad = 3,
                   ValorTotal = 4500000,
                   FacturaId = Guid.Parse("40ff5488-fdab-45b5-bc3a-14302d59869a"),
               }
               );

            base.OnModelCreating(modelBuilder);
        }
    }
}
