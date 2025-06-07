using FleetManager.Domain.Entities;
using FleetManager.Infrastructure.EntityFramework.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.EntityFramework.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}