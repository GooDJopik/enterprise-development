namespace CarRental.Domain.Models;

/// <summary>
/// Represents a car model.
/// </summary>
public class Model
{
    /// <summary>
    /// Unique identifier for the model.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the car model.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Type of drive (e.g., front, rear, all-wheel).
    /// </summary>
    public string DriveType { get; set; } = "";

    /// <summary>
    /// Number of seats in the car.
    /// </summary>
    public int Seats { get; set; }

    /// <summary>
    /// Body type of the car (e.g., sedan, SUV).
    /// </summary>
    public string BodyType { get; set; } = "";

    /// <summary>
    /// Class of the car (e.g., economy, business, premium).
    /// </summary>
    public string CarClass { get; set; } = "";
}