using EventIngestionAPI.IntegrationEvents;
using System.Text.Json;

namespace EventIngestionAPI.Infrastructure.Services;

public interface IEventMapper
{
    Task<InternalEvent> Map(JsonElement json);
}
