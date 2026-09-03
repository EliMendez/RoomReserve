using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Features.Bookings.CheckAvailability
{
    public class CheckAvailabilityValidator: AbstractValidator<CheckAvailabilityQuery>
    {
        public CheckAvailabilityValidator()
        {
            RuleFor(x => x.Date)
                .NotEqual(default(DateOnly)).WithMessage("La fecha es requerida.");

            RuleFor(x => x.StartTime)
                .NotEqual(default(TimeOnly)).WithMessage("La hora de inicio es requerida.");

            RuleFor(x => x.EndTime)
                .NotEqual(default(TimeOnly)).WithMessage("La hora de fin es requerida.");

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("La hora de fin debe ser posterior a la hora de inicio.");

            RuleFor(x => x.Attendees)
                .GreaterThan(0).WithMessage("La capacidad minima debe ser mayor a 0.");
        }
    }
}
