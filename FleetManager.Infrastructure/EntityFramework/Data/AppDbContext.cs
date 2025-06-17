using FleetManager.Domain.Entities;
using FleetManager.Domain.Entities.Diagnostics;
using FleetManager.Infrastructure.EntityFramework.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.EntityFramework.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<DiagnosticProtocol> DiagnosticProtocols { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<ErrorCode> ErrorCodes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleMap());
            modelBuilder.ApplyConfiguration(new DiagnosticProtocolMap());
            modelBuilder.ApplyConfiguration(new SensorMap());
            modelBuilder.ApplyConfiguration(new ErrorCodeMap());
            DiagnosticSeed.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}