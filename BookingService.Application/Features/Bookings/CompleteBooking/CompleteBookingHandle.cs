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

namespace BookingService.Application.Features.Bookings.CompleteBooking
{
    public class CompleteBookingHandle : IRequestHandler<CompleteBookingCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompleteBookingHandle(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CompleteBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(request.BookingId);

            if (booking is null)
                throw new KeyNotFoundException("La reservación de la sala no existe.");

            if (booking.Status != BookingStatus.CONFIRMED.ToString())
                throw new InvalidOperationException("No se puede completar la reservación.");

            booking.Status = BookingStatus.COMPLETED.ToString();

            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
