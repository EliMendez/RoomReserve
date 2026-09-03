using Dapper;
using BookingService.Domain.Entities;
using BookingService.Domain.Enums;
using BookingService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BookingService.Application.Interfaces.Repository;

namespace BookingService.Infrastructure.Repository
{
    public class BookingQueryRepository : IBookingQueryRepository
    {
        private readonly IDbConnection _connection;
        public BookingQueryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<int>> GetUnavailabilityAsync(
            DateOnly date,
            TimeOnly startTime, 
            TimeOnly endTime
        ) {

            const string sql = """
                SELECT
                    DISTINCT RoomId
                FROM
                    Bookings
                WHERE
                    Status != @CancelledStatus AND
                    BookingDate = @BookingDate AND
                    StartTime < @EndTime AND
                    EndTime > @StartTime
                """;

            return await _connection.QueryAsync<int>(sql, new
            {
                BookingDate = date.ToDateTime(TimeOnly.MinValue),
                StartTime = startTime.ToTimeSpan(),
                EndTime = endTime.ToTimeSpan(),
                CancelledStatus = BookingStatus.CANCELLED.ToString()
            });
        }
    }
}
