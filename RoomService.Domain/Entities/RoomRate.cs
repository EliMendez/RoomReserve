using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Domain.Entities
{
    public class RoomRate
    {
        public int RoomRateId { get; set; }
        public int RoomId { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal PricePerHour { get; set; }

        public Room Room { get; set; } = null!;
    }
}
