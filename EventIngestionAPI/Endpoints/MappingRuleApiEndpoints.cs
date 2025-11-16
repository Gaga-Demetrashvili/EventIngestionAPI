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
        routeBuilder.MapPut("/mapping-rules/{id:int}", UpdateMappingRule);
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
            IsActive = mappingRuleForCreationDto.IsActive ?? true,
            CreatedAt = DateTime.Now
        };

        await mappingRuleStore.CreateMappingRule(mappingRule);

        return TypedResults.Created(mappingRule.Id.ToString());
    }

    internal static async Task<IResult> UpdateMappingRule(int id,
        [FromServices] IMappingRuleStore mappingRuleStore,
        [FromServices] IValidator<MappingRuleForUpdateDto> validator,
        [FromBody] MappingRuleForUpdateDto? mappingRuleForUpdateDto)
    {
        if (mappingRuleForUpdateDto is null)
            return Results.BadRequest("MappingRuleForUpdateDto is null");

        var validationResult = validator.Validate(mappingRuleForUpdateDto);
        if (!validationResult.IsValid)
            return Results.UnprocessableEntity(validationResult.ToDictionary());

        var mappingRuleEntity = await mappingRuleStore.GetById(id, trackChanges: true);
        if (mappingRuleEntity is null)
            return Results.NotFound($"Mapping rule with id: {id} was not found.");

        mappingRuleEntity.ExternalField = mappingRuleForUpdateDto.ExternalField!;
        mappingRuleEntity.InternalField = mappingRuleForUpdateDto.InternalField!;
        mappingRuleEntity.MappingRuleTypeId = (int)mappingRuleForUpdateDto.MappingRuleTypeId!;
        mappingRuleEntity.IsActive = mappingRuleForUpdateDto.IsActive ?? mappingRuleEntity.IsActive;
        mappingRuleEntity.UpdatedAt = DateTime.Now;

        await mappingRuleStore.UpdateMappingRule(mappingRuleEntity);

        return TypedResults.NoContent();
    }
}
