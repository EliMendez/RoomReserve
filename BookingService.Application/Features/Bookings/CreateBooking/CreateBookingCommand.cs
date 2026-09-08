using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Features.Bookings.CreateBooking
{
    public record CreateBookingCommand
    (
        int RoomId,
        DateOnly BookingDate,
        TimeOnly StartTime,
        TimeOnly EndTime,
        int NumberOfAttendees,
        string Email
    ): IRequest<int>;
}
