using CarRental.Domain.Models;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for <see cref="Model"/> entities.
/// </summary>
public class ModelRepository(CarRentalDbContext dbContext) : EfRepository<Model>(dbContext);