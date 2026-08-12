using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Interface
{
    public interface IUnitOfWork
    {
        IRoomCommandRepository Rooms { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
