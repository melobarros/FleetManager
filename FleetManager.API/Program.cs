using FleetManager.Application.Services;
using FleetManager.Domain.Interfaces;
using FleetManager.Infrastructure.EntityFramework.Data;
using FleetManager.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseSqlite("Data Source=:memory;Mode=Memory;Cache=Shared");
    }
    else
    {
        var dataDir = Environment.GetEnvironmentVariable("DATA_DIR");
        var path = string.IsNullOrEmpty(dataDir)
            ? "fleet.db"
            : $"{dataDir}/fleet.db";
        options.UseSqlite($"Data Source={path}");
    }
        
});

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleAppService, VehicleAppService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.OpenConnection();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();