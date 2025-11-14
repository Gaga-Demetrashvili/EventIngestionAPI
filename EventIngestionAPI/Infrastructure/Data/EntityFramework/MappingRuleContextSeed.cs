using Microsoft.EntityFrameworkCore;

namespace EventIngestionAPI.Infrastructure.Data.EntityFramework;

internal static class MappingRuleContextSeed
{
    public static void MigrateDatabase(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var mappingRuleContext = scope.ServiceProvider.GetRequiredService<MappingRuleContext>();

        mappingRuleContext.Database.Migrate();
    }
}
