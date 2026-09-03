using BookingService.Application.Dto.Bookings;
using BookingService.Application.Dto.Rooms;
using BookingService.Application.Features.Bookings.CancelBooking;
using BookingService.Application.Features.Bookings.CheckAvailability;
using BookingService.Application.Features.Bookings.CompleteBooking;
using BookingService.Application.Features.Bookings.ConfirmBooking;
using BookingService.Application.Features.Bookings.CreateBooking;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Api.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingsController(IMediator mediator) 
        { 
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvailableRoomDto>>> AvailabilityRoom([FromQuery] CheckAvailabilityDto dto)
        {
            var query = new CheckAvailabilityQuery(
                Date: dto.Date,
                StartTime: dto.StartTime,
                EndTime: dto.EndTime,
                Attendees: dto.Attendees
            );

            var availabilities = await _mediator.Send(query);
            return Ok(availabilities);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBooking([FromBody] CreateBookingDto bookingDto)
        {
            var command = new CreateBookingCommand(
                RoomId: bookingDto.RoomId,
                BookingDate: bookingDto.BookingDate,
                StartTime: bookingDto.StartTime,
                EndTime: bookingDto.EndTime,
                NumberOfAttendees: bookingDto.NumberOfAttendees
            );

            var bookingId = await _mediator.Send(command);
            return Ok(bookingId);
        }

        [HttpPatch("{bookingId:int}/cancel")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            await _mediator.Send(new CancelBookingCommand(bookingId));
            return NoContent();
        }

        [HttpPatch("{bookingId:int}/confirm")]
        public async Task<IActionResult> ConfirmBooking(int bookingId)
        {
            await _mediator.Send(new ConfirmBookingCommand(bookingId));
            return NoContent();
        }

        [HttpPatch("{bookingId:int}/complete")]
        public async Task<IActionResult> CompleteBooking(int bookingId)
        {
            await _mediator.Send(new CompleteBookingCommand(bookingId));
            return NoContent();
        }
    }
}
