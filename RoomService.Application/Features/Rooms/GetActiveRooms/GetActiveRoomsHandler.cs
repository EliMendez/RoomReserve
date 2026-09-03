using MediatR;
using RoomService.Application.Dto.Rooms;
using RoomService.Application.Features.Rooms.GetActiveRooms;
using RoomService.Application.Interfaces.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.Rooms.GetActiveRooms
{
    public class GetActiveRoomsHandler : IRequestHandler<GetActiveRoomsQuery, IEnumerable<RoomDto>>
    {
        private readonly IRoomQueryRepository _roomQueryRepository;
        public GetActiveRoomsHandler(IRoomQueryRepository roomQueryRepository)
        {
            _roomQueryRepository = roomQueryRepository;
        }

        public async Task<IEnumerable<RoomDto>> Handle(GetActiveRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _roomQueryRepository.GetActiveRoomsAsync(request.MinimumCapacity);
            return rooms;
        }
    }
}
