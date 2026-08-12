using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.Rooms.CreateRoom
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la sala es requerido.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción de la sala es requerida.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0.");

            RuleFor(x => x.PricePerHour)
                .GreaterThan(0).WithMessage("El precio por hora debe ser mayor a 0.");
        }
    }
}
