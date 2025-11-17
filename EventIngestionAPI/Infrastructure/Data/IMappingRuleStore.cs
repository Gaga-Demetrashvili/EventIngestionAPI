using EventIngestionAPI.Entities;
using System.Linq.Expressions;

namespace EventIngestionAPI.Infrastructure.Data;

public interface IMappingRuleStore
{
    Task <MappingRule?> GetById(int id, bool trackChanges);
    Task<IEnumerable<MappingRule>?> GetAll(bool trackChanges);
    Task<IEnumerable<MappingRule>?> GetByCondition(Expression<Func<MappingRule, bool>> expression, bool trackChanges);
    Task CreateMappingRule(MappingRule mappingRule);
    Task UpdateMappingRule(MappingRule mappingRule);
    Task DeleteMappingRule(MappingRule mappingRule);
}
