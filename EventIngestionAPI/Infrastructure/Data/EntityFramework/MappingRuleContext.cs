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
    public DbSet<MappingRuleType> MappingRuleTypes { get; set; }

    public async Task<MappingRule?> GetById(int id, bool trackChanges) =>
        !trackChanges
        ? await MappingRules.AsNoTracking().FirstOrDefaultAsync(mr => mr.Id == id)
        : await MappingRules.FirstOrDefaultAsync(mr => mr.Id == id);

    public Task CreateMappingRule(MappingRule mappingRule)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MappingRule>?> GetAll(bool trackChanges) =>
        !trackChanges 
        ? await MappingRules.AsNoTracking().ToListAsync()
        : await MappingRules.ToListAsync();

    public Task UpdateMappingRule(MappingRule mappingRule)
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MappingRuleConfiguration());
        modelBuilder.ApplyConfiguration(new MappingRuleTypeConfiguration());
    }
}
