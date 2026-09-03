using MediatR;
using RoomService.Application.Dto.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.Rooms.GetActiveRooms
{
    public record GetActiveRoomsQuery(int? MinimumCapacity = null) : IRequest<IEnumerable<RoomDto>>;
}
