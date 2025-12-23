using CarRental.Application.Contracts.Dtos.Cars;
using CarRental.Application.Contracts.Interfaces;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// CRUD API controller for cars.
/// </summary>
public sealed class CarController(IApplicationService<CarDto, CarCreateUpdateDto> service, ILogger<CarController> logger)
    : CrudControllerBase<CarDto, CarCreateUpdateDto>(service, logger);