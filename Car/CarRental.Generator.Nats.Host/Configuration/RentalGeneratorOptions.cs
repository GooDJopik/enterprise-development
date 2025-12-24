namespace CarRental.Generator.Nats.Host.Configuration;

/// <summary>
/// Configuration options for the rental generator.
/// </summary>
public class RentalGeneratorOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "RentalGenerator";

    /// <summary>
    /// Generation interval in milliseconds.
    /// </summary>
    public int IntervalMs { get; set; } = 3000;

    /// <summary>
    /// NATS stream name.
    /// </summary>
    public string StreamName { get; set; } = "rentals";

    /// <summary>
    /// NATS subject for rental creation.
    /// </summary>
    public string Subject { get; set; } = "rentals.create";

    /// <summary>
    /// Minimum client/car ID.
    /// </summary>
    public int MinId { get; set; } = 1;

    /// <summary>
    /// Maximum client/car ID.
    /// </summary>
    public int MaxId { get; set; } = 30;

    /// <summary>
    /// Minimum rental duration in hours.
    /// </summary>
    public int MinDurationHours { get; set; } = 1;

    /// <summary>
    /// Maximum rental duration in hours.
    /// </summary>
    public int MaxDurationHours { get; set; } = 744;

    /// <summary>
    /// Number of days range for start time generation.
    /// </summary>
    public int StartTimeDaysRange { get; set; } = 30;
}