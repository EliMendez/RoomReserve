using Microsoft.Extensions.Configuration;
using PaymentService.Application.Interfaces.Publisher;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Messaging
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IConfiguration _configuration;

        public RabbitMqPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task PublishAsync<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"]!,
                Port = int.Parse(_configuration["RabbitMQ:Port"]!),
                UserName = _configuration["RabbitMQ:UserName"]!,
                Password = _configuration["RabbitMQ:Password"]!
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: "roomly.events",
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false);

            var eventName = message!.GetType().Name;

            var body = JsonSerializer.SerializeToUtf8Bytes(message);

            var properties = new BasicProperties
            {
                Persistent = true,
                ContentType = "application/json",
                Type = eventName
            };

            /*
                await channel.BasicPublishAsync(
                exchange: "roomly.events",
                routingKey: "BookingCreatedEvent"
            */
            await channel.BasicPublishAsync(
                exchange: "roomly.events",
                routingKey: eventName,
                mandatory: false,
                basicProperties: properties,
                body: body);
        }
    }
}
