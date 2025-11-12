using Microsoft.EntityFrameworkCore;

namespace EventIngestionAPI.Infrastructure.Data.EntityFramework;

public static class EntityFrameworkExtensions
{
    public static void AddSqlServerDatastore(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<MappingRuleContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IMappingRuleStore, MappingRuleContext>();
    }
}
