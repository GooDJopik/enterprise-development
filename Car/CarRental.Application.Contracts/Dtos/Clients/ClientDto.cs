namespace CarRental.Application.Contracts.Dtos.Clients;

/// <summary>
/// Client DTO.
/// </summary>
/// <param name="Id">Client identifier.</param>
/// <param name="LicenseNumber">Driver license number.</param>
/// <param name="FullName">Full name.</param>
/// <param name="BirthDate">Birth date.</param>
public record ClientDto(int Id, string LicenseNumber, string FullName, DateTime BirthDate);