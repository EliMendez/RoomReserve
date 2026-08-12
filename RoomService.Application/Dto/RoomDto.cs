using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Dto
{
    public record RoomDto
    (
        int RoomId,
        string Name,
        string Description,
        int Capacity,
        decimal PricePerHour,
        string Status
    );
}
