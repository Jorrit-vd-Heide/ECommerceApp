using EventBus.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EventBus.RabbitMQ;

public class RabbitMQEventBus
{
    public async Task Publish(IntegrationEvent @event)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        var eventName = @event.GetType().Name;

        await channel.QueueDeclareAsync(queue: eventName, durable: false, exclusive: false, autoDelete: false);

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: "", routingKey: eventName, body: body);

        Console.WriteLine($"[x] Event gepubliceerd: {eventName}");
    }
}