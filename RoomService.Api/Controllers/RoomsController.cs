using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomService.Application.Dto;
using RoomService.Application.Features.Rooms.Commands;
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
