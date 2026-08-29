using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Features.Bookings.ConfirmBooking
{
    public record ConfirmBookingCommand
    (
        int BookingId
    ): IRequest<Unit>;
}
