using EventIngestionAPI.Entities;
using EventIngestionAPI.IntegrationEvents;
using System.Text.Json;

namespace EventIngestionAPI.Infrastructure.Services;

public class EventMapper
{
    private readonly IReadOnlyDictionary<string, string> _mappingRules;
    private readonly IReadOnlyDictionary<string, string> _defaultMappingRules;

    public EventMapper(IEnumerable<MappingRule> mappingRules)
    {
        _mappingRules = mappingRules.ToDictionary
            (mr => mr.ExternalField,
            mr => mr.InternalField,
            StringComparer.OrdinalIgnoreCase
        );
    }

    public InternalEvent Map(JsonElement json)
    {
        var result = new InternalEvent();

        foreach (var prop in json.EnumerateObject())
        {
            string external = prop.Name;

            if (_mappingRules.TryGetValue(external, out string internalName))
            {
                switch (internalName)
                {
                    case nameof(InternalEvent.PlayerId):
                        result.PlayerId = prop.Value.GetString() ?? string.Empty;
                        break;
                    case nameof(InternalEvent.Amount):
                        result.Amount = prop.Value.GetDecimal();
                        break;
                    case nameof(InternalEvent.Currency):
                        result.Currency = prop.Value.GetString() ?? string.Empty;
                        break;
                    case nameof(InternalEvent.OccurredAt):
                        result.OccurredAt = prop.Value.GetDateTime();
                        break;
                }
            }
        }

        return result;
    }
}
