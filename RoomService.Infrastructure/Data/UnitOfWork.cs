using RoomService.Application.Interface;
using RoomService.Application.Interfaces.RoomRates;
using RoomService.Application.Interfaces.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRoomCommandRepository Rooms {  get; }
        public IRoomRateCommandRepository RoomRates { get; }

        public UnitOfWork(
            AppDbContext context, 
            IRoomCommandRepository roomRepository,
            IRoomRateCommandRepository roomRateRepository
        ) {
            _context = context;
            Rooms = roomRepository;
            RoomRates = roomRateRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
