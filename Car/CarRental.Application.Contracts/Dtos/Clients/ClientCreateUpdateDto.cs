using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Contracts.Dtos.Clients;

/// <summary>
/// Client create/update DTO.
/// </summary>
/// <param name="LicenseNumber">Driver license number.</param>
/// <param name="FullName">Full name.</param>
/// <param name="BirthDate">Birth date.</param>
public record ClientCreateUpdateDto(
    [Required(ErrorMessage = "License number is required")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "License number must be between 1 and 20 characters")]
    string LicenseNumber,

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Full name must be between 1 and 200 characters")]
    string FullName,

    [Required(ErrorMessage = "Birth date is required")]
    DateTime BirthDate);