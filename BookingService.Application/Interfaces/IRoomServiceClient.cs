using BookingService.Application.Dto.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces
{
    public interface IRoomServiceClient
    {
        Task<RoomDto?> GetRoomByIdAsync(int roomId);
    }
}
