namespace EventIngestionAPI.ApiModels;

public record CreateMappingRuleRequest(string ExternalField, string InternalField, int MappingRuleTypeId);