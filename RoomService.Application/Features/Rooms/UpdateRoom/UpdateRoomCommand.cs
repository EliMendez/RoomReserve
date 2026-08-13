using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.Rooms.UpdateRoom
{
    public record UpdateRoomCommand
    (
        int RoomId,
        string Name,
        string Description,
        int Capacity,
        decimal PricePerHour
    ): IRequest<bool>;
}
