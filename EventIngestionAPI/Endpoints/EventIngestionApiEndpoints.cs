using System.Text.Json;

namespace EventIngestionAPI.Endpoints;

public static class EventIngestionApiEndpoints
{
    public static void RegisterEventIngestionApiEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("/events", IngestEvent);
    }

    internal static async Task<IResult> IngestEvent(JsonElement payload)
    {
        return Results.Ok(new
        {
            message = "Event ingested successfully.",
            data = payload
        });
    }
}
