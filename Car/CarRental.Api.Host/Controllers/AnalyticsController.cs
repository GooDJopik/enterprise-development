using CarRental.Application.Contracts.Dtos.Analytics;
using CarRental.Application.Contracts.Dtos.Cars;
using CarRental.Application.Contracts.Dtos.Clients;
using CarRental.Application.Contracts.Interfaces;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// API controller for analytics queries.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Returns clients who rented cars of the specified model.
    /// </summary>
    /// <param name="modelName">Car model name.</param>
    /// <returns>List of clients.</returns>
    [HttpGet("clients-rented-model")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<ClientDto>>> GetClientsWhoRentedModel([FromQuery] string modelName)
    {
        var start = Stopwatch.GetTimestamp();
        var controllerName = GetType().Name;
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        logger.LogInformation(
            "Request started: traceId={TraceId} controller={Controller} action={Action} modelName={ModelName}",
            HttpContext.TraceIdentifier,
            controllerName,
            actionName,
            modelName);

        try
        {
            var result = await analyticsService.GetClientsWhoRentedModel(modelName);
            logger.LogInformation(
                "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} count={Count} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status200OK,
                result.Count,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Request failed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status500InternalServerError,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Problem(title: "Internal Server Error", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Returns cars that are currently rented at the specified moment.
    /// </summary>
    /// <param name="referenceDate">Point in time.</param>
    /// <returns>List of rented cars.</returns>
    [HttpGet("cars-currently-rented")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CarDto>>> GetCarsCurrentlyRented([FromQuery] DateTime referenceDate)
    {
        var start = Stopwatch.GetTimestamp();
        var controllerName = GetType().Name;
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        logger.LogInformation(
            "Request started: traceId={TraceId} controller={Controller} action={Action} referenceDate={ReferenceDate}",
            HttpContext.TraceIdentifier,
            controllerName,
            actionName,
            referenceDate);

        try
        {
            var result = await analyticsService.GetCarsCurrentlyRented(referenceDate);
            logger.LogInformation(
                "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} count={Count} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status200OK,
                result.Count,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Request failed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status500InternalServerError,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Problem(title: "Internal Server Error", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Returns top 5 most rented cars.
    /// </summary>
    /// <returns>List of cars (top 5).</returns>
    [HttpGet("top-5-most-rented-cars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CarDto>>> GetTop5MostRentedCars()
    {
        var start = Stopwatch.GetTimestamp();
        var controllerName = GetType().Name;
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        logger.LogInformation(
            "Request started: traceId={TraceId} controller={Controller} action={Action}",
            HttpContext.TraceIdentifier,
            controllerName,
            actionName);

        try
        {
            var result = await analyticsService.GetTop5MostRentedCars();
            logger.LogInformation(
                "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} count={Count} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status200OK,
                result.Count,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Request failed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status500InternalServerError,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Problem(title: "Internal Server Error", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Returns rental count per car.
    /// </summary>
    /// <returns>List of rental counts per car.</returns>
    [HttpGet("rental-count-per-car")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CarRentalCountDto>>> GetRentalCountPerCar()
    {
        var start = Stopwatch.GetTimestamp();
        var controllerName = GetType().Name;
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        logger.LogInformation(
            "Request started: traceId={TraceId} controller={Controller} action={Action}",
            HttpContext.TraceIdentifier,
            controllerName,
            actionName);

        try
        {
            var result = await analyticsService.GetRentalCountPerCar();
            logger.LogInformation(
                "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} count={Count} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status200OK,
                result.Count,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Request failed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status500InternalServerError,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Problem(title: "Internal Server Error", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Returns top 5 clients by total spent.
    /// </summary>
    /// <returns>List of client totals (top 5).</returns>
    [HttpGet("top-5-clients-by-total-spent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<ClientTotalSpentDto>>> GetTop5ClientsByTotalSpent()
    {
        var start = Stopwatch.GetTimestamp();
        var controllerName = GetType().Name;
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        logger.LogInformation(
            "Request started: traceId={TraceId} controller={Controller} action={Action}",
            HttpContext.TraceIdentifier,
            controllerName,
            actionName);

        try
        {
            var result = await analyticsService.GetTop5ClientsByTotalSpent();
            logger.LogInformation(
                "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} count={Count} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status200OK,
                result.Count,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Request failed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status500InternalServerError,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Problem(title: "Internal Server Error", statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}