using EventIngestionAPI.Entities;
using EventIngestionAPI.Infrastructure.Data.EntityFramework.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace EventIngestionAPI.Infrastructure.Data.EntityFramework;

public class MappingRuleContext : DbContext, IMappingRuleStore
{

    public MappingRuleContext(DbContextOptions<MappingRuleContext> options): base(options)
    {     
    }

    public DbSet<MappingRule> MappingRules { get; set; }

    public Task CreateMappingRule(MappingRule mappingRule)
    {
        throw new NotImplementedException();
    }

    public Task<MappingRule?> GetAll(bool onlyActive, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMappingRule(MappingRule mappingRule)
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MappingRuleConfiguration());
    }
}
