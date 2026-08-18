using Microsoft.EntityFrameworkCore;
using RoomService.Application.Interfaces.RoomRates;
using RoomService.Domain.Entities;
using RoomService.Domain.Enums;
using RoomService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Infrastructure.Repository.RoomRates
{
    public class RoomRateCommandRepository : IRoomRateCommandRepository
    {
        private readonly AppDbContext _context;
        public RoomRateCommandRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RoomRate roomRate)
        {
            await _context.RoomRates.AddAsync(roomRate);
        }

        public async Task<bool> HasOverlapAsync(
            int roomId,
            TimeOnly startTime,
            TimeOnly endTime
        )
        {
            return await _context.RoomRates.AnyAsync(r =>
                r.RoomId == roomId &&
                startTime < r.EndTime &&
                endTime > r.StartTime);
        }
    }
}