using BookingService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces
{
    public interface IBookingCommandRepository
    {
        Task AddAsync(Booking booking);
        Task<bool> HasOverlapAsync(int roomId, DateOnly BookingDate, TimeOnly startTime, TimeOnly endTime);
    }
}
