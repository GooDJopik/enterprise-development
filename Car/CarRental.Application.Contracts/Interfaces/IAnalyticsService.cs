using CarRental.Application.Contracts.Dtos.Analytics;
using CarRental.Application.Contracts.Dtos.Cars;
using CarRental.Application.Contracts.Dtos.Clients;

namespace CarRental.Application.Contracts.Interfaces;

/// <summary>
/// Analytics service contract for predefined rental reports.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Returns information about all clients who rented cars of the specified model.
    /// The result is ordered by full name.
    /// </summary>
    /// <param name="modelName">Car model name.</param>
    /// <returns>A list of client DTOs ordered by full name.</returns>
    public Task<List<ClientDto>> GetClientsWhoRentedModel(string modelName);

    /// <summary>
    /// Returns information about cars that are in rent at the specified moment.
    /// </summary>
    /// <param name="referenceDate">A date/time used to determine active rentals.</param>
    /// <returns>A list of rented car DTOs.</returns>
    public Task<List<CarDto>> GetCarsCurrentlyRented(DateTime referenceDate);

    /// <summary>
    /// Returns the top 5 most frequently rented cars.
    /// </summary>
    /// <returns>A list of car DTOs (top 5).</returns>
    public Task<List<CarDto>> GetTop5MostRentedCars();

    /// <summary>
    /// Returns the number of rentals for each car.
    /// </summary>
    /// <returns>A list of rental counts per car.</returns>
    public Task<List<CarRentalCountDto>> GetRentalCountPerCar();

    /// <summary>
    /// Returns the top 5 clients by total rental amount.
    /// </summary>
    /// <returns>A list of client totals (top 5).</returns>
    public Task<List<ClientTotalSpentDto>> GetTop5ClientsByTotalSpent();
}