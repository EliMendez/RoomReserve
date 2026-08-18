using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.Rooms.ChangeStatusRoom
{
    public record ChangeStatusRoomCommand
    (
        int RoomId
    ): IRequest<bool>;
}
