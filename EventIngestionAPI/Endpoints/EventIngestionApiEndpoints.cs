using EventIngestionAPI.Infrastructure.EventBus.Abstractions;
using EventIngestionAPI.Infrastructure.Services;
using EventIngestionAPI.IntegrationEvents;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventIngestionAPI.Endpoints;

public static class EventIngestionApiEndpoints
{
    public static void RegisterEventIngestionApiEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/events", IngestEvent);
        routeBuilder.MapPost("/events/simulate", SimulateEvents);
    }

    internal static async Task<IResult> IngestEvent(JsonElement payload,
        [FromServices] IEventMapper eventMapper,
        [FromServices] IValidator<InternalEvent> validator,
        [FromServices] IEventBus eventBus)
    {
        var internalEvent = await eventMapper.Map(payload);
        var validationResult = validator.Validate(internalEvent);
        if (!validationResult.IsValid)
            return Results.UnprocessableEntity(validationResult.ToDictionary());

        try
        {
            await eventBus.PublishAsync(internalEvent);
        }
        catch (Exception ex)
        {
            return Results.Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status503ServiceUnavailable,
                title: "Failed to publish event");
        }

        return Results.Ok(new
        {
            message = "Event ingested successfully.",
            data = internalEvent
        });
    }

    internal static async Task<IResult> SimulateEvents(
        [FromServices] IEventMapper eventMapper,
        [FromServices] IValidator<InternalEvent> validator,
        [FromServices] IEventBus eventBus)
    {
        const int eventCount = 100;
        var random = new Random();
        var currencies = new[] { "USD", "EUR", "GBP", "JPY", "CAD" };
        var publishedEvents = new List<InternalEvent>();
        var failedEvents = new List<object>();

        for (int i = 0; i < eventCount; i++)
        {
            // Generate random external event as JsonElement
            var externalEvent = new
            {
                player_id = $"player_{random.Next(1000, 9999)}",
                amount = Math.Round((decimal)(random.NextDouble() * 1000), 2),
                currency = currencies[random.Next(currencies.Length)],
                occurred_at = DateTime.UtcNow.AddMinutes(-random.Next(0, 1440)).ToString("o")
            };

            var jsonString = JsonSerializer.Serialize(externalEvent);
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonString);

            try
            {
                var internalEvent = await eventMapper.Map(jsonElement);
                var validationResult = validator.Validate(internalEvent);
                
                if (validationResult.IsValid)
                {
                    await eventBus.PublishAsync(internalEvent);
                    publishedEvents.Add(internalEvent);
                }
                else
                    failedEvents.Add(new { eventData = externalEvent, errors = validationResult.ToDictionary() });
            }
            catch (Exception ex)
            {
                failedEvents.Add(new { eventData = externalEvent, error = ex.Message });
            }
        }

        return Results.Ok(new
        {
            message = $"Simulation complete. {publishedEvents.Count} events published, {failedEvents.Count} failed.",
            publishedCount = publishedEvents.Count,
            failedCount = failedEvents.Count,
            publishedEvents = publishedEvents.Take(10),
            failedEvents = failedEvents.Take(10)
        });
    }
}
