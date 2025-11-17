namespace EventIngestionAPI.Infrastructure.EventBus;

public record Event
{
    public Event()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
    }

    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
}
