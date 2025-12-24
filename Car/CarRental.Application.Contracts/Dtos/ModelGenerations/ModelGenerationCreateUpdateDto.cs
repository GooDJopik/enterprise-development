using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Contracts.Dtos.ModelGenerations;

/// <summary>
/// Model generation create/update DTO.
/// </summary>
/// <param name="ModelId">Model identifier.</param>
/// <param name="Year">Year.</param>
/// <param name="EngineVolume">Engine volume.</param>
/// <param name="TransmissionType">Transmission type.</param>
/// <param name="PricePerHour">Price per hour.</param>
public record ModelGenerationCreateUpdateDto(
    [Required(ErrorMessage = "Model ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Model ID must be a positive number")]
    int ModelId,

    [Required(ErrorMessage = "Year is required")]
    [Range(1800, 2100, ErrorMessage = "Year must be between 1800 and 2100")]
    int Year,

    [Required(ErrorMessage = "Engine volume is required")]
    [Range(0.1, 20.0, ErrorMessage = "Engine volume must be between 0.1 and 20.0 liters")]
    double EngineVolume,

    [Required(ErrorMessage = "Transmission type is required")]
    [StringLength(25, MinimumLength = 1, ErrorMessage = "Transmission type must be between 1 and 25 characters")]
    string TransmissionType,

    [Required(ErrorMessage = "Price per hour is required")]
    [Range(0.01, 100000, ErrorMessage = "Price per hour must be between 0.01 and 100000")]
    decimal PricePerHour);