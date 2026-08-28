using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dto.Bookings
{
    public record CreateBookingDto
    (
        int RoomId,
        DateOnly BookingDate,
        TimeOnly StartTime,
        TimeOnly EndTime,
        int NumberOfAttendees
    );
}
