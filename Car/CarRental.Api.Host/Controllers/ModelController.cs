using CarRental.Application.Contracts.Dtos.Models;
using CarRental.Application.Contracts.Interfaces;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// CRUD API controller for models.
/// </summary>
public sealed class ModelController(IApplicationService<ModelDto, ModelCreateUpdateDto> service, ILogger<ModelController> logger)
    : CrudControllerBase<ModelDto, ModelCreateUpdateDto>(service, logger);