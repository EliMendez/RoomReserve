using MimeKit;
using RoomReserve.Contracts.Bookings;
using MailKit.Net.Smtp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendBookingConfirmationAsync(string email, string subject, string body)
        {
            /*
             El usuario crea una reserva. BookingService guarda la reserva, 
             crea el evento BookingCreatedEvent y lo publica en RabbitMQ 
             indicando el Exchange roomly.events y la routing key BookingCreatedEvent.

             RabbitMQ recibe el mensaje en ese Exchange y revisa los bindings. 
             Busca qué colas están vinculadas a roomly.events mediante esa routing key 
             y coloca el mensaje en las colas correspondientes.

             Cuando inicio EmailService, este consume específicamente la cola email-service. 
             Si existen mensajes pendientes, RabbitMQ se los entrega al consumer. 
             BookingCreatedConsumer procesa cada BookingCreatedEvent 
             y finalmente EmailSender realiza el envío del correo.

             Productor -> "Publica en ESTE Exchange con ESTA Routing Key" 
             -> Exchange -> "¿Qué bindings coinciden?" -> Queue(s)
             -> Consumer -> Procesamiento
             */

            var message = new MimeMessage();

            message.From.Add(
                new MailboxAddress(
                    _configuration["Email:FromName"],
                    _configuration["Email:FromAddress"]!));

            message.To.Add(
                MailboxAddress.Parse(email));

            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            Console.WriteLine($"Evento recibido: {subject}");

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _configuration["Email:SmtpServer"]!,
                int.Parse(_configuration["Email:SmtpPort"]!),
                MailKit.Security.SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _configuration["Email:Username"]!,
                _configuration["Email:Password"]!);

            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }
    }
}
