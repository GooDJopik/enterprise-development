namespace CarRental.Application.Contracts.Dtos.Models;

/// <summary>
/// Model DTO.
/// </summary>
/// <param name="Id">Model identifier.</param>
/// <param name="Name">Model name.</param>
/// <param name="DriveType">Drive type.</param>
/// <param name="Seats">Number of seats.</param>
/// <param name="BodyType">Body type.</param>
/// <param name="CarClass">Car class.</param>
public record ModelDto(int Id, string Name, string DriveType, int Seats, string BodyType, string CarClass);