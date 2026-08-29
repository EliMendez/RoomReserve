using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Features.Bookings.CompleteBooking
{
    public class CompleteBookingValidator : AbstractValidator<CompleteBookingCommand>
    {
        public CompleteBookingValidator()
        {
            RuleFor(x => x.BookingId)
                .GreaterThan(0).WithMessage("El id de la reserva debe ser mayor a 0.");
        }
    }
}
