using Microsoft.EntityFrameworkCore;
using RoomService.Application.Interface;
using RoomService.Domain.Entities;
using RoomService.Domain.Enums;
using RoomService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Infrastructure.Repository.Rooms
{
    public class RoomCommandRepository : IRoomCommandRepository
    {
        private readonly AppDbContext _context;
        public RoomCommandRepository(AppDbContext context)
        {
            _context = context;   
        }

        public async Task<bool> ExistsByName(string name)
        {
            name = name.Trim();
            return await _context.Rooms.AnyAsync(r => r.Name == name);
        }

        public async Task AddAsync(Room room)
        {
            room.Status = RoomStatus.ACTIVE.ToString();
            await _context.Rooms.AddAsync(room);
        }
    }
}
