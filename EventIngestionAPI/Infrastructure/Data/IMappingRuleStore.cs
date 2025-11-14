using EventIngestionAPI.Entities;

namespace EventIngestionAPI.Infrastructure.Data;

public interface IMappingRuleStore
{
    Task <MappingRule?> GetById(int id, bool trackChanges);
    Task<IEnumerable<MappingRule>?> GetAll(bool trackChanges);
    Task CreateMappingRule(MappingRule mappingRule);
    Task UpdateMappingRule(MappingRule mappingRule);
}
