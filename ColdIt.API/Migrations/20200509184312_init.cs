using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ColdIt.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Cliente = table.Column<string>(nullable: true),
                    ValorTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Codigo = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductoId = table.Column<Guid>(nullable: false),
                    Cartidad = table.Column<int>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: false),
                    FacturaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Facturas",
                columns: new[] { "Id", "Cliente", "ValorTotal" },
                values: new object[] { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), "Mr Splinter", 4560000.0 });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Codigo", "Nombre", "Valor" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "001", "Memoria", 30000.0 },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "002", "Portatil", 1500000.0 },
                    { new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "003", "Celuar", 700000.0 },
                    { new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"), "004", "Teclado", 20000.0 },
                    { new Guid("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"), "005", "Mouse", 10000.0 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Cartidad", "FacturaId", "ProductoId", "ValorTotal" },
                values: new object[] { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), 2, new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), 60000.0 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Cartidad", "FacturaId", "ProductoId", "ValorTotal" },
                values: new object[] { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), 3, new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), 4500000.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Items_FacturaId",
                table: "Items",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ProductoId",
                table: "Items",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
