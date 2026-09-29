using PaymentService.Worker.Consumers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RoomReserve.Contracts.Bookings;
using System;
using System.Text;
using System.Text.Json;

namespace PaymentService.Worker
{ 
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(
            IConfiguration configuration,
            IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            // ============================================================
            // 1. CONEXIÓN A RABBITMQ
            // ============================================================
            // Aquí configuramos cómo conectarnos a RabbitMQ:
            // host, puerto, usuario y contraseña.
            //
            // Solo estamos preparando la conexión.
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"]!,
                Port = int.Parse(_configuration["RabbitMQ:Port"]!),
                UserName = _configuration["RabbitMQ:UserName"]!,
                Password = _configuration["RabbitMQ:Password"]!
            };

            // Creamos la conexión con RabbitMQ.
            await using var connection =
                await factory.CreateConnectionAsync();

            // Creamos un CHANNEL para trabajar con RabbitMQ.
            // channel   = canal por donde hacemos operaciones
            await using var channel =
                await connection.CreateChannelAsync();


            // ============================================================
            // 2. EXCHANGE
            // ============================================================
            // Declaramos/preparamos nuestro Exchange.
            //
            // Exchange = recibe los mensajes del Producer
            // y decide a qué Queue(s) deben ir.
            //
            // En nuestro caso:
            // Exchange = "roomly.events"
            // Tipo     = Topic
            await channel.ExchangeDeclareAsync(
                exchange: "roomly.events",
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false);


            // ============================================================
            // 3. QUEUE
            // ============================================================
            // Declaramos/preparamos nuestra Queue.
            //
            // Queue = donde los mensajes quedan esperando
            //         hasta que un Consumer los procese.
            //
            // Esta Queue pertenece a PaymentService.
            await channel.QueueDeclareAsync(
                queue: "payment-service",
                durable: true,
                exclusive: false,
                autoDelete: false);


            // ============================================================
            // 4. BINDING
            // ============================================================
            // Aquí conectamos Exchange + Queue mediante una routing key.
            //
            // Estamos diciendo:
            //
            // "Cuando llegue a roomly.events un mensaje cuya
            // routing key sea BookingCreatedEvent,
            // puede enviarse a payment-service."
            await channel.QueueBindAsync(
                queue: "payment-service",
                exchange: "roomly.events",
                routingKey: "BookingCreatedEvent");


            // ============================================================
            // 5. CONSUMER
            // ============================================================
            // Creamos el Consumer.
            //
            // Consumer = el encargado de recibir/leer los mensajes
            // que están en nuestra Queue.
            var consumer =
                new AsyncEventingBasicConsumer(channel);


            // ============================================================
            // 6. ¿QUÉ HACEMOS CUANDO LLEGA UN MENSAJE?
            // ============================================================
            // Este código se ejecuta cada vez que RabbitMQ
            // entrega un mensaje al Consumer.
            consumer.ReceivedAsync += async (sender, args) =>
            {
                try
                {
                    // ----------------------------------------------------
                    // 6.1 LEEMOS EL MENSAJE
                    // ----------------------------------------------------
                    // RabbitMQ nos entrega el contenido como bytes.
                    // Lo convertimos a texto JSON.
                    var json = Encoding.UTF8.GetString(
                        args.Body.ToArray());


                    // ----------------------------------------------------
                    // 6.2 DESERIALIZAMOS
                    // ----------------------------------------------------
                    // Convertimos el JSON en nuestro objeto C#.
                    //
                    // JSON -> BookingCreatedEvent
                    var message =
                        JsonSerializer.Deserialize<BookingCreatedEvent>(
                            json);

                    if (message is null)
                        return;


                    // ----------------------------------------------------
                    // 6.3 PROCESAMOS EL EVENTO
                    // ----------------------------------------------------
                    // Aquí ocurre la lógica de negocio.
                    //
                    // Por ejemplo:
                    // BookingCreatedEvent -> procesar pago
                    // Crear un scope para los servicios Scoped
                    using var scope = _scopeFactory.CreateScope();

                    var bookingConsumer =
                        scope.ServiceProvider
                            .GetRequiredService<BookingCreatedConsumer>();
                    await bookingConsumer.Consume(message);


                    // ----------------------------------------------------
                    // 6.4 ACK = TODO SALIÓ BIEN
                    // ----------------------------------------------------
                    // Le decimos a RabbitMQ:
                    //
                    // "Ya procesé correctamente este mensaje.
                    // Puedes considerarlo terminado."
                    //
                    // ACK = mensaje procesado correctamente
                    await channel.BasicAckAsync(
                        deliveryTag: args.DeliveryTag,
                        multiple: false);
                }
                catch (Exception ex)
                {
                    // ----------------------------------------------------
                    // 6.5 NACK = ALGO FALLÓ
                    // ----------------------------------------------------
                    // Si ocurre un error procesando el mensaje:
                    //
                    // NACK = "No pude procesarlo"
                    //
                    // requeue: true
                    // = vuelve a poner el mensaje en la Queue
                    //   para intentar procesarlo nuevamente.
                    Console.WriteLine(
                        $"Error procesando pago: {ex.Message}");

                    await channel.BasicNackAsync(
                        deliveryTag: args.DeliveryTag,
                        multiple: false,
                        requeue: true);
                }
            };


            // ============================================================
            // 7. EMPEZAMOS A ESCUCHAR LA QUEUE
            // ============================================================
            // Le decimos a RabbitMQ:
            //
            // "Este Consumer va a escuchar payment-service."
            //
            // autoAck: false
            // = NO hagas ACK automáticamente.
            // Nosotros hacemos ACK manualmente cuando terminemos
            // de procesar correctamente el mensaje.
            await channel.BasicConsumeAsync(
                queue: "payment-service",
                autoAck: false,
                consumer: consumer);


            // Mensajes informativos para saber que el servicio arrancó.
            Console.WriteLine(
                "PaymentService escuchando RabbitMQ...");

            Console.WriteLine(
                "Queue: payment-service");


            // ============================================================
            // 8. MANTENER EL WORKER VIVO
            // ============================================================
            // El Worker es un servicio que debe quedarse funcionando.
            //
            // Queremos que permanezca aquí:
            //
            // "Esperando mensajes..."
            //
            // Si llega un mensaje -> el Consumer lo procesa.
            await Task.Delay(
                Timeout.Infinite,
                stoppingToken);
        }
    }
}
