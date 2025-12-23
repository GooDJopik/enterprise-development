namespace CarRental.Application.Contracts.Dtos.Rentals;

/// <summary>
/// Rental DTO.
/// </summary>
/// <param name="Id">Rental identifier.</param>
/// <param name="ClientId">Client identifier.</param>
/// <param name="CarId">Car identifier.</param>
/// <param name="StartTime">Rental start time.</param>
/// <param name="DurationHours">Duration in hours.</param>
public record RentalDto(int Id, int ClientId, int CarId, DateTime StartTime, int DurationHours);