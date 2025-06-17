using FleetManager.Domain.Entities.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Infrastructure.EntityFramework.Mappings;

public class ErrorCodeMap : IEntityTypeConfiguration<ErrorCode>
{
    public void Configure(EntityTypeBuilder<ErrorCode> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(e => e.Description)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(e => e.ProtocolId)
               .IsRequired();
    }
}