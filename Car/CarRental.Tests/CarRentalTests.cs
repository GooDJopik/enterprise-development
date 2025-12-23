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
        var expectedClients = new List<int>
        {
            data.Clients[0].Id, 
            data.Clients[7].Id,
        }.OrderBy(x => x).ToList();

        var clients = data.Rentals
            .Where(r => r.Car.Generation.Model.Name == "Toyota Corolla")
            .Select(r => r.Client.Id)
            .Distinct()
            .OrderBy(id => id)
            .ToList();

        Assert.NotEmpty(clients);
        Assert.Equal(expectedClients, clients);
    }

    /// <summary>
    /// Cars that are leased on a specific date.
    /// </summary>
    [Fact]
    public void CarsCurrentlyRentedShouldReturnCorrectCars()
    {
        var referenceDate = new DateTime(2025, 11, 10, 10, 0, 0);
        var expectedCars = new List<string>
        {
            data.Cars[0].LicensePlate, 
            data.Cars[1].LicensePlate
        }.OrderBy(x => x).ToList();

        var currentlyRented = data.Rentals
            .Where(r => r.StartTime <= referenceDate && referenceDate < r.StartTime.AddHours(r.DurationHours))
            .Select(r => r.Car.LicensePlate)
            .Distinct()
            .OrderBy(x => x)
            .ToList();

        Assert.Equal(expectedCars, currentlyRented);
    }

    /// <summary>
    /// Top 5 most frequently rented cars.
    /// </summary>
    [Fact]
    public void Top5MostRentedCarsShouldReturnCorrectCars()
    {
        var expectedTop = new List<string>
        {
            "C323OB", 
            "I101DH", 
            "M110LC", 
            "Y349KY", 
            "E111EP"
        }.OrderBy(x => x).ToList();

        var topCars = data.Rentals
            .Where(r => r.Car != null)
            .GroupBy(r => r.Car.LicensePlate)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .OrderBy(x => x)
            .ToList();

        Assert.Equal(expectedTop, topCars);
    }

    /// <summary>
    /// The number of rents for each car.
    /// </summary>
    [Theory]
    [InlineData("I101DH", 3)]
    [InlineData("O122OP", 1)]
    [InlineData("C323OB", 5)]
    [InlineData("M234PP", 2)]
    [InlineData("A754BA", 2)]
    public void RentalCountPerCarShouldReturnCorrectCounts(string licensePlate, int expectedCount)
    {
        var actualCount = data.Rentals.Count(r => r.Car.LicensePlate == licensePlate);

        Assert.Equal(expectedCount, actualCount);
    }

    /// <summary>
    /// Top 5 clients by rental amount.
    /// </summary>
    [Fact]
    public void Top5ClientsByTotalSpentShouldReturnCorrectClients()
    {
        var expected = new List<string>
        {
            "Petrov Alexey Sergeevich",
            "Ivanov Ivan Ivanovich",
            "Popov Sergey Viktorovich",
            "Gusev Alexey Konstantinovich",
            "Smirnova Ekaterina Nikolaevna"
        }.OrderBy(x => x).ToList();

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
            .Select(x => x.FullName)
            .OrderBy(x => x)
            .ToList();

        Assert.Equal(expected, top5);
    }
}
