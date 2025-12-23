using AutoMapper;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Domain.Repositories;

namespace CarRental.Application.Services;

/// <summary>
/// Base implementation of <see cref="IApplicationService{TDto,TCreateUpdateDto}"/>.
/// </summary>
/// <typeparam name="TEntity">Domain entity type.</typeparam>
/// <typeparam name="TDto">DTO type.</typeparam>
/// <typeparam name="TCreateUpdateDto">Create/update DTO type.</typeparam>
public abstract class CrudServiceBase<TEntity, TDto, TCreateUpdateDto>(IRepository<TEntity> repository, IMapper mapper)
    : IApplicationService<TDto, TCreateUpdateDto>
    where TEntity : class
{
    /// <summary>
    /// Gets the repository used by the service.
    /// </summary>
    protected IRepository<TEntity> Repository { get; } = repository;

    /// <summary>
    /// Gets the AutoMapper instance used by the service.
    /// </summary>
    protected IMapper Mapper { get; } = mapper;

    /// <inheritdoc/>
    public virtual async Task<List<TDto>> GetAll()
    {
        var entities = await Repository.GetAll();
        return Mapper.Map<List<TDto>>(entities);
    }

    /// <inheritdoc/>
    public virtual async ValueTask<TDto?> GetById(int id)
    {
        var entity = await Repository.GetById(id);
        return entity is null ? default : Mapper.Map<TDto>(entity);
    }

    /// <inheritdoc/>
    public virtual async Task<TDto> Create(TCreateUpdateDto input)
    {
        var entity = Mapper.Map<TEntity>(input);
        await Repository.Add(entity);
        return Mapper.Map<TDto>(entity);
    }

    /// <inheritdoc/>
    public virtual async Task<TDto?> Update(int id, TCreateUpdateDto input)
    {
        var existing = await Repository.GetById(id);
        if (existing is null)
        {
            return default;
        }

        Mapper.Map(input, existing);
        await Repository.Update(existing);
        return Mapper.Map<TDto>(existing);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> Delete(int id)
    {
        var existing = await Repository.GetById(id);
        if (existing is null)
        {
            return false;
        }

        await Repository.Remove(existing);
        return true;
    }
}