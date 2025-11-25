namespace CarRental.Domain.Models;

/// <summary>
/// Represents a car available for rental.
/// </summary>
public class Car
{
    /// <summary>
    /// Unique identifier for the car.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// License plate number of the car.
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Color of the car.
    /// </summary>
    public required string Color { get; set; }

    /// <summary>
    /// The generation of the car's model.
    /// </summary>
    public required ModelGeneration Generation { get; set; }
}
