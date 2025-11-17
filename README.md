# OnAim---Integration-Engineer-Task

Integration Engineer Task for OnAim — A .NET Web API that receives external event payloads, validates and maps them into OnAim’s internal event format, and publishes them. Field mapping rules are dynamic and can be modified at runtime via API endpoints.

API Documentation

# How to run project

"ConnectionStrings": {
"DefaultConnection": "Server={YourRemoteOrLocalDBServer}";
}

"RabbitMq": {
"Hostname": "{YourRemoteOrLocalRabbitMqServer}"
}

In order initial db migrations to be applied on the start of API
"RunMigrationsOnStartup": true

When running project for the first time migration seeds default MappingRules data in the EventIngestionDb.dbo.MappingRules table

# How to use API

## API Endpoints for MappingRules CRUD operations

GET - https://localhost:7077/mapping-rules - Gets All mapping rules (dynamic and default)

POST - https://localhost:7077/mapping-rules - Creates Mapping Rule
Reques body:
{
"externalField": "usr",
"internalField": "PlayerId",
"mappingRuleTypeId": 1/2 (1 - default, 2 - dynamic)
"IsActive": true/false (Optional property. If not passed it's defaulted to true)
}

UPDATE - https://localhost:7077/mapping-rules/id - Updates Mapping Rule based on id
Reques body:
{
"externalField": "usr",
"internalField": "PlayerId",
"mappingRuleTypeId": 1/2 (1 - default, 2 - dynamic)
"IsActive": true/false (Optional property. If not passed it's defaulted to current value)
}

DELETE - https://localhost:7077/mapping-rules/id - Deletes Mapping Rule based on id

## API Endpoints for EventIngestion

My approach on mapping externalFields to InternalFields is to at first try to map them with dynamic mapping rules, if there
is no specific externalField mapping than to use default mapping rules. After that I am doing validation.
If mapping rule was not found or required externalField value is empty, I return respective error message.

There is a simple publishing simulation failure functionality added in PublishAsync method.
Chance of failure can be adjusted from there by manipulating "FailureProbability" property.
Because of the Tech Task requirements and to not go into overengineering, in case one event publishing endpoint call
if publish is failed only failure respones is returned.
In case 100 event publishing endpoint, failed events are saved in list and then 10 of them are printed just to showcase as well
as 10 successfully published events.
Real production environment approach would be to save failed events in db table and have worker which would try sending that
events again, but only the ones that failed on publishing step and not on validation step.

POST - https://localhost:7077/events - Maps external event to internal event and publishes it
Reques body:
{
"usr": "player_123",
"amt": "25.50",
"curr": "GEL",
"ts": "2025-11-05T12:33:47Z"
}

POST - https://localhost:7077/events/simulate - simulates creation of 100 events and publishes them.
No Request body is needed
