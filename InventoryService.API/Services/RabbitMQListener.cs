using EventBus.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace InventoryService.API.Services
{
    public class RabbitMQListener
    {
        public async Task StartListeningAsync()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            var queueName = "ProductCreatedEvent";

            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var integrationEvent = JsonSerializer.Deserialize<ProductCreatedIntegrationEvent>(message);

                await Task.Yield();
            };

            await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}
