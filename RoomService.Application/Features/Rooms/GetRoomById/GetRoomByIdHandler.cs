using MediatR;
using RoomService.Application.Dto.Rooms;
using RoomService.Application.Features.Rooms.GetRoomById;
using RoomService.Application.Interfaces.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.Rooms.GetByRoomId
{
    public class GetRoomByIdHandler : IRequestHandler<GetRoomByIdQuery, RoomDto?>
    {
        private readonly IRoomQueryRepository _roomQueryRepository;
        public GetRoomByIdHandler(IRoomQueryRepository roomQueryRepository)
        {
            _roomQueryRepository = roomQueryRepository;
        }

        public async Task<RoomDto?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomQueryRepository.GetByIdAsync(request.RoomId);
            return room;
        }
    }
}
