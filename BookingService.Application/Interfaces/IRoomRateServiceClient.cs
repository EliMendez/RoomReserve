using BookingService.Application.Dto.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces
{
    public interface IRoomRateServiceClient
    {
        Task<IEnumerable<RoomRateDto>> GetRoomRatesAsync(int roomId);
    }
}
