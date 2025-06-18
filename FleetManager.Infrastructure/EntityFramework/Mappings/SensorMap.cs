using FleetManager.Domain.Entities.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Infrastructure.EntityFramework.Mappings;

public class SensorMap : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(s => s.Unit)
               .HasMaxLength(20);

        builder.Property(s => s.ProtocolId)
               .IsRequired();

        builder.Property(s => s.MinThreshold)
               .IsRequired()
               .HasColumnType("INTEGER");

        builder.Property(s => s.MaxThreshold)
               .IsRequired()
               .HasColumnType("INTEGER");
    }
}