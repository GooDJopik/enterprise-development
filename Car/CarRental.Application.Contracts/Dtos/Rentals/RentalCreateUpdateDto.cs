using System.ComponentModel.DataAnnotations;

namespace CarRental.Application.Contracts.Dtos.Rentals;

/// <summary>
/// Rental create/update DTO.
/// </summary>
/// <param name="ClientId">Client identifier.</param>
/// <param name="CarId">Car identifier.</param>
/// <param name="StartTime">Rental start time.</param>
/// <param name="DurationHours">Duration in hours.</param>
public record RentalCreateUpdateDto(
    [Required(ErrorMessage = "Client ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Client ID must be a positive number")]
    int ClientId,

    [Required(ErrorMessage = "Car ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Car ID must be a positive number")]
    int CarId,

    [Required(ErrorMessage = "Start time is required")]
    DateTime StartTime,

    [Required(ErrorMessage = "Duration is required")]
    [Range(1, 744, ErrorMessage = "Duration must be between 1 and 744 hours (1 month)")]
    int DurationHours);