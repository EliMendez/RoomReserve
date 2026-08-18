using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomService.Application.Interfaces.RoomRates;
using RoomService.Application.Interfaces.Rooms;

namespace RoomService.Application.Interface
{
    public interface IUnitOfWork
    {
        IRoomCommandRepository Rooms { get; }
        IRoomRateCommandRepository RoomRates { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
