using CarRental.Application.Contracts.Dtos.ModelGenerations;
using CarRental.Application.Contracts.Interfaces;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// CRUD API controller for model generations.
/// </summary>
public sealed class ModelGenerationController(
    IApplicationService<ModelGenerationDto, ModelGenerationCreateUpdateDto> service,
    ILogger<ModelGenerationController> logger)
    : CrudControllerBase<ModelGenerationDto, ModelGenerationCreateUpdateDto>(service, logger);