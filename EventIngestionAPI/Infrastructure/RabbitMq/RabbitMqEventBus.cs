using EventIngestionAPI.Infrastructure.EventBus;
using EventIngestionAPI.Infrastructure.EventBus.Abstractions;
using System.Text.Json;

namespace EventIngestionAPI.Infrastructure.RabbitMq;

public class RabbitMqEventBus : IEventBus
{
    private readonly IRabbitMqConnection _rabbitMqConnection;
    private static readonly Random _random = new Random();
    private const double FailureProbability = 0.2; // 20% chance of failure

    public RabbitMqEventBus(IRabbitMqConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    public Task PublishAsync(Event @event)
    {
        if (_random.NextDouble() < FailureProbability)
            throw new InvalidOperationException("Simulated publishing failure: Unable to connect to message broker");

        var routingKey = @event.GetType().Name;

        using var channel = _rabbitMqConnection.Connection.CreateModel();

        channel.QueueDeclare(
            queue: routingKey,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType());

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: null,
            body: body);

        return Task.CompletedTask;
    }
}
