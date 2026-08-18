using RoomService.Application.Dto.RoomRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Interfaces
{
    public interface IRoomRateQueryRepository
    {
        Task<IEnumerable<RoomRateDto>> GetAllAsync(int roomId);
    }
}
