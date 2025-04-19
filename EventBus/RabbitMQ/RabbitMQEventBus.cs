using EventBus.Abstractions;
using EventBus.Events;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace EventBus.RabbitMQ;
public class RabbitMQEventBus : IEventBus
{
    public async void Publish(IntegrationEvent @event)
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

        await channel.QueueDeclareAsync(queue: eventName, durable: false, exclusive: false, autoDelete: false, arguments: null); 

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: eventName, body: body);
        Console.WriteLine($" [x] Sent {message}");
    }
}
