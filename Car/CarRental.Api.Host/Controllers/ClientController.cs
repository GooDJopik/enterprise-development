using CarRental.Application.Contracts.Dtos.Clients;
using CarRental.Application.Contracts.Interfaces;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// CRUD API controller for clients.
/// </summary>
public sealed class ClientController(IApplicationService<ClientDto, ClientCreateUpdateDto> service, ILogger<ClientController> logger)
    : CrudControllerBase<ClientDto, ClientCreateUpdateDto>(service, logger);