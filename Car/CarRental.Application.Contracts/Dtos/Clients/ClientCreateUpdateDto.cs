namespace CarRental.Application.Contracts.Dtos.Clients;

/// <summary>
/// Client create/update DTO.
/// </summary>
/// <param name="LicenseNumber">Driver license number.</param>
/// <param name="FullName">Full name.</param>
/// <param name="BirthDate">Birth date.</param>
public record ClientCreateUpdateDto(string LicenseNumber, string FullName, DateTime BirthDate);