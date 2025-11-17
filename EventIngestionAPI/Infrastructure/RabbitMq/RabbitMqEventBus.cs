using EventIngestionAPI.Infrastructure.EventBus;
using EventIngestionAPI.Infrastructure.EventBus.Abstractions;
using System.Text.Json;

namespace EventIngestionAPI.Infrastructure.RabbitMq;

public class RabbitMqEventBus : IEventBus
{
    private readonly IRabbitMqConnection _rabbitMqConnection;

    public RabbitMqEventBus(IRabbitMqConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    public Task PublishAsync(Event @event)
    {
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
