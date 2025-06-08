using FleetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Infrastructure.EntityFramework.Mappings
{
    public class VehicleMap : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasDiscriminator<string>("VehicleType")
                   .HasValue<Car>("Car")
                   .HasValue<Truck>("Truck")
                   .HasValue<Bus>("Bus");

            builder.HasKey(v => new { v.ChassisSeries, v.ChassisNumber });

            builder.Property(v => v.ChassisSeries)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(v => v.ChassisNumber)
                   .IsRequired();

            builder.Property(v => v.Color)
                   .IsRequired();
        }
    }
}
