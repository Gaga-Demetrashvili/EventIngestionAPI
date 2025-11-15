using EventIngestionAPI.Entities;
using EventIngestionAPI.Enums;
using EventIngestionAPI.Infrastructure.Data;
using EventIngestionAPI.IntegrationEvents;
using System.Text.Json;

namespace EventIngestionAPI.Infrastructure.Services;

public class EventMapper(IServiceScopeFactory scopeFactory) : IEventMapper
{
    private IReadOnlyDictionary<string, string> _dynamicMappingRules;
    private IReadOnlyDictionary<string, string> _defaultMappingRules;
    private readonly SemaphoreSlim _lock = new(1, 1);
    private bool _initialized = false;
    private DateTime _lastLoad = DateTime.MinValue;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    private async Task EnsureInitialized()
    {
        if (_initialized && DateTime.Now - _lastLoad < _cacheDuration)
            return;

        await _lock.WaitAsync();
        try
        {
            if (_initialized && DateTime.Now - _lastLoad < _cacheDuration)
                return;

            using var scope = scopeFactory.CreateScope();
            var mappingRuleStore = scope.ServiceProvider.GetRequiredService<IMappingRuleStore>();
            var mappingRules = await mappingRuleStore.GetAll(trackChanges: false) ?? Enumerable.Empty<MappingRule>();

            _dynamicMappingRules = mappingRules
                .Where(mr => mr.MappingRuleTypeId == (int)MappingRuleTypeEnum.Dynamic)
                .ToDictionary
                (mr => mr.ExternalField,
                mr => mr.InternalField,
                StringComparer.OrdinalIgnoreCase
            );

            _defaultMappingRules = mappingRules
                .Where(mr => mr.MappingRuleTypeId == (int)MappingRuleTypeEnum.Default)
                .ToDictionary
                (mr => mr.ExternalField,
                mr => mr.InternalField,
                StringComparer.OrdinalIgnoreCase
            );

            _initialized = true;
            _lastLoad = DateTime.Now;
        }
        finally
        {
            _lock.Release();
        }
    }

    private static readonly Dictionary<string, Action<InternalEvent, JsonElement>> _setters =
    new(StringComparer.OrdinalIgnoreCase)
    {
        [nameof(InternalEvent.PlayerId)] = (ie, je) => ie.PlayerId = je.GetString() ?? string.Empty,
        [nameof(InternalEvent.Amount)] = (ie, je) => ie.Amount = je.GetDecimal(),
        [nameof(InternalEvent.Currency)] = (ie, je) => ie.Currency = je.GetString() ?? string.Empty,
        [nameof(InternalEvent.OccurredAt)] = (ie, je) => ie.OccurredAt = je.GetDateTime(),
    };

    public async Task<InternalEvent> Map(JsonElement json)
    {
        await EnsureInitialized();
        var result = new InternalEvent();

        foreach (var prop in json.EnumerateObject())
        {
            string external = prop.Name;

            if (!_dynamicMappingRules.TryGetValue(external, out string? internalName))
                _defaultMappingRules.TryGetValue(external, out internalName);

            if (internalName is null)
                continue;

            if (_setters.TryGetValue(internalName, out var setter))
                setter(result, prop.Value);
        }

        return result;
    }
}
