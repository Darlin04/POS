using Inventory.Domain.MeasurementUnits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Persistence.EntityConfigurations;

public class MeasurementUnitEntityConfiguration : IEntityTypeConfiguration<MeasurementUnit>
{
    public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
    {
        builder.ToTable("measurement_units", "inventory");
        
        builder.HasKey(mu => mu.Id);
        builder.Property(mu => mu.Id).ValueGeneratedNever().HasColumnOrder(0);

        builder.OwnsOne(mu => mu.MeasurementUnitName, muName =>
        {
            muName.Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(20)
                .IsRequired();

            muName.HasIndex(n => n.Value)
                .IsUnique();
        });

        builder.OwnsOne(mu => mu.MeasurementUnitAbbreviation, muAbbreviation =>
        {
            muAbbreviation.Property(a => a.Value)
                .HasColumnName("abbreviation")
                .HasMaxLength(3)
                .IsRequired();

            muAbbreviation.HasIndex(a => a.Value)
                .IsUnique();
        });
    }
}