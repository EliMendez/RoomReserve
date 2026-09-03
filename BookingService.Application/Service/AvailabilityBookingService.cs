using BookingService.Application.Dto.Rooms;
using BookingService.Application.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Service
{
    public class AvailabilityBookingService : IAvailabilityBookingService
    {
        public IEnumerable<AvailableRoomDto> GetAvailableRooms(IEnumerable<RoomDto> rooms, IEnumerable<int> unavailableRoomIds)
        {
            return rooms
                .Where(room => !unavailableRoomIds.Contains(room.RoomId))
                .Select(room => new AvailableRoomDto(
                    RoomId: room.RoomId,
                    Name: room.Name,
                    Capacity: room.Capacity
                ));
        }
    }
}
