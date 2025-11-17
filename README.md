# Event Ingestion API

A .NET Web API that receives external event payloads, validates and maps them into an internal event format, and publishes them to RabbitMQ. Field mapping rules are dynamic and can be modified at runtime via API endpoints.

---

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- SQL Server (local or remote)
- RabbitMQ (local or remote)

### Configuration

Update `appsettings.json` with your database and RabbitMQ connection details:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server={YourRemoteOrLocalDBServer};Database=EventIngestionDb;..."
  },
  "RabbitMq": {
    "Hostname": "{YourRemoteOrLocalRabbitMqServer}"
  },
  "RunMigrationsOnStartup": true
}
```

### First Run

When running the project for the first time:
1. Set `RunMigrationsOnStartup` to `true` to automatically apply database migrations
2. Default mapping rules will be seeded into `EventIngestionDb.dbo.MappingRules` table

---

## 📚 API Documentation

Base URL: `https://localhost:7077`

### Mapping Rules Endpoints

#### Get All Mapping Rules
```http
GET /mapping-rules
```
Returns all mapping rules (both default and dynamic).

#### Create Mapping Rule
```http
POST /mapping-rules
```
**Request Body:**
```json
{
  "externalField": "usr",
  "internalField": "PlayerId",
  "mappingRuleTypeId": 1,
  "isActive": true
}
```
**Notes:**
- `mappingRuleTypeId`: `1` = Default, `2` = Dynamic
- `isActive`: Optional (defaults to `true`)

#### Update Mapping Rule
```http
PUT /mapping-rules/{id}
```
**Request Body:**
```json
{
  "externalField": "usr",
  "internalField": "PlayerId",
  "mappingRuleTypeId": 1,
  "isActive": false
}
```
**Notes:**
- `isActive`: Optional (retains current value if not provided)

#### Delete Mapping Rule
```http
DELETE /mapping-rules/{id}
```

---

### Event Ingestion Endpoints

#### Publish Single Event
```http
POST /events
```
**Request Body:**
```json
{
  "usr": "player_123",
  "amt": "25.50",
  "curr": "GEL",
  "ts": "2025-11-05T12:33:47Z"
}
```

Maps an external event to internal format, validates it, and publishes to RabbitMQ.

#### Simulate 100 Events
```http
POST /events/simulate
```
Generates and publishes 100 random events with:
- Random player IDs
- Random amounts (0.00 - 999.99)
- Random currencies (USD, EUR, GBP, JPY, CAD)
- Random timestamps (within last 24 hours)

**Response includes:**
- Total published count
- Total failed count
- First 10 published events (sample)
- First 10 failed events (sample)

---

## 🔧 Implementation Details

### Mapping Strategy
1. **Dynamic rules first**: Attempts to map using dynamic mapping rules
2. **Default rules fallback**: Uses default mapping rules if no dynamic match exists
3. **Validation**: Validates mapped internal event before publishing
4. **Error handling**: Returns detailed error messages for missing mappings or validation failures

### Failure Simulation
- Random publishing failures are simulated in the `PublishAsync` method
- Failure probability can be adjusted via the `FailureProbability` constant (default: 10%)
- Single event endpoint: Returns 503 error if publishing fails
- Simulation endpoint: Collects failed events and reports them in the response

### Production Considerations
In a production environment, failed events should be:
- **Stored in a database** (Dead Letter Queue pattern)
- **Retried by a background worker** (only for infrastructure failures)
- **Separated by failure type**:
  - Validation failures → No retry (permanent errors)
  - Publishing failures → Retry with exponential backoff (transient errors)

---

## 🛠️ Technology Stack
- **.NET 9.0**
- **Entity Framework Core** (database access)
- **RabbitMQ** (message broker)
- **FluentValidation** (input validation)
- **Minimal APIs** (endpoint routing)

---

## 📝 License
This project is licensed under the MIT License.
