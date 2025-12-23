using AutoMapper;
using CarRental.Application.Contracts.Dtos.ModelGenerations;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Domain.Models;
using CarRental.Domain.Repositories;

namespace CarRental.Application.Services;

/// <summary>
/// Application service for <see cref="ModelGeneration"/>.
/// </summary>
public class ModelGenerationService(
    IRepository<ModelGeneration> repository,
    IRepository<Model> modelRepository,
    IMapper mapper)
    : CrudServiceBase<ModelGeneration, ModelGenerationDto, ModelGenerationCreateUpdateDto>(repository, mapper),
        IApplicationService<ModelGenerationDto, ModelGenerationCreateUpdateDto>
{
    /// <inheritdoc/>
    public override async Task<ModelGenerationDto> Create(ModelGenerationCreateUpdateDto input)
    {
        var model = await modelRepository.GetById(input.ModelId) ?? throw new InvalidOperationException($"Model with id={input.ModelId} was not found.");
        var entity = Mapper.Map<ModelGeneration>(input);
        entity.Model = model;

        await Repository.Add(entity);
        return Mapper.Map<ModelGenerationDto>(entity);
    }

    /// <inheritdoc/>
    public override async Task<ModelGenerationDto?> Update(int id, ModelGenerationCreateUpdateDto input)
    {
        var existing = await Repository.GetById(id);
        if (existing is null)
        {
            return null;
        }

        var model = await modelRepository.GetById(input.ModelId) ?? throw new InvalidOperationException($"Model with id={input.ModelId} was not found.");
        Mapper.Map(input, existing);
        existing.Model = model;

        await Repository.Update(existing);
        return Mapper.Map<ModelGenerationDto>(existing);
    }
}