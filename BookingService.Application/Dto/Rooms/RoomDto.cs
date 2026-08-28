using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dto.Rooms
{
    public record RoomDto
    (
        int RoomId,
        int Capacity,
        decimal PricePerHour,
        string Status
    );
}
