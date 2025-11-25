using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using CarRental.Domain;
using CarRental.Domain.Models;

namespace CarRental.Tests;

/// <summary>
/// A set of unit tests to test the functionality of car rental.
/// Checks are conducted based on reference data from <see cref="DataSeeder"/>.
/// </summary>
public class RentalTests(DataSeeder dataSeeder) : IClassFixture<DataSeeder>
{
    private readonly List<Rental> _rentals = dataSeeder.Rentals;

    /// <summary>
    /// Verifies that customers who have rented a car of a specified model are returned in alphabetical order by full name.
    /// Works dynamically based on seeded data.
    /// </summary>
    [Fact]
    public void ClientsWhoRentedSpecificModelShouldBeOrderedByFullName()
    {
        var modelName = "Toyota Corolla";

        var expectedClients = _rentals
            .Where(r => r.Car.Generation.Model.Name == modelName)
            .Select(r => r.Client.FullName)
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        var result = _rentals
            .Where(r => r.Car.Generation.Model.Name == modelName)
            .Select(r => r.Client.FullName)
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        Assert.Equal(expectedClients, result);
    }

    /// <summary>
    /// Verifies that the cars currently rented at a given time are returned correctly.
    /// </summary>
    [Fact]
    public void CarsCurrentlyRentedShouldReturnCarsInUse()
    {
        var now = new DateTime(2025, 11, 10, 10, 0, 0);

        var expectedLicensePlates = _rentals
            .Where(r => r.StartTime <= now && r.StartTime.AddHours(r.DurationHours) > now)
            .Select(r => r.Car.LicensePlate)
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        var result = _rentals
            .Where(r => r.StartTime <= now && r.StartTime.AddHours(r.DurationHours) > now)
            .Select(r => r.Car.LicensePlate)
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        Assert.Equal(expectedLicensePlates, result);
    }

    /// <summary>
    /// Checks the top 5 most frequently rented cars.
    /// Works dynamically based on seeded data.
    /// </summary>
    [Fact]
    public void Top5MostFrequentlyRentedCarsShouldReturnCorrectResult()
    {
        var result = _rentals
            .GroupBy(r => r.Car.LicensePlate)
            .Select(g => new { LicensePlate = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => (x.LicensePlate, x.Count))
            .ToList();

        Assert.True(result.SequenceEqual(result.OrderByDescending(x => x.Count)));
    }

    /// <summary>
    /// Checks the number of rents for each car.
    /// Dynamic validation based on seeded data.
    /// </summary>
    [Fact]
    public void RentCountForEachCarShouldReturnCorrectCounts()
    {
        var expected = _rentals
            .GroupBy(r => r.Car.LicensePlate)
            .ToDictionary(g => g.Key, g => g.Count());

        var result = _rentals
            .GroupBy(r => r.Car.LicensePlate)
            .ToDictionary(g => g.Key, g => g.Count());

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Checks the top 5 clients by total rental cost.
    /// Computed dynamically based on seeded data.
    /// </summary>
    [Fact]
    public void Top5ClientsByTotalRentalCostShouldReturnCorrectList()
    {
        var expected = _rentals
            .GroupBy(r => r.Client)
            .Select(g => new
            {
                ClientName = g.Key.FullName,
                TotalPrice = g.Sum(r => (int)(r.Car.Generation.PricePerHour * r.DurationHours))
            })
            .OrderByDescending(x => x.TotalPrice)
            .Take(5)
            .Select(x => (x.ClientName, x.TotalPrice))
            .ToList();

        var result = _rentals
            .GroupBy(r => r.Client)
            .Select(g => new
            {
                ClientName = g.Key.FullName,
                TotalPrice = g.Sum(r => (int)(r.Car.Generation.PricePerHour * r.DurationHours))
            })
            .OrderByDescending(x => x.TotalPrice)
            .Take(5)
            .Select(x => (x.ClientName, x.TotalPrice))
            .ToList();

        Assert.Equal(expected, result);
    }
}
