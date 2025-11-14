namespace EventIngestionAPI.Entities;

public class MappingRule
{
    public int Id { get; set; }
    public required string ExternalField { get; set; } = string.Empty;
    public required string InternalField { get; set; } = string.Empty;
    public required bool IsActive { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required int MappingRuleTypeId { get; set; }
    public MappingRuleType? MappingRuleType { get; set; }
}
