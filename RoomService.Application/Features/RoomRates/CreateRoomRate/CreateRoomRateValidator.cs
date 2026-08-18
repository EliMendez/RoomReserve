using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.RoomRates.CreateRoomRate
{
    public class CreateRoomRateValidator: AbstractValidator<CreateRoomRateCommand>
    {
        public CreateRoomRateValidator()
        {
            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("El identificador de la sala debe ser mayor a 0.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("La hora de inicio de la tarifa es requerida.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("La hora de fin de la tarifa es requerida.");

            RuleFor(x => x)
                .Must(x => x.StartTime < x.EndTime)
                .WithMessage("La hora de inicio debe ser menor que la hora de fin.");

            RuleFor(x => x.PricePerHour)
                .GreaterThan(0).WithMessage("El precio por hora debe ser mayor a 0.");
        }
    }
}
