using CarRental.Domain.Models;

namespace CarRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for <see cref="Client"/> entities.
/// </summary>
public class ClientRepository(CarRentalDbContext dbContext) : EfRepository<Client>(dbContext);