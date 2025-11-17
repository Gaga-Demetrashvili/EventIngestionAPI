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

        await eventBus.PublishAsync(internalEvent);

        return Results.Ok(new
        {
            message = "Event ingested successfully.",
            data = internalEvent
        });
    }
}
