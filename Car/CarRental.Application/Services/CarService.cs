using AutoMapper;
using CarRental.Application.Contracts.Dtos.Cars;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Domain.Models;
using CarRental.Domain.Repositories;

namespace CarRental.Application.Services;

/// <summary>
/// Application service for <see cref="Car"/>.
/// </summary>
public class CarService(
    IRepository<Car> repository,
    IRepository<ModelGeneration> generationRepository,
    IMapper mapper)
    : CrudServiceBase<Car, CarDto, CarCreateUpdateDto>(repository, mapper), IApplicationService<CarDto, CarCreateUpdateDto>
{
    /// <inheritdoc/>
    public override async Task<CarDto> Create(CarCreateUpdateDto input)
    {
        var generation = await generationRepository.GetById(input.GenerationId) 
            ?? throw new InvalidOperationException($"Model generation with id={input.GenerationId} was not found.");
        var entity = Mapper.Map<Car>(input);
        entity.Generation = generation;

        await Repository.Add(entity);
        return Mapper.Map<CarDto>(entity);
    }

    /// <inheritdoc/>
    public override async Task<CarDto?> Update(int id, CarCreateUpdateDto input)
    {
        var existing = await Repository.GetById(id);
        if (existing is null)
        {
            return null;
        }

        var generation = await generationRepository.GetById(input.GenerationId) 
            ?? throw new InvalidOperationException($"Model generation with id={input.GenerationId} was not found.");
        Mapper.Map(input, existing);
        existing.Generation = generation;

        await Repository.Update(existing);
        return Mapper.Map<CarDto>(existing);
    }
}