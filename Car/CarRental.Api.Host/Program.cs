using CarRental.Application.Contracts.Dtos.Cars;
using CarRental.Application.Contracts.Dtos.Clients;
using CarRental.Application.Contracts.Dtos.ModelGenerations;
using CarRental.Application.Contracts.Dtos.Models;
using CarRental.Application.Contracts.Dtos.Rentals;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Application.Profiles;
using CarRental.Application.Services;
using CarRental.Domain.Models;
using CarRental.Domain.Repositories;
using CarRental.Infrastructure.EfCore;
using CarRental.Infrastructure.EfCore.Repositories;
using CarRental.Infrastructure.Nats.Configuration;
using CarRental.Infrastructure.Nats.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("CarRental"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }
});

builder.AddNpgsqlDbContext<CarRentalDbContext>("CarRentalDatabase");

builder.AddNatsClient("nats");

builder.Services.AddAutoMapper(configAction =>
{
    configAction.AddProfile<CarRentalApplicationProfile>();
});

builder.Services.AddScoped<IRepository<Car>, CarRepository>();
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
builder.Services.AddScoped<IRepository<Model>, ModelRepository>();
builder.Services.AddScoped<IRepository<ModelGeneration>, ModelGenerationRepository>();
builder.Services.AddScoped<IRepository<Rental>, RentalRepository>();

builder.Services.AddScoped<IApplicationService<CarDto, CarCreateUpdateDto>, CarService>();
builder.Services.AddScoped<IApplicationService<ClientDto, ClientCreateUpdateDto>, ClientService>();
builder.Services.AddScoped<IApplicationService<ModelDto, ModelCreateUpdateDto>, ModelService>();
builder.Services.AddScoped<IApplicationService<ModelGenerationDto, ModelGenerationCreateUpdateDto>, ModelGenerationService>();
builder.Services.AddScoped<IApplicationService<RentalDto, RentalCreateUpdateDto>, RentalService>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.Configure<RentalConsumerOptions>(builder.Configuration.GetSection(RentalConsumerOptions.SectionName));

builder.Services.AddHostedService<RentalConsumerService>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>();

    await context.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();