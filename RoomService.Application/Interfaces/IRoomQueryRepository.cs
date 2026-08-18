using RoomService.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Interfaces
{
    public interface IRoomQueryRepository
    {
        Task<IEnumerable<RoomDto>> GetActiveRoomsAsync();
        Task<RoomDto?> GetByIdAsync(int roomId);
        Task<bool> ExistsById(int roomId);
    }
}
