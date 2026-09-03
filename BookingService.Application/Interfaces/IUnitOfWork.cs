using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingService.Application.Interfaces.Repository;

namespace BookingService.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IBookingCommandRepository Bookings { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
