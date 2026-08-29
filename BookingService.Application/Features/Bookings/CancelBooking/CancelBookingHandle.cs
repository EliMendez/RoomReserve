using AutoMapper;
using BookingService.Application.Interfaces;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Features.Bookings.CancelBooking
{
    public class CancelBookingHandle : IRequestHandler<CancelBookingCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CancelBookingHandle(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(request.BookingId);

            if (booking is null)
                throw new KeyNotFoundException("La reservación de la sala no existe.");

            if (booking.Status != BookingStatus.PENDING.ToString())
                throw new InvalidOperationException("No se puede cancelar la reservación.");

            booking.Status = BookingStatus.CANCELLED.ToString();

            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
