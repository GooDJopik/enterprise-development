using CarRental.Domain.Models;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for <see cref="ModelGeneration"/> entities.
/// </summary>
public class ModelGenerationRepository(CarRentalDbContext dbContext) : EfRepository<ModelGeneration>(dbContext);