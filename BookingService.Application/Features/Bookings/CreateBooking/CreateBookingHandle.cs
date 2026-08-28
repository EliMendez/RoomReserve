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

namespace BookingService.Application.Features.Bookings.CreateBooking
{
    public class CreateBookingHandle : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomServiceClient _roomServiceClient;
        private readonly IRoomRateServiceClient _roomRateServiceClient;
        private readonly IMapper _mapper;
        public CreateBookingHandle(
            IUnitOfWork unitOfWork,
            IRoomServiceClient roomServiceClient,
            IRoomRateServiceClient roomRateServiceClient,
            IMapper mapper
        ) { 
            _unitOfWork = unitOfWork;
            _roomServiceClient = roomServiceClient;
            _roomRateServiceClient = roomRateServiceClient;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomServiceClient.GetRoomByIdAsync(request.RoomId);

            if (room is null)
                throw new KeyNotFoundException("La sala no existe.");

            if (room.Status != "ACTIVE")
                throw new InvalidOperationException("La sala está inactiva.");
            
            if(request.NumberOfAttendees > room.Capacity)
                throw new InvalidOperationException("La cantidad de asistentes no debe superar la capacidad de la sala.");

            var hasOverlap = await _unitOfWork.Bookings.HasOverlapAsync(request.RoomId, request.BookingDate, request.StartTime, request.EndTime);
            if (hasOverlap)
                throw new InvalidOperationException("El horario de la reserva esta superpuesto con otra para la sala.");

            var rates = await _roomRateServiceClient.GetRoomRatesAsync(request.RoomId);

            decimal subtotal = 0;
            var totalDuration = request.EndTime - request.StartTime;
            var durationHours = (decimal)totalDuration.TotalHours;

            foreach (var rate in rates)
            {
                var startTime = request.StartTime > rate.StartTime
                    ? request.StartTime
                    : rate.StartTime;

                var endTime = request.EndTime < rate.EndTime
                    ? request.EndTime
                    : rate.EndTime;

                if (startTime < endTime)
                {
                    var duration = endTime - startTime;
                    subtotal += (decimal)duration.TotalHours * rate.PricePerHour;
                }
            }

            var booking = _mapper.Map<Booking>(request);

            booking.Duration = durationHours;
            booking.Subtotal = subtotal;
            booking.Total = subtotal;
            booking.Status = BookingStatus.PENDING.ToString();

            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return booking.BookingId;
        }
    }
}
