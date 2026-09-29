using BookingService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Features.Bookings.UpdatePaymentStatus
{
    public record UpdatePaymentStatusCommand
    (
        int BookingId,
        string PaymentStatus
    ): IRequest;
}
