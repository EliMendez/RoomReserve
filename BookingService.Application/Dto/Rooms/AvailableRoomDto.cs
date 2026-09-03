using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dto.Rooms
{
    public record AvailableRoomDto
    (
        int RoomId,
        string Name,
        int Capacity
    );
}
