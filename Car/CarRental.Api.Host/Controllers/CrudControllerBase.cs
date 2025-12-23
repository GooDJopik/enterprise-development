using CarRental.Application.Contracts.Interfaces;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Host.Controllers;

/// <summary>
/// Base CRUD controller for DTO-based application services.
/// </summary>
/// <typeparam name="TDto">DTO type.</typeparam>
/// <typeparam name="TCreateUpdateDto">Create/update DTO type.</typeparam>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto>(
    IApplicationService<TDto, TCreateUpdateDto> service,
    ILogger logger)
    : ControllerBase
{
    /// <summary>
    /// Returns all items.
    /// </summary>
    /// <returns>All items.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<List<TDto>>> GetAll()
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
            var items = await service.GetAll();

            logger.LogInformation(
                "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} count={Count} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status200OK,
                items.Count,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Ok(items);
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
    /// Returns item by id.
    /// </summary>
    /// <param name="id">Item identifier.</param>
    /// <returns>Item if found; otherwise 404.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> GetById([FromRoute] int id)
    {
        var start = Stopwatch.GetTimestamp();
        var controllerName = GetType().Name;
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        logger.LogInformation(
            "Request started: traceId={TraceId} controller={Controller} action={Action} id={Id}",
            HttpContext.TraceIdentifier,
            controllerName,
            actionName,
            id);

        try
        {
            var dto = await service.GetById(id);
            if (dto is null)
            {
                logger.LogInformation(
                    "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                    HttpContext.TraceIdentifier,
                    controllerName,
                    actionName,
                    StatusCodes.Status404NotFound,
                    Stopwatch.GetElapsedTime(start).TotalMilliseconds);

                return NotFound();
            }

            logger.LogInformation(
                "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status200OK,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Ok(dto);
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
    /// Creates a new item.
    /// </summary>
    /// <param name="input">Create DTO.</param>
    /// <returns>Created item.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto input)
    {
        var start = Stopwatch.GetTimestamp();
        var controllerName = GetType().Name;
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        logger.LogInformation(
            "Request started: traceId={TraceId} controller={Controller} action={Action} bodyType={BodyType}",
            HttpContext.TraceIdentifier,
            controllerName,
            actionName,
            typeof(TCreateUpdateDto).Name);

        try
        {
            var created = await service.Create(input);

            logger.LogInformation(
                "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status201Created,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Created(string.Empty, created);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(
                ex,
                "Request failed (bad request): traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status400BadRequest,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Problem(title: ex.Message, statusCode: StatusCodes.Status400BadRequest);
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
    /// Updates an existing item.
    /// </summary>
    /// <param name="id">Item identifier.</param>
    /// <param name="input">Update DTO.</param>
    /// <returns>Updated item if found; otherwise 404.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Update([FromRoute] int id, [FromBody] TCreateUpdateDto input)
    {
        var start = Stopwatch.GetTimestamp();
        var controllerName = GetType().Name;
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        logger.LogInformation(
            "Request started: traceId={TraceId} controller={Controller} action={Action} id={Id} bodyType={BodyType}",
            HttpContext.TraceIdentifier,
            controllerName,
            actionName,
            id,
            typeof(TCreateUpdateDto).Name);

        try
        {
            var updated = await service.Update(id, input);
            if (updated is null)
            {
                logger.LogInformation(
                    "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                    HttpContext.TraceIdentifier,
                    controllerName,
                    actionName,
                    StatusCodes.Status404NotFound,
                    Stopwatch.GetElapsedTime(start).TotalMilliseconds);

                return NotFound();
            }

            logger.LogInformation(
                "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status200OK,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(
                ex,
                "Request failed (bad request): traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status400BadRequest,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return Problem(title: ex.Message, statusCode: StatusCodes.Status400BadRequest);
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
    /// Deletes an item.
    /// </summary>
    /// <param name="id">Item identifier.</param>
    /// <returns>204 if deleted; 404 if not found.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> Delete([FromRoute] int id)
    {
        var start = Stopwatch.GetTimestamp();
        var controllerName = GetType().Name;
        var actionName = ControllerContext.ActionDescriptor.ActionName;
        logger.LogInformation(
            "Request started: traceId={TraceId} controller={Controller} action={Action} id={Id}",
            HttpContext.TraceIdentifier,
            controllerName,
            actionName,
            id);

        try
        {
            var deleted = await service.Delete(id);
            if (!deleted)
            {
                logger.LogInformation(
                    "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                    HttpContext.TraceIdentifier,
                    controllerName,
                    actionName,
                    StatusCodes.Status404NotFound,
                    Stopwatch.GetElapsedTime(start).TotalMilliseconds);

                return NotFound();
            }

            logger.LogInformation(
                "Request completed: traceId={TraceId} controller={Controller} action={Action} status={StatusCode} elapsedMs={ElapsedMs}",
                HttpContext.TraceIdentifier,
                controllerName,
                actionName,
                StatusCodes.Status204NoContent,
                Stopwatch.GetElapsedTime(start).TotalMilliseconds);

            return NoContent();
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