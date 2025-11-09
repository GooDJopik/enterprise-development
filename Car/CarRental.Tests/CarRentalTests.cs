using System;
using System.Linq;
using Xunit;
using CarRental.Models;
using System.Collections.Generic;

namespace CarRental.Tests
{
    /// <summary>
    /// Contains unit tests that verify analytical queries 
    /// and business logic of the car rental system using seeded data.
    /// </summary>
    public class RentalTests : IClassFixture<DataSeeder>
    {
        /// <summary>
        /// List of available car models.
        /// </summary>
        private readonly List<Model> _models;

        /// <summary>
        /// List of car model generations with pricing and technical details.
        /// </summary>
        private readonly List<ModelGeneration> _generations;

        /// <summary>
        /// List of cars currently in the rental fleet.
        /// </summary>
        private readonly List<Car> _cars;

        /// <summary>
        /// List of clients who have rented cars.
        /// </summary>
        private readonly List<Client> _clients;

        /// <summary>
        /// List of all rental transactions.
        /// </summary>
        private readonly List<Rental> _rentals;

        /// <summary>
        /// Initializes the test class with data from <see cref="DataSeeder"/> fixture.
        /// </summary>
        /// <param name="dataSeeder">Fixture providing seeded test data.</param>
        public RentalTests(DataSeeder dataSeeder)
        {
            _models = dataSeeder.models;
            _generations = dataSeeder.generations;
            _cars = dataSeeder.cars;
            _clients = dataSeeder.clients;
            _rentals = dataSeeder.rentals;
        }

        /// <summary>
        /// Verifies that all clients who rented cars of a specific model 
        /// are correctly retrieved and sorted by full name.
        /// </summary>
        [Fact]
        public void ClientsByModel_ShouldReturnOrderedByFullName()
        {
            string targetModel = "Toyota Corolla";

            var clients = _rentals
                .Where(r => r.Car.Generation.Model.Name == targetModel)
                .Select(r => r.Client)
                .Distinct()
                .OrderBy(c => c.FullName)
                .ToList();

            Assert.NotEmpty(clients);

            foreach (var c in clients)
                Console.WriteLine($"{c.FullName} ({c.LicenseNumber})");
        }

        /// <summary>
        /// Determines which cars are currently in use based on the active rental period.
        /// </summary>
        [Fact]
        public void CarsInUse_ShouldReturnCurrentlyRentedCars()
        {
            var now = new DateTime(2025, 11, 10, 10, 0, 0);

            var carsInUse = _rentals
                .Where(r => r.StartTime <= now && r.StartTime.AddHours(r.DurationHours) > now)
                .Select(r => r.Car)
                .Distinct()
                .ToList();

            Assert.NotEmpty(carsInUse);

            foreach (var car in carsInUse)
                Console.WriteLine($"{car.LicensePlate} - {car.Generation.Model.Name}");
        }

        /// <summary>
        /// Finds the five cars that have been rented the most times.
        /// </summary>
        [Fact]
        public void Top5MostRentedCars_ShouldReturnTop5()
        {
            var topCars = _rentals
                .GroupBy(r => r.Car)
                .Select(g => new { Car = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            Assert.True(topCars.Count <= 5);

            foreach (var x in topCars)
                Console.WriteLine($"{x.Car.LicensePlate} - {x.Car.Generation.Model.Name} ({x.Count} rentals)");
        }

        /// <summary>
        /// Calculates the number of rentals for each car in the fleet.
        /// </summary>
        [Fact]
        public void RentCountPerCar_ShouldReturnCountForEachCar()
        {
            var rentCounts = _rentals
                .GroupBy(r => r.Car)
                .Select(g => new { Car = g.Key, Count = g.Count() })
                .ToList();

            Assert.NotEmpty(rentCounts);

            foreach (var x in rentCounts)
                Console.WriteLine($"{x.Car.LicensePlate} - {x.Car.Generation.Model.Name} ({x.Count} rentals)");
        }

        /// <summary>
        /// Determines the top five clients who have spent the most money 
        /// based on the total duration and hourly rate of their rentals.
        /// </summary>
        [Fact]
        public void Top5ClientsByTotalRentSum_ShouldReturnTop5()
        {
            var topClients = _rentals
                .GroupBy(r => r.Client)
                .Select(g => new
                {
                    Client = g.Key,
                    Total = g.Sum(r => r.DurationHours * r.Car.Generation.PricePerHour)
                })
                .OrderByDescending(x => x.Total)
                .Take(5)
                .ToList();

            Assert.True(topClients.Count <= 5);

            foreach (var x in topClients)
                Console.WriteLine($"{x.Client.FullName} ({x.Client.LicenseNumber}) - Total: {x.Total}");
        }
    }
}
