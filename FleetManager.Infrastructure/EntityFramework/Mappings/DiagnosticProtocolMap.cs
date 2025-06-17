using System;
using FleetManager.Domain.Entities.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Infrastructure.EntityFramework.Mappings;

public class DiagnosticProtocolMap : IEntityTypeConfiguration<DiagnosticProtocol>
{
    public void Configure(EntityTypeBuilder<DiagnosticProtocol> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.VehicleType)
            .IsRequired();

        builder.HasMany(p => p.Sensors)
               .WithOne(s => s.Protocol)
               .HasForeignKey(s => s.ProtocolId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ErrorCodes)
               .WithOne(e => e.Protocol)
               .HasForeignKey(e => e.ProtocolId)
               .OnDelete(DeleteBehavior.Cascade);
    }

}
