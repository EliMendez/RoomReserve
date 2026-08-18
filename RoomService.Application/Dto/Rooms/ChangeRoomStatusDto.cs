using RoomService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Dto.Rooms
{
    public record ChangeRoomStatusDto(
        string Status
    );
}
