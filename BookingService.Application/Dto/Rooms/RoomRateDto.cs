using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dto.Rooms
{
    public record RoomRateDto
    (
        int RoomRateId,
        int RoomId,
        TimeOnly StartTime,
        TimeOnly EndTime,
        decimal PricePerHour
    );
}
