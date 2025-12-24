using CarRental.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Base EF Core repository that implements CRUD operations.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public abstract class EfRepository<TEntity>(CarRentalDbContext dbContext) : IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// EF Core DbContext.
    /// </summary>
    protected CarRentalDbContext DbContext { get; } = dbContext;

    /// <summary>
    /// DbSet for the entity.
    /// </summary>
    protected DbSet<TEntity> Set => DbContext.Set<TEntity>();

    /// <inheritdoc />
    public virtual ValueTask<TEntity?> GetById(int id)
        => Set.FindAsync(id);

    /// <inheritdoc />
    public virtual Task<List<TEntity>> GetAll()
        => Set.ToListAsync();

    /// <inheritdoc />
    public async Task Add(TEntity entity)
    {
        await Set.AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Update(TEntity entity)
    {
        Set.Update(entity);
        await DbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Remove(TEntity entity)
    {
        Set.Remove(entity);
        await DbContext.SaveChangesAsync();
    }
}