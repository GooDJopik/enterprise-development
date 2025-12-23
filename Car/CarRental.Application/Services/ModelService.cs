using AutoMapper;
using CarRental.Application.Contracts.Dtos.Models;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Domain.Models;
using CarRental.Domain.Repositories;

namespace CarRental.Application.Services;

/// <summary>
/// Application service for <see cref="Model"/>.
/// </summary>
public class ModelService(IRepository<Model> repository, IMapper mapper)
    : CrudServiceBase<Model, ModelDto, ModelCreateUpdateDto>(repository, mapper), IApplicationService<ModelDto, ModelCreateUpdateDto>;