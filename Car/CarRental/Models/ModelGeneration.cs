namespace CarRental.Models;

/// <summary>
/// Represents a specific generation of a car model.
/// </summary>
public class ModelGeneration
{
    /// <summary>
    /// Unique identifier for the model generation.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The model to which this generation belongs.
    /// </summary>
    public Model Model { get; set; } = new();

    /// <summary>
    /// Year of release for this generation.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Engine volume in liters.
    /// </summary>
    public double EngineVolume { get; set; }

    /// <summary>
    /// Transmission type (e.g., manual, automatic).
    /// </summary>
    public string TransmissionType { get; set; } = "";

    /// <summary>
    /// Rental price per hour in local currency.
    /// </summary>
    public decimal PricePerHour { get; set; }
}
