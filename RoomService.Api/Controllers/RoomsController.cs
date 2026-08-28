using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomService.Application.Dto.Rooms;
using RoomService.Application.Features.Rooms.ChangeStatusRoom;
using RoomService.Application.Features.Rooms.CreateRoom;
using RoomService.Application.Features.Rooms.GetActiveRooms;
using RoomService.Application.Features.Rooms.GetRoomById;
using RoomService.Application.Features.Rooms.UpdateRoom;
using RoomService.Domain.Enums;

namespace RoomService.Api.Controllers
{
    [Route("api/rooms")]
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

        [HttpGet("{roomId:int}")]
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
            var command = new CreateRoomCommand(
                Name: roomDto.Name,
                Description: roomDto.Description,
                Capacity: roomDto.Capacity,
                PricePerHour: roomDto.PricePerHour
            );

            var roomId = await _mediator.Send(command);
            return CreatedAtAction(
                nameof(GetRoom),
                new { roomId },
                roomId
            );
        }

        [HttpPut("{roomId:int}")]
        public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] UpdateRoomDto roomDto)
        {
            var command = new UpdateRoomCommand(
                RoomId: roomId,
                Name: roomDto.Name,
                Description: roomDto.Description,
                Capacity: roomDto.Capacity,
                PricePerHour: roomDto.PricePerHour
            );

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{roomId:int}/status")]
        public async Task<IActionResult> ChangeStatusRoom(int roomId)
        {
            await _mediator.Send(new ChangeStatusRoomCommand(roomId));
            return NoContent();
        }
    }
}
