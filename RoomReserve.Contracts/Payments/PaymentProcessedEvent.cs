using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReserve.Contracts.Payments
{
    public record PaymentProcessedEvent(
        int PaymentId,
        int BookingId,
        decimal Amount,
        string Status,
        string Email
    );
}
