namespace EventIngestionAPI.IntegrationEvents;

public record InternalEvent(
    string PlayerId,
    decimal Amount,
    string Currency,
    DateTime OccurredAt
);
