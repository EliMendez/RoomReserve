using RoomService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Interfaces.Rooms
{
    public interface IRoomCommandRepository
    {
        Task<bool> ExistsByName(string name, int? roomId = null);
        Task<bool> ExistsById(int roomId);
        Task<Room?> GetByIdAsync(int roomId);
        Task AddAsync(Room room);
        void Update(Room room);
    }
}
