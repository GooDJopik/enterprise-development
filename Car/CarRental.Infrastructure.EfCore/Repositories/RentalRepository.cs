using CarRental.Domain.Models;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for <see cref="Rental"/> entities.
/// </summary>
public class RentalRepository(CarRentalDbContext dbContext) : EfRepository<Rental>(dbContext);