namespace EventIngestionAPI.Entities;

public class MappingRule
{
    public int Id { get; set; }
    public string ExternalField { get; set; } = string.Empty;
    public string InternalField { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
