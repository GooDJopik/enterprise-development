using System.Text.Json;
using CarRental.Application.Contracts.Dtos.Rentals;
using CarRental.Generator.Nats.Host.Configuration;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Net;

namespace CarRental.Generator.Nats.Host.Services;

/// <summary>
/// Service for publishing rental data to NATS JetStream.
/// </summary>
public class RentalProducer(
    INatsConnection natsConnection,
    IOptions<RentalGeneratorOptions> options,
    ILogger<RentalProducer> logger)
{
    private readonly RentalGeneratorOptions _options = options.Value;
    private INatsJSContext? _jetStream;
    private bool _streamInitialized;

    /// <summary>
    /// Ensures the JetStream stream is created.
    /// </summary>
    public async Task EnsureStreamCreatedAsync(CancellationToken cancellationToken = default)
    {
        if (_streamInitialized)
            return;

        _jetStream = natsConnection.CreateJetStreamContext();

        var streamConfig = new StreamConfig(_options.StreamName, [_options.Subject])
        {
            Storage = StreamConfigStorage.File,
            Retention = StreamConfigRetention.Limits,
            MaxAge = TimeSpan.FromHours(12)
        };

        try
        {
            await _jetStream.CreateStreamAsync(streamConfig, cancellationToken);
            logger.LogInformation("Created JetStream stream '{StreamName}'", _options.StreamName);
        }
        catch (NatsJSApiException ex) when (ex.Error.Code == 400)
        {
            logger.LogInformation("JetStream stream '{StreamName}' already exists", _options.StreamName);
        }

        _streamInitialized = true;
    }

    /// <summary>
    /// Publishes a rental DTO to JetStream.
    /// </summary>
    /// <param name="rental">The rental DTO to publish.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task PublishAsync(RentalCreateUpdateDto rental, CancellationToken cancellationToken = default)
    {
        if (_jetStream is null)
            throw new InvalidOperationException("Stream not initialized. Call EnsureStreamCreatedAsync first.");

        var data = JsonSerializer.SerializeToUtf8Bytes(rental);

        var ack = await _jetStream.PublishAsync(_options.Subject, data, cancellationToken: cancellationToken);
        ack.EnsureSuccess();

        logger.LogDebug("Published rental to JetStream: ClientId={ClientId}, CarId={CarId}, Sequence={Sequence}", rental.ClientId, rental.CarId, ack.Seq);
    }
}