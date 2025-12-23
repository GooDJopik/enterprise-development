namespace CarRental.Application.Contracts.Dtos.Analytics;

/// <summary>
/// DTO that represents a client's total spending on rentals.
/// </summary>
/// <param name="FullName">Client full name.</param>
/// <param name="TotalSpent">Total spent amount.</param>
public record ClientTotalSpentDto(string FullName, decimal TotalSpent);