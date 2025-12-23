using CarRental.Domain.Models;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for <see cref="Car"/> entities.
/// </summary>
public class CarRepository(CarRentalDbContext dbContext) : EfRepository<Car>(dbContext);