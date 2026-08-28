using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Features.Bookings.CreateBooking
{
    public class CreateBookingValidator: AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("El id de la sala debe ser mayor a 0.");
                
            RuleFor(x => x.BookingDate)
                .NotEqual(default(DateOnly)).WithMessage("La fecha de reserva es requerida.");

            RuleFor(x => x.StartTime)
                .NotEqual(default(TimeOnly)).WithMessage("La hora de inicio es requerida.");

            RuleFor(x => x.EndTime)
                .NotEqual(default(TimeOnly)).WithMessage("La hora de fin es requerida.");

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("La hora de fin debe ser posterior a la hora de inicio.");

            RuleFor(x => x.NumberOfAttendees)
                .GreaterThan(0).WithMessage("El número de asistentes debe ser mayor a 0.");
        }
    }
}
