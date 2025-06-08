using FleetManager.Application.Services;
using FleetManager.Domain.Interfaces;
using FleetManager.Infrastructure.EntityFramework.Data;
using FleetManager.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=fleet.db")
);

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleAppService, VehicleAppService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerPathFeature>();
        var ex = feature?.Error;
        var response = new { message = ex?.Message };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex is ArgumentException ? 400 :
                                     ex is InvalidOperationException ? 409 :
                                     ex is KeyNotFoundException ? 404 :
                                     500;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    });
});

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();