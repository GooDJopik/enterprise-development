namespace CarRental.Application.Contracts.Dtos.Cars;

/// <summary>
/// Car DTO.
/// </summary>
/// <param name="Id">Car identifier.</param>
/// <param name="LicensePlate">License plate.</param>
/// <param name="Color">Color.</param>
/// <param name="GenerationId">Model generation identifier.</param>
public record CarDto(int Id, string LicensePlate, string Color, int GenerationId);