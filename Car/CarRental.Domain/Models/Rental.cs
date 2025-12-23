namespace CarRental.Domain.Models;

/// <summary>
/// Represents a rental transaction where a client rents a car.
/// </summary>
public class Rental
{
    /// <summary>
    /// Unique identifier for the rental.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// The client who rents the car.
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Navigation property for ClientId.
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// The car being rented.
    /// </summary>
    public int CarId { get; set; }

    /// <summary>
    /// Navigation property for CarId.
    /// </summary>
    public required Car Car { get; set; }

    /// <summary>
    /// Start date and time of the rental.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    public int DurationHours { get; set; }
}