using PaymentService.Application.Interfaces;
using PaymentService.Application.Interfaces.Publisher;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;
using RoomReserve.Contracts.Bookings;
using RoomReserve.Contracts.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Worker.Consumers
{
    public class BookingCreatedConsumer
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IRabbitMqPublisher _messagePublisher;
        public BookingCreatedConsumer(
            IPaymentRepository paymentRepository, 
            IPaymentProcessor paymentProcessor,
            IRabbitMqPublisher messagePublisher
        ) {
            _paymentRepository = paymentRepository;
            _paymentProcessor = paymentProcessor;
            _messagePublisher = messagePublisher;
        }

        public async Task Consume(BookingCreatedEvent message)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("BOOKINGCREATED RECIBIDO");
            Console.WriteLine($"BookingId: {message.BookingId}");
            Console.WriteLine($"Total: {message.Total}");
            Console.WriteLine($"Email: {message.Email}");
            Console.WriteLine("=================================");

            var payment = new Payment
            {
                BookingId = message.BookingId,
                Amount = message.Total,
                Email = message.Email,
                Status = PaymentStatus.PENDING.ToString()
            };

            payment = await _paymentProcessor.ProcessAsync(payment);

            await _paymentRepository.AddAsync(payment);

            Console.WriteLine($"Pago procesado. Estado: {payment.Status}");

            var paymentProcessedEvent = new PaymentProcessedEvent(
                PaymentId: payment.PaymentId,
                BookingId: payment.BookingId,
                Amount: payment.Amount,
                Email: payment.Email,
                Status: payment.Status
            );

            await _messagePublisher.PublishAsync(paymentProcessedEvent);

            Console.WriteLine("PaymentProcessedEvent publicado.");
        }
    }
}
