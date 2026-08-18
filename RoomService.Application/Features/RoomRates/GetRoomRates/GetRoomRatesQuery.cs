using MediatR;
using RoomService.Application.Dto.RoomRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.RoomRates.GetRoomRates
{
    public record GetRoomRatesQuery(
        int RoomId
    ): IRequest<IEnumerable<RoomRateDto>>;
}
