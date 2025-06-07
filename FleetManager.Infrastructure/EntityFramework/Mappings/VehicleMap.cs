using FleetManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Infrastructure.EntityFramework.Mappings
{
    public class VehicleMap :IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasDiscriminator<string>("VehicleType")
                .HasValue<Car>(nameof(Car))
                .HasValue<Truck>(nameof(Truck))
                .HasValue<Bus>(nameof(Bus));

            builder.OwnsOne(b => b.ChassisId, chassis =>
                {
                    chassis.Property(c => c.Series)
                        .HasColumnName("ChassisSeries")
                        .IsRequired();

                    chassis.Property(c => c.Number)
                        .HasColumnName("ChassisNumber")
                        .IsRequired();
                });

            builder.Property<string>("ChassisSeries");
            builder.Property<uint>("ChassisNumber");
            builder.HasKey("ChassisSeries", "ChassisNumber");

            builder.Property(b => b.Color)
                .IsRequired();
        }
    }
}
