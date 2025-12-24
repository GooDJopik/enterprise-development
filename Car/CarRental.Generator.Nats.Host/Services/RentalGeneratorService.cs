using CarRental.Generator.Nats.Host.Configuration;
using Microsoft.Extensions.Options;

namespace CarRental.Generator.Nats.Host.Services;

/// <summary>
/// Background service that orchestrates rental generation and publishing.
/// </summary>
public class RentalGeneratorService(
    RentalGenerator generator,
    RentalProducer producer,
    IOptions<RentalGeneratorOptions> options,
    ILogger<RentalGeneratorService> logger) : BackgroundService
{
    private readonly RentalGeneratorOptions _options = options.Value;

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Rental generator service started. Interval: {IntervalMs}ms, Subject: {Subject}", _options.IntervalMs, _options.Subject);

        await producer.EnsureStreamCreatedAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var rental = generator.Generate();

                logger.LogInformation(
                    "Generated rental: ClientId={ClientId}, CarId={CarId}, StartTime={StartTime}, DurationHours={DurationHours}",
                    rental.ClientId,
                    rental.CarId,
                    rental.StartTime,
                    rental.DurationHours);

                await producer.PublishAsync(rental, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Cancellation request, stopping generator service");
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error generating or publishing rental");
            }

            await Task.Delay(_options.IntervalMs, stoppingToken);
        }

        logger.LogInformation("Rental generator service stopped");
    }
}