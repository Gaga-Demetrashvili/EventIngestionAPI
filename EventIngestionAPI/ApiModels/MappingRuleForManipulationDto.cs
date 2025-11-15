namespace EventIngestionAPI.ApiModels;

public record MappingRuleForManipulationDto()
{
    public string? ExternalField { get; set; }
    public string? InternalField { get; set; }
    public int? MappingRuleTypeId { get; set; }
}