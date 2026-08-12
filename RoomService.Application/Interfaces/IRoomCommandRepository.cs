using RoomService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Interface
{
    public interface IRoomCommandRepository
    {
        Task<bool> ExistsByName(string name);
        Task AddAsync(Room room);
    }
}
