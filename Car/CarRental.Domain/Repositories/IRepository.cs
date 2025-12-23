namespace CarRental.Domain.Repositories;

/// <summary>
/// Defines a CRUD repository.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Gets an entity by its integer identifier.
    /// </summary>
    /// <param name="id">Entity identifier.</param>
    /// <returns>Entity instance or null if not found.</returns>
    public ValueTask<TEntity?> GetById(int id);

    /// <summary>
    /// Returns all entities.
    /// </summary>
    /// <returns>List of entities.</returns>
    public Task<List<TEntity>> GetAll();

    /// <summary>
    /// Adds a new entity to the storage.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    public Task Add(TEntity entity);

    /// <summary>
    /// Updates an existing entity in the storage.
    /// </summary>
    /// <param name="entity">Entity to update.</param>
    public Task Update(TEntity entity);

    /// <summary>
    /// Removes an existing entity from the storage.
    /// </summary>
    /// <param name="entity">Entity to remove.</param>
    public Task Remove(TEntity entity);
}