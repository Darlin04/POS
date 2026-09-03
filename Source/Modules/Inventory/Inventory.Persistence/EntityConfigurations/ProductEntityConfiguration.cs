using Inventory.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Persistence.EntityConfigurations;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products", "inventory");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasColumnName("id")
            .HasColumnOrder(0);

        builder.OwnsOne(p => p.BarCode, barCode =>
        {
            barCode.Property(b => b.Value)
                .HasColumnName("bar_code")
                .HasMaxLength(14)
                .HasColumnOrder(1)
                .IsRequired();

            barCode.HasIndex(bc => bc.Value)
                .IsUnique();
        });

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnOrder(2)
            .IsRequired();

        builder.ComplexProperty(p => p.Cost, cost =>
        {
            cost.Property(c => c.Amount)
                .HasColumnName("cost_amount")
                .HasPrecision(18, 2)
                .HasColumnOrder(3)
                .IsRequired();

            cost.Property(c => c.Currency)
                .HasColumnName("currency")
                .HasConversion<string>()
                .HasMaxLength(3)
                .HasColumnOrder(4)
                .IsRequired();
        });

        builder.ComplexProperty(p => p.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasColumnName("price_amount")
                .HasPrecision(18, 2)
                .HasColumnOrder(5)
                .IsRequired();

            price.Ignore(p => p.Currency);
        });

        builder.ComplexProperty(p => p.WholesaleQuantity, wholesaleQuantity =>
        {
            wholesaleQuantity.Property(wq => wq.Value)
                .HasColumnName("wholesale_quantity")
                .HasPrecision(18, 2)
                .HasColumnOrder(6)
                .IsRequired();
        });

        builder.ComplexProperty(p => p.WholesalePrice, wholesalePrice =>
        {
            wholesalePrice.Property(wp => wp.Amount)
                .HasColumnName("wholesale_price_amount")
                .HasPrecision(18, 2)
                .HasColumnOrder(7)
                .IsRequired();

            wholesalePrice.Ignore(wp => wp.Currency);
        });

        builder.ComplexProperty(p => p.Stock, stock =>
        {
            stock.Property(s => s.Value)
                .HasColumnName("stock")
                .HasPrecision(18, 2)
                .HasColumnOrder(8)
                .IsRequired();
        });

        builder.Property(p => p.MinStock)
            .HasColumnName("min_stock")
            .HasPrecision(18, 2)
            .HasColumnOrder(9)
            .IsRequired();
    }
}