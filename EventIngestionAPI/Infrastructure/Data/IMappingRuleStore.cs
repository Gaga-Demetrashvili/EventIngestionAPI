using EventIngestionAPI.Entities;

namespace EventIngestionAPI.Infrastructure.Data;

public interface IMappingRuleStore
{
    Task<MappingRule?> GetAll(bool onlyActive, bool trackChanges);
    Task CreateMappingRule(MappingRule mappingRule);
    Task UpdateMappingRule(MappingRule mappingRule);
}
