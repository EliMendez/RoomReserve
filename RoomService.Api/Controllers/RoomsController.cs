using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomService.Application.Dto;
using RoomService.Application.Features.Rooms.Commands;
using RoomService.Application.Features.Rooms.GetActiveRooms;
using RoomService.Application.Features.Rooms.GetRoomById;
using RoomService.Domain.Entities;
using System.Xml.Linq;

namespace RoomService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms()
        {
            var rooms = await _mediator.Send(new GetActiveRoomsQuery());
            return Ok(rooms);
        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int roomId)
        {
            var room = await _mediator.Send(new GetRoomByIdQuery(roomId));
            if (room == null)
                return NotFound();

            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateRoom([FromBody] CreateRoomDto roomDto)
        {
            CreateRoomCommand createRoomCommand = new CreateRoomCommand(
                Name: roomDto.Name,
                Description: roomDto.Description,
                Capacity: roomDto.Capacity,
                PricePerHour: roomDto.PricePerHour
            );

            var roomId = await _mediator.Send(createRoomCommand);
            return Ok(roomId);
        }
    }
}
