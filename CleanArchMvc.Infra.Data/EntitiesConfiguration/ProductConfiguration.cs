using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name).HasMaxLength(100).IsRequired();
            builder.Property(i => i.Description).HasMaxLength(200).IsRequired();

            builder.Property(i => i.Price).HasPrecision(10, 2);

            builder.HasOne(i => i.Category).WithMany(i => i.Products).HasForeignKey(i => i.CategoryId);

            SeedProducts(builder);
        }

        private static void SeedProducts(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (
                new Product(1, "Caderno espiral", "Caderno espiral 100 folhas", 7.45M, 50, "caderno1.jpg") { CategoryId = 1 },
                new Product(2, "Estojo escolar", "Estojo escolar cinza", 5.65M, 70, "estojo1.jpg") { CategoryId = 1 },
                new Product(3, "Borracha escolar", "Borracha branca pequena", 3.25M, 80, "borracha1.jpg") { CategoryId = 1 },
                new Product(4, "Calculadora escolar", "Calculadora simples", 15.39M, 20, "calculadora1.jpg") { CategoryId = 2 }
            );
        }
    }
}
