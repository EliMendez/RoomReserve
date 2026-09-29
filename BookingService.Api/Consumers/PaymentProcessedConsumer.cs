using BookingService.Application.Features.Bookings.UpdatePaymentStatus;
using MediatR;
using RoomReserve.Contracts.Payments;

namespace BookingService.Api.Consumers
{
    public class PaymentProcessedConsumer
    {
        private readonly IMediator _mediator;
        public PaymentProcessedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(PaymentProcessedEvent message)
        {

            var command = new UpdatePaymentStatusCommand(
                BookingId: message.BookingId,
                PaymentStatus: message.Status
            );

            await _mediator.Send(command);
        }
    }
}
