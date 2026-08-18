using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Dto.RoomRates
{
    public record CreateRoomRateDto(
        int RoomId,
        TimeOnly StartTime,
        TimeOnly EndTime,
        decimal PricePerHour
    );
}
