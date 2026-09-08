using RoomReserve.Contracts.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Services
{
    public interface IEmailSender
    {
        Task SendBookingConfirmationAsync(string email, string subject, string body);
    }
}
