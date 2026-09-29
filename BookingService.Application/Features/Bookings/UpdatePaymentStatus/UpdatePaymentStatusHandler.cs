using BookingService.Application.Interfaces;
using BookingService.Application.Interfaces.Repository;
using BookingService.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Features.Bookings.UpdatePaymentStatus
{
    public class UpdatePaymentStatusHandler : IRequestHandler<UpdatePaymentStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePaymentStatusHandler(
            IUnitOfWork unitOfWork
        ) {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(request.BookingId);

            if (booking == null)
                throw new KeyNotFoundException("La reservación de la sala no existe.");

            booking.PaymentStatus = request.PaymentStatus;

            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
