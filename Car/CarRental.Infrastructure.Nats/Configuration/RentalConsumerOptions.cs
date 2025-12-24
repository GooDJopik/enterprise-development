namespace CarRental.Infrastructure.Nats.Configuration;

/// <summary>
/// Configuration options for the rental consumer.
/// </summary>
public class RentalConsumerOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "RentalConsumer";

    /// <summary>
    /// NATS stream name.
    /// </summary>
    public string StreamName { get; set; } = "rentals";

    /// <summary>
    /// NATS subject for rental creation.
    /// </summary>
    public string Subject { get; set; } = "rentals.create";

    /// <summary>
    /// Consumer name.
    /// </summary>
    public string ConsumerName { get; set; } = "rental-consumer";
}
