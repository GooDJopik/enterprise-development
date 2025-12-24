using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for <see cref="ModelGeneration"/> entities.
/// </summary>
public class ModelGenerationRepository(CarRentalDbContext dbContext) : EfRepository<ModelGeneration>(dbContext)
{
    /// <inheritdoc />
    public override async ValueTask<ModelGeneration?> GetById(int id)
        => await Set.Include(mg => mg.Model).FirstOrDefaultAsync(mg => mg.Id == id);

    /// <inheritdoc />
    public override Task<List<ModelGeneration>> GetAll()
        => Set.Include(mg => mg.Model).ToListAsync();
}