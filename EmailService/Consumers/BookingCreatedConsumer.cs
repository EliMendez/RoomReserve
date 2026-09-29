using EmailService.Services;
using RoomReserve.Contracts.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Consumers
{
    public class BookingCreatedConsumer
    {
        private readonly IEmailSender _emailSender;

        public BookingCreatedConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        
        public async Task Consume(BookingCreatedEvent message)
        {
            var subject = $"Reserva #{message.BookingId}";

            var body = $"""
                La reserva se ha realizado correctamente.

                Reserva: {message.BookingId}
                Sala: {message.RoomId}
                Fecha: {message.BookingDate}
                Horario: {message.StartTime} - {message.EndTime}
                Personas: {message.NumberOfAttendees}
                Total: ${message.Total}
                """;

            await _emailSender.SendBookingConfirmationAsync(message.Email, subject, body);
        }
    }
}
