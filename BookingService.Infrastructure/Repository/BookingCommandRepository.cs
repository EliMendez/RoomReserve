using BookingService.Application.Interfaces.Repository;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using BookingService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.Repository
{
    public class BookingCommandRepository : IBookingCommandRepository
    {
        private readonly AppDbContext _context;
        public BookingCommandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAvailabilityAsync(
            int roomId, 
            DateOnly startDate, 
            DateOnly endDate, 
            int attendees
        ) {
            return await _context.Bookings
                .Where(x => x.RoomId == roomId && x.BookingDate >= startDate && x.BookingDate <= endDate)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int bookingId)
        {
            return await _context.Bookings.SingleOrDefaultAsync(x => x.BookingId == bookingId);
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
        }

        public void Update(Booking booking)
        {
            _context.Bookings.Update(booking);
        }

        public async Task<bool> HasOverlapAsync(
            int roomId,
            DateOnly BookingDate,
            TimeOnly startTime,
            TimeOnly endTime
        ) {
            return await _context.Bookings.AnyAsync(r =>
                r.RoomId == roomId &&
                r.BookingDate == BookingDate &&
                r.Status != BookingStatus.CANCELLED.ToString() &&
                startTime < r.EndTime &&
                endTime > r.StartTime);
        }
    }
}
