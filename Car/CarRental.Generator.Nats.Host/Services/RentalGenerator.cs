using Bogus;
using CarRental.Application.Contracts.Dtos.Rentals;
using CarRental.Generator.Nats.Host.Configuration;
using Microsoft.Extensions.Options;

namespace CarRental.Generator.Nats.Host.Services;

/// <summary>
/// Service for generating fake rental data using Bogus.
/// </summary>
public class RentalGenerator(IOptions<RentalGeneratorOptions> options)
{
    private readonly RentalGeneratorOptions _options = options.Value;
    private readonly Faker _faker = new();

    /// <summary>
    /// Generates a fake rental create/update DTO.
    /// </summary>
    /// <returns>A randomly generated <see cref="RentalCreateUpdateDto"/>.</returns>
    public RentalCreateUpdateDto Generate() =>
        new(ClientId: _faker.Random.Int(_options.MinId, _options.MaxId),
            CarId: _faker.Random.Int(_options.MinId, _options.MaxId),
            StartTime: _faker.Date.Between(
                DateTime.UtcNow.AddDays(-_options.StartTimeDaysRange),
                DateTime.UtcNow.AddDays(_options.StartTimeDaysRange)),
            DurationHours: _faker.Random.Int(_options.MinDurationHours, _options.MaxDurationHours));
}