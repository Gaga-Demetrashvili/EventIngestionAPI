using EventIngestionAPI.Infrastructure.EventBus;

namespace EventIngestionAPI.IntegrationEvents;

public record InternalEvent : Event
{
    public string PlayerId { get; set; } = string.Empty;
    public decimal? Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public DateTime? OccurredAt { get; set; }
}