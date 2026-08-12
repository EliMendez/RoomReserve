using AutoMapper;
using RoomService.Application.Features.Rooms.Commands;
using RoomService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Mapping
{
    public class RoomProfile: Profile
    {
        public RoomProfile()
        {
            CreateMap<CreateRoomCommand, Room>();
        }
    }
}
