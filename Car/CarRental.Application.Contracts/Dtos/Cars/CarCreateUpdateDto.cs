using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Contracts.Dtos.Cars;

/// <summary>
/// Car create/update DTO.
/// </summary>
/// <param name="LicensePlate">License plate.</param>
/// <param name="Color">Color.</param>
/// <param name="GenerationId">Model generation identifier.</param>
public record CarCreateUpdateDto(
    [Required(ErrorMessage = "License plate is required")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "License plate must be between 1 and 10 characters")]
    string LicensePlate,

    [Required(ErrorMessage = "Color is required")]
    [StringLength(25, MinimumLength = 1, ErrorMessage = "Color must be between 1 and 25 characters")]
    string Color,

    [Required(ErrorMessage = "Generation ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Generation ID must be a positive number")]
    int GenerationId);