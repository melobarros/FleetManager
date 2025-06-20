using FleetManager.Application.Services;
using FleetManager.Domain.Interfaces;
using FleetManager.Infrastructure.EntityFramework.Data;
using FleetManager.Infrastructure.EntityFramework.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManager.Tests.Fixtures
{
    public class TestFixture
    {
        public ServiceProvider Provider { get; }

        private readonly SqliteConnection _connection;

        public TestFixture()
        {
            var services = new ServiceCollection();

            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlite(_connection));

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IProtocolRepository, ProtocolRepository>();
            services.AddScoped<IVehicleAppService, VehicleAppService>();
            services.AddScoped<IVehicleFactoryAppService, VehicleFactoryAppService>();
            services.AddScoped<IDiagnosticAppService, DiagnosticAppService>();

            Provider = services.BuildServiceProvider();
            using var scope = Provider.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            ctx.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Provider.Dispose();
            _connection.Close();
        }
    }
}
