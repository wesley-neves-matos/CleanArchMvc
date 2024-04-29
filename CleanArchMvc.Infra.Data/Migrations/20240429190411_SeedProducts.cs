using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchMvc.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Caderno espiral 100 folhas", "caderno1.jpg", "Caderno espiral", 7.45m, 0 },
                    { 2, 1, "Estojo escolar cinza", "estojo1.jpg", "Estojo escolar", 5.65m, 0 },
                    { 3, 1, "Borracha branca pequena", "borracha1.jpg", "Borracha escolar", 3.25m, 0 },
                    { 4, 2, "Calculadora simples", "calculadora1.jpg", "Calculadora escolar", 15.39m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
