using EventIngestionAPI.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventIngestionAPI.ContextFactory;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<MappingRuleContext>
{
    public MappingRuleContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<MappingRuleContext>()
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new MappingRuleContext(builder.Options);
    }
}
