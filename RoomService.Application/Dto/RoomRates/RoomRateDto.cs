using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Dto.RoomRates
{
    public record RoomRateDto(
        int RoomRateId,
        int RoomId,
        TimeSpan StartTime,
        TimeSpan EndTime,
        decimal PricePerHour
    );
}
