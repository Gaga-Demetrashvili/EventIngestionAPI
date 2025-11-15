namespace EventIngestionAPI.IntegrationEvents;

public class InternalEvent
{
    public string PlayerId { get; set; } = string.Empty;
    public decimal? Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public DateTime? OccurredAt { get; set; }
}