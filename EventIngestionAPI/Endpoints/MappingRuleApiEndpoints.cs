using EventIngestionAPI.ApiModels;
using EventIngestionAPI.Entities;
using EventIngestionAPI.Infrastructure.Data;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EventIngestionAPI.Endpoints;

public static class MappingRuleApiEndpoints
{
    public static void RegisterMappingRuleApiEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/mapping-rules", GetMappingRules);
        routeBuilder.MapPost("/mapping-rules", CreateMappingRule);
    }

    internal static async Task<IResult> GetMappingRules([FromServices] IMappingRuleStore mappingRuleStore)
    {
        var mappingRules = await mappingRuleStore.GetAll( trackChanges: false);

        return !mappingRules!.Any()
            ? Results.NotFound("Mapping rules were not found")
            : Results.Ok(mappingRules);
    }

    internal static async Task<IResult> CreateMappingRule([FromServices] IMappingRuleStore mappingRuleStore,
        [FromServices] IValidator<MappingRuleForCreationDto> validator,
        [FromBody] MappingRuleForCreationDto? mappingRuleForCreationDto)
    {
        if (mappingRuleForCreationDto is null)
            return Results.BadRequest("MappingRuleForCreationDto is null");

        var validationResult = validator.Validate(mappingRuleForCreationDto);
        if (!validationResult.IsValid)
            return Results.UnprocessableEntity(validationResult.ToDictionary());

        var mappingRule = new MappingRule()
        {
            ExternalField = mappingRuleForCreationDto.ExternalField!,
            InternalField = mappingRuleForCreationDto.InternalField!,
            MappingRuleTypeId = (int)mappingRuleForCreationDto.MappingRuleTypeId!,
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        await mappingRuleStore.CreateMappingRule(mappingRule);

        return TypedResults.Created(mappingRule.Id.ToString());
    }
}
