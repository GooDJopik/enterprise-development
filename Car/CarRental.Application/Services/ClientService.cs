using AutoMapper;
using CarRental.Application.Contracts.Dtos.Clients;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Domain.Models;
using CarRental.Domain.Repositories;

namespace CarRental.Application.Services;

/// <summary>
/// Application service for <see cref="Client"/>.
/// </summary>
public class ClientService(IRepository<Client> repository, IMapper mapper)
    : CrudServiceBase<Client, ClientDto, ClientCreateUpdateDto>(repository, mapper), IApplicationService<ClientDto, ClientCreateUpdateDto>;