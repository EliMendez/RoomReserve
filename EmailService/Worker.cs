using System.Text;
using System.Text.Json;
using EmailService.Consumers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RoomReserve.Contracts.Bookings;

namespace EmailService
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly BookingCreatedConsumer _consumer;

        public Worker(
            IConfiguration configuration,
            BookingCreatedConsumer consumer)
        {
            _configuration = configuration;
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"]!,
                Port = int.Parse(_configuration["RabbitMQ:Port"]!),
                UserName = _configuration["RabbitMQ:UserName"]!,
                Password = _configuration["RabbitMQ:Password"]!
            };

            await using var connection =
                await factory.CreateConnectionAsync();

            await using var channel =
                await connection.CreateChannelAsync();

            // 1. Exchange
            await channel.ExchangeDeclareAsync(
                exchange: "roomly.events",
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false);

            // 2. Queue
            await channel.QueueDeclareAsync(
                queue: "email-service",
                durable: true,
                exclusive: false,
                autoDelete: false);

            // 3. Binding
            await channel.QueueBindAsync(
                queue: "email-service",
                exchange: "roomly.events",
                routingKey: "BookingCreatedEvent");

            // 4. Consumer
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, args) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(
                        args.Body.ToArray());

                    var message =
                        JsonSerializer.Deserialize<BookingCreatedEvent>(
                            json);

                    if (message is null)
                        return;

                    await _consumer.Consume(message);

                    await channel.BasicAckAsync(
                        deliveryTag: args.DeliveryTag,
                        multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Error procesando mensaje: {ex.Message}");

                    await channel.BasicNackAsync(
                        deliveryTag: args.DeliveryTag,
                        multiple: false,
                        requeue: true);
                }
            };

            // 5. Empezar a escuchar
            await channel.BasicConsumeAsync(
                queue: "email-service",
                autoAck: false,
                consumer: consumer);

            Console.WriteLine("EmailService escuchando RabbitMQ...");
            Console.WriteLine("Queue: email-service");

            await Task.Delay(
                Timeout.Infinite,
                stoppingToken);
        }
    }
}
