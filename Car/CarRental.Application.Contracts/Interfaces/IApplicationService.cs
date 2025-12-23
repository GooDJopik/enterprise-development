namespace CarRental.Application.Contracts.Interfaces;

/// <summary>
/// Generic application service contract that provides CRUD operations for DTOs.
/// </summary>
/// <typeparam name="TDto">The DTO type.</typeparam>
/// <typeparam name="TCreateUpdateDto">The DTO type used for create and update operations.</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto>
{
    /// <summary>
    /// Gets all items.
    /// </summary>
    /// <returns>A list of items.</returns>
    public Task<List<TDto>> GetAll();

    /// <summary>
    /// Gets an item by identifier.
    /// </summary>
    /// <param name="id">Item identifier.</param>
    /// <returns>The item, or null if it was not found.</returns>
    public ValueTask<TDto?> GetById(int id);

    /// <summary>
    /// Creates a new item.
    /// </summary>
    /// <param name="input">Create input DTO.</param>
    /// <returns>The created item.</returns>
    public Task<TDto> Create(TCreateUpdateDto input);

    /// <summary>
    /// Updates an existing item.
    /// </summary>
    /// <param name="id">Item identifier.</param>
    /// <param name="input">Update input DTO.</param>
    /// <returns>The updated item, or null if it was not found.</returns>
    public Task<TDto?> Update(int id, TCreateUpdateDto input);

    /// <summary>
    /// Deletes an item by identifier.
    /// </summary>
    /// <param name="id">Item identifier.</param>
    /// <returns>true if the item was deleted; otherwise, false.</returns>
    public Task<bool> Delete(int id);
}