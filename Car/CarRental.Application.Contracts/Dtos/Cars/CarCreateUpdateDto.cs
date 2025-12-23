namespace CarRental.Application.Contracts.Dtos.Cars;

/// <summary>
/// Car create/update DTO.
/// </summary>
/// <param name="LicensePlate">License plate.</param>
/// <param name="Color">Color.</param>
/// <param name="GenerationId">Model generation identifier.</param>
public record CarCreateUpdateDto(string LicensePlate, string Color, int GenerationId);