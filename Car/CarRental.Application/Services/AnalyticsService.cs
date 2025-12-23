using AutoMapper;
using CarRental.Application.Contracts.Dtos.Analytics;
using CarRental.Application.Contracts.Dtos.Cars;
using CarRental.Application.Contracts.Dtos.Clients;
using CarRental.Application.Contracts.Interfaces;
using CarRental.Domain.Models;
using CarRental.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Services;

/// <summary>
/// Default implementation of <see cref="IAnalyticsService"/>.
/// </summary>
public class AnalyticsService(CarRentalDbContext dbContext, IMapper mapper) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<List<ClientDto>> GetClientsWhoRentedModel(string modelName)
    {
        var rentals = await LoadRentalsWithDetails();

        var clients = rentals
            .Where(r => r.Car?.Generation?.Model?.Name == modelName)
            .Select(r => r.Client)
            .Where(c => c is not null)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.FullName)
            .ToList();

        return mapper.Map<List<ClientDto>>(clients);
    }

    /// <inheritdoc/>
    public async Task<List<CarDto>> GetCarsCurrentlyRented(DateTime referenceDate)
    {
        var rentals = await LoadRentalsWithDetails();

        var cars = rentals
            .Where(r => r.StartTime <= referenceDate && referenceDate < r.StartTime.AddHours(r.DurationHours))
            .Select(r => r.Car)
            .Where(c => c is not null)
            .DistinctBy(c => c.Id)
            .ToList();

        return mapper.Map<List<CarDto>>(cars);
    }

    /// <inheritdoc/>
    public async Task<List<CarDto>> GetTop5MostRentedCars()
    {
        var rentals = await LoadRentalsWithDetails();

        var topCarIds = rentals
            .Where(r => r.Car is not null)
            .GroupBy(r => r.CarId)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        var cars = rentals
            .Where(r => topCarIds.Contains(r.CarId))
            .Select(r => r.Car)
            .Where(c => c is not null)
            .DistinctBy(c => c.Id)
            .ToDictionary(c => c.Id);

        var orderedTopCars = topCarIds
            .Where(cars.ContainsKey)
            .Select(id => cars[id]!)
            .ToList();

        return mapper.Map<List<CarDto>>(orderedTopCars);
    }

    /// <inheritdoc/>
    public async Task<List<CarRentalCountDto>> GetRentalCountPerCar()
    {
        var cars = await dbContext.Cars.AsNoTracking().ToListAsync();
        var rentals = await dbContext.Rentals.AsNoTracking().ToListAsync();

        var rentalsCountByCarId = rentals
            .GroupBy(r => r.CarId)
            .ToDictionary(g => g.Key, g => g.Count());

        return [.. cars.Select(c => new CarRentalCountDto(c.LicensePlate, rentalsCountByCarId.GetValueOrDefault(c.Id)))];
    }

    /// <inheritdoc/>
    public async Task<List<ClientTotalSpentDto>> GetTop5ClientsByTotalSpent()
    {
        var rentals = await LoadRentalsWithDetails();

        var top5 = rentals
            .GroupBy(r => r.ClientId)
            .Select(g => new
            {
                g.First().Client,
                TotalSpent = g.Sum(r => r.Car!.Generation!.PricePerHour * r.DurationHours)
            })
            .Where(x => x.Client is not null)
            .OrderByDescending(x => x.TotalSpent)
            .Take(5)
            .ToList();

        return [.. top5.Select(x => new ClientTotalSpentDto(x.Client!.FullName, x.TotalSpent))];
    }

    private Task<List<Rental>> LoadRentalsWithDetails()
    {
        return dbContext.Rentals
            .AsNoTracking()
            .Include(r => r.Client)
            .Include(r => r.Car)
                .ThenInclude(c => c.Generation)
                    .ThenInclude(g => g.Model)
            .ToListAsync();
    }
}