using BookingService.Application.Dto.Bookings;
using BookingService.Application.Dto.Rooms;
using BookingService.Application.Interfaces.Repository;
using BookingService.Application.Interfaces.Service;
using BookingService.Application.Interfaces.ServiceClient;
using BookingService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingService.Application.Features.Bookings.CheckAvailability
{
    public class CheckAvailabilityHandle : IRequestHandler<CheckAvailabilityQuery, IEnumerable<AvailableRoomDto>>
    {
        protected readonly IBookingQueryRepository _queryRepository;
        protected readonly IRoomServiceClient _roomServiceClient;
        protected readonly IAvailabilityBookingService _availabilityBookingService;
        public CheckAvailabilityHandle(
            IBookingQueryRepository queryRepository, 
            IRoomServiceClient roomServiceClient,
            IAvailabilityBookingService availabilityBookingService
        ) {
            _queryRepository = queryRepository;
            _roomServiceClient = roomServiceClient;
            _availabilityBookingService = availabilityBookingService;
        }

        public async Task<IEnumerable<AvailableRoomDto>> Handle(CheckAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _roomServiceClient.GetActiveRoomsAsync(request.Attendees);
            
            var unavailableRoomIds = await _queryRepository.GetUnavailabilityAsync(request.Date, request.StartTime, request.EndTime);

            return _availabilityBookingService.GetAvailableRooms(rooms, unavailableRoomIds);
        }
    }
}
