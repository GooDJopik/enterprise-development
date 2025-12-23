namespace CarRental.Application.Contracts.Dtos.Rentals;

/// <summary>
/// Rental create/update DTO.
/// </summary>
/// <param name="ClientId">Client identifier.</param>
/// <param name="CarId">Car identifier.</param>
/// <param name="StartTime">Rental start time.</param>
/// <param name="DurationHours">Duration in hours.</param>
public record RentalCreateUpdateDto(int ClientId, int CarId, DateTime StartTime, int DurationHours);