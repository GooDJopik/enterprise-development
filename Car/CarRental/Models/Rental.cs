namespace CarRental.Models;

/// <summary>
/// Represents a rental transaction where a client rents a car.
/// </summary>
public class Rental
{
    /// <summary>
    /// Unique identifier for the rental.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The client who rents the car.
    /// </summary>
    public Client Client { get; set; } = new();

    /// <summary>
    /// The car being rented.
    /// </summary>
    public Car Car { get; set; } = new();

    /// <summary>
    /// Start date and time of the rental.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    public int DurationHours { get; set; }
}