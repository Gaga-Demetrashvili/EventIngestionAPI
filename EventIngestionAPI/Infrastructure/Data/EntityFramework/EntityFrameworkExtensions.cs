using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EventIngestionAPI.Infrastructure.Data.EntityFramework;

public static class EntityFrameworkExtensions
{
    public static void AddSqlServerDatastore(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<MappingRuleContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)));

        services.AddScoped<IMappingRuleStore, MappingRuleContext>();
    }
}
