using CarRental.Application.Contracts.Dtos.Rentals;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Infrastructure.Nats.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Net;
using System.Text.Json;

namespace CarRental.Infrastructure.Nats.Services;

/// <summary>
/// Background service that consumes rental messages from NATS JetStream.
/// </summary>
public class RentalConsumerService(
    INatsConnection natsConnection,
    IServiceScopeFactory scopeFactory,
    IOptions<RentalConsumerOptions> options,
    ILogger<RentalConsumerService> logger) : BackgroundService
{
    private readonly RentalConsumerOptions _options = options.Value;

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Rental consumer service started. Stream: {StreamName}, Subject: {Subject}", _options.StreamName, _options.Subject);

        var jetStream = natsConnection.CreateJetStreamContext();

        await EnsureStreamAndConsumerAsync(jetStream, stoppingToken);

        var consumer = await jetStream.GetConsumerAsync(_options.StreamName, _options.ConsumerName, stoppingToken);

        await foreach (var msg in consumer.ConsumeAsync<byte[]>(cancellationToken: stoppingToken))
        {
            await ProcessMessageAsync(msg, stoppingToken);
        }

        logger.LogInformation("Rental consumer service stopped");
    }

    private async Task EnsureStreamAndConsumerAsync(INatsJSContext jetStream, CancellationToken cancellationToken)
    {
        var streamConfig = new StreamConfig(_options.StreamName, [_options.Subject])
        {
            Storage = StreamConfigStorage.File,
            Retention = StreamConfigRetention.Limits,
            MaxAge = TimeSpan.FromHours(12)
        };

        try
        {
            await jetStream.CreateStreamAsync(streamConfig, cancellationToken);
            logger.LogInformation("Created JetStream stream '{StreamName}'", _options.StreamName);
        }
        catch (NatsJSApiException ex) when (ex.Error.Code == 400)
        {
            logger.LogDebug("JetStream stream '{StreamName}' already exists", _options.StreamName);
        }

        var consumerConfig = new ConsumerConfig(_options.ConsumerName)
        {
            DurableName = _options.ConsumerName,
            AckPolicy = ConsumerConfigAckPolicy.Explicit,
            FilterSubject = _options.Subject
        };

        try
        {
            await jetStream.CreateConsumerAsync(_options.StreamName, consumerConfig, cancellationToken);
            logger.LogInformation("Created JetStream consumer '{ConsumerName}'", _options.ConsumerName);
        }
        catch (NatsJSApiException ex) when (ex.Error.Code == 400)
        {
            logger.LogDebug("JetStream consumer '{ConsumerName}' already exists", _options.ConsumerName);
        }
    }

    private async Task ProcessMessageAsync(NatsJSMsg<byte[]> msg, CancellationToken cancellationToken)
    {
        try
        {
            var rental = JsonSerializer.Deserialize<RentalCreateUpdateDto>(msg.Data);

            if (rental is null)
            {
                logger.LogWarning("Failed to deserialize rental message, skipping");
                await msg.AckAsync(cancellationToken: cancellationToken);
                return;
            }

            logger.LogInformation(
                "Processing rental: ClientId={ClientId}, CarId={CarId}, StartTime={StartTime}, DurationHours={DurationHours}",
                rental.ClientId,
                rental.CarId,
                rental.StartTime,
                rental.DurationHours);

            await using var scope = scopeFactory.CreateAsyncScope();
            var rentalService = scope.ServiceProvider.GetRequiredService<IApplicationService<RentalDto, RentalCreateUpdateDto>>();

            var result = await rentalService.Create(rental);

            logger.LogInformation("Successfully created rental with Id={RentalId}", result.Id);

            await msg.AckAsync(cancellationToken: cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Failed to create rental due to missing related entity. Message will be acknowledged and skipped.");
            await msg.AckAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error processing rental message");
            await msg.NakAsync(cancellationToken: cancellationToken);
        }
    }
}