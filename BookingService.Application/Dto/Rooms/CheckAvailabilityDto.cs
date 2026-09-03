using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dto.Rooms
{
    public record CheckAvailabilityDto
    (
        DateOnly Date,
        TimeOnly StartTime,
        TimeOnly EndTime,
        int Attendees
    );
}
