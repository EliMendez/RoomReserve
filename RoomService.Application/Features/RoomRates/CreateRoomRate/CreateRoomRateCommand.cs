using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.RoomRates.CreateRoomRate
{
    public record CreateRoomRateCommand(
        int RoomId,
        TimeOnly StartTime,
        TimeOnly EndTime,
        decimal PricePerHour
    ): IRequest<int>;
}
