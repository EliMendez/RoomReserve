using BookingService.Application.Dto.Rooms;
using BookingService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Features.Bookings.CheckAvailability
{
    public record CheckAvailabilityQuery
    (
        DateOnly Date, TimeOnly StartTime, TimeOnly EndTime, int Attendees
    ) : IRequest<IEnumerable<AvailableRoomDto>>;
}
