namespace CarRental.Application.Contracts.Dtos.ModelGenerations;

/// <summary>
/// Model generation DTO.
/// </summary>
/// <param name="Id">Model generation identifier.</param>
/// <param name="ModelName">Model name.</param>
/// <param name="Year">Year.</param>
/// <param name="EngineVolume">Engine volume.</param>
/// <param name="TransmissionType">Transmission type.</param>
/// <param name="PricePerHour">Price per hour.</param>
public record ModelGenerationDto(
    int Id,
    string ModelName,
    int Year,
    double EngineVolume,
    string TransmissionType,
    decimal PricePerHour);