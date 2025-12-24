using CarRental.Generator.Nats.Host.Configuration;
using CarRental.Generator.Nats.Host.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<RentalGeneratorOptions>(
    builder.Configuration.GetSection(RentalGeneratorOptions.SectionName));

builder.AddNatsClient("nats");

builder.Services.AddSingleton<RentalGenerator>();
builder.Services.AddSingleton<RentalProducer>();
builder.Services.AddHostedService<RentalGeneratorService>();

var host = builder.Build();
host.Run();