using AutoMapper;
using RoomService.Application.Features.RoomRates.CreateRoomRate;
using RoomService.Application.Features.Rooms.CreateRoom;
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
            CreateMap<CreateRoomRateCommand, RoomRate>();
        }
    }
}
