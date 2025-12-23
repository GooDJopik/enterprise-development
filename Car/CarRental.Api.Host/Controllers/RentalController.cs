using CarRental.Application.Contracts.Dtos.Rentals;
using CarRental.Application.Contracts.Interfaces;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// CRUD API controller for rentals.
/// </summary>
public sealed class RentalController(IApplicationService<RentalDto, RentalCreateUpdateDto> service, ILogger<RentalController> logger)
    : CrudControllerBase<RentalDto, RentalCreateUpdateDto>(service, logger);