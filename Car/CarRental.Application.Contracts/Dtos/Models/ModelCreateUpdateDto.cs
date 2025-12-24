using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Contracts.Dtos.Models;

/// <summary>
/// Model create/update DTO.
/// </summary>
/// <param name="Name">Model name.</param>
/// <param name="DriveType">Drive type.</param>
/// <param name="Seats">Number of seats.</param>
/// <param name="BodyType">Body type.</param>
/// <param name="CarClass">Car class.</param>
public record ModelCreateUpdateDto(
    [Required(ErrorMessage = "Model name is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Model name must be between 1 and 100 characters")]
    string Name,

    [Required(ErrorMessage = "Drive type is required")]
    [StringLength(25, MinimumLength = 1, ErrorMessage = "Drive type must be between 1 and 25 characters")]
    string DriveType,

    [Required(ErrorMessage = "Number of seats is required")]
    [Range(1, 10, ErrorMessage = "Number of seats must be between 1 and 10")]
    int Seats,

    [Required(ErrorMessage = "Body type is required")]
    [StringLength(25, MinimumLength = 1, ErrorMessage = "Body type must be between 1 and 25 characters")]
    string BodyType,

    [Required(ErrorMessage = "Car class is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Car class must be between 1 and 20 characters")]
    string CarClass);