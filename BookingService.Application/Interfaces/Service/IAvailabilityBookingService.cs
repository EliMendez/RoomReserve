using BookingService.Application.Dto.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces.Service
{
    public interface IAvailabilityBookingService
    {
        IEnumerable<AvailableRoomDto> GetAvailableRooms(IEnumerable<RoomDto> rooms, IEnumerable<int> unavailableRoomIds);
    }
}
