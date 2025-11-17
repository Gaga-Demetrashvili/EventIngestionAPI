using RabbitMQ.Client;

namespace EventIngestionAPI.Infrastructure.RabbitMq;

public interface IRabbitMqConnection
{
    IConnection Connection { get; } 
}
