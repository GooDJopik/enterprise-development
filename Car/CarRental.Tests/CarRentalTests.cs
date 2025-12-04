using CarRental.Domain;
using CarRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CarRental.Tests;

/// <summary>
/// Unit tests for car rental queries using DataSeeder fixture.
/// </summary>
public class RentalTests(DataSeeder data) : IClassFixture<DataSeeder>
{
    /// <summary>
    /// Customers who have rented a Toyota Corolla, sorted by full name.
    /// </summary>
    [Fact]
    public void ClientsByModelShouldReturnClientsOrderedByFullName()
    {
        var clients = data.Rentals
            .Where(r => r.Car?.Generation?.Model?.Name == "Toyota Corolla")
            .Select(r => r.Client)
            .Where(c => c != null)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        var expected = new List<string>
        {
            "Иванов Иван Иванович",
            "Морозова Наталья Андреевна"
        };

        Assert.Equal(expected.Count, clients.Count);
        Assert.All(expected, name => Assert.Contains(clients, c => c.FullName == name));
    }

    /// <summary>
    /// Cars that are leased on a specific date.
    /// </summary>
    [Fact]
    public void CarsCurrentlyRentedShouldReturnCorrectCars()
    {
        var referenceDate = new DateTime(2025, 11, 10, 10, 0, 0);

        var currentlyRented = data.Rentals
            .Where(r => r.StartTime <= referenceDate && referenceDate < r.StartTime.AddHours(r.DurationHours))
            .Select(r => r.Car)
            .Distinct()
            .ToList();

        var expectedCars = new List<Car>
        {
            data.Cars[0], 
            data.Cars[1]
        };

        Assert.Equal(expectedCars.Count, currentlyRented.Count);
        Assert.All(expectedCars, car => Assert.Contains(car, currentlyRented));
    }

    /// <summary>
    /// Top 5 most frequently rented cars.
    /// </summary>
    [Fact]
    public void Top5MostRentedCarsShouldReturnCorrectCars()
    {
        var topCars = data.Rentals
            .Where(r => r.Car != null)
            .GroupBy(r => r.Car)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        var expectedTop = new List<string>
        {
            "С323ОВ", 
            "И101ВР", 
            "М110ЛС", 
            "У349КУ", 
            "Е111ЕР"
        };

        Assert.Equal(expectedTop.Count, topCars.Count);
        foreach (var license in expectedTop)
        {
            Assert.Contains(topCars, c => c.LicensePlate == license);
        }
    }

    /// <summary>
    /// The number of rents for each car.
    /// </summary>
    [Fact]
    public void RentalCountPerCarShouldReturnCorrectCounts()
    {
        var counts = data.Cars.ToDictionary(
            car => car.LicensePlate,
            car => data.Rentals.Count(r => r.Car == car)
        );

        Assert.Equal(3, counts["И101ВР"]);
        Assert.Equal(1, counts["О122ОР"]);
        Assert.Equal(5, counts["С323ОВ"]);
        Assert.Equal(2, counts["М234РР"]);
        Assert.Equal(2, counts["А754ВА"]);
    }

    /// <summary>
    /// Top 5 clients by rental amount.
    /// </summary>
    [Fact]
    public void Top5ClientsByTotalSpentShouldReturnCorrectClients()
    {
        var top5 = data.Clients
            .Select(c => new
            {
                c.FullName,
                TotalSpent = data.Rentals
                    .Where(r => r.Client == c)
                    .Sum(r => r.Car.Generation.PricePerHour * r.DurationHours)
            })
            .OrderByDescending(x => x.TotalSpent)
            .Take(5)
            .ToList();

        var expected = new List<string>
        {
            "Петров Алексей Сергеевич",
            "Иванов Иван Иванович",
            "Попов Сергей Викторович",
            "Гусев Алексей Константинович",
            "Смирнова Екатерина Николаевна"
        };

        Assert.Equal(expected.Count, top5.Count);
        Assert.All(expected, name => Assert.Contains(top5, x => x.FullName == name));
    }
}
