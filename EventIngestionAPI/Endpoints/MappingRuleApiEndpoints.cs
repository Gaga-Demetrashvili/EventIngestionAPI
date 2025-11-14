using EventIngestionAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EventIngestionAPI.Endpoints;

public static class MappingRuleApiEndpoints
{
    public static void RegisterMappingRuleApiEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/mapping-rules", GetMappingRules);
    }

    internal static async Task<IResult> GetMappingRules([FromServices] IMappingRuleStore mappingRuleStore)
    {
        var mappingRules = await mappingRuleStore.GetAll( trackChanges: false);

        return !mappingRules!.Any()
            ? Results.NotFound("Mapping rules were not found")
            : Results.Ok(mappingRules);
    }
}
