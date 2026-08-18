using RoomService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Interfaces.RoomRates
{
    public interface IRoomRateCommandRepository
    {
        Task AddAsync(RoomRate roomRate);
        Task<bool> HasOverlapAsync(int roomId, TimeOnly startTime, TimeOnly endTime);
    }
}
