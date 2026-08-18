using FluentValidation;
using RoomService.Application.Features.RoomRates.GetRoomRates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.RoomRates.GetRoomRates
{
    public class GetRoomRatesValidator: AbstractValidator<GetRoomRatesQuery>
    {
        public GetRoomRatesValidator()
        {
            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("El identificador de la sala debe ser mayor a 0.");
        }
    }
}
