namespace CarRental.Application.Contracts.Dtos.Analytics;

/// <summary>
/// DTO that represents the number of rentals for a car.
/// </summary>
/// <param name="LicensePlate">Car license plate.</param>
/// <param name="RentalsCount">Number of rentals.</param>
public record CarRentalCountDto(string LicensePlate, int RentalsCount);