using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomService.Application.Dto.RoomRates;
using RoomService.Application.Features.RoomRates.CreateRoomRate;
using RoomService.Application.Features.RoomRates.GetRoomRates;

namespace RoomService.Api.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomRatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoomRatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{roomId:int}/rates")]
        public async Task<ActionResult<IEnumerable<RoomRateDto>>> GetRoomRates(int roomId)
        {
            var roomRates = await _mediator.Send(new GetRoomRatesQuery(roomId));
            return Ok(roomRates);
        }

        [HttpPost("{roomId:int}/rates")]
        public async Task<ActionResult<int>> CreateRoomRate(int roomId, [FromBody] CreateRoomRateDto roomRateDto)
        {
            var command = new CreateRoomRateCommand(
                RoomId: roomId,
                StartTime: roomRateDto.StartTime,
                EndTime: roomRateDto.EndTime,
                PricePerHour: roomRateDto.PricePerHour
            );

            var roomRateId = await _mediator.Send(command);
            return Ok(roomRateId);
        }
    }
}
