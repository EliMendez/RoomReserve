using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.Rooms.ChangeStatusRoom
{
    public class ChangeStatusRoomValidator : AbstractValidator<ChangeStatusRoomCommand>
    {
        public ChangeStatusRoomValidator()
        {
            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("El identificador de la sala debe ser mayor a 0.");
        }
    }
}
