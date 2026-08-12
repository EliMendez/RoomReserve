using Dapper;
using RoomService.Application.Dto;
using RoomService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Infrastructure.Repository.Rooms
{
    public class RoomQueryRepository : IRoomQueryRepository
    {
        private readonly IDbConnection _connection;

        public RoomQueryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<RoomDto>> GetActiveRoomsAsync()
        {
            const string sql = """
                SELECT 
                    RoomId,
                    Name,
                    Description,
                    Capacity,
                    PricePerHour,
                    Status
                FROM Rooms
                WHERE
                    Status = 'ACTIVE'
                ORDER BY RoomId DESC;
                """;

            return await _connection.QueryAsync<RoomDto>(sql);
        }

        public async Task<RoomDto?> GetByIdAsync(int roomId)
        {
            const string sql = """
                SELECT 
                    RoomId,
                    Name,
                    Description,
                    Capacity,
                    PricePerHour,
                    Status
                FROM Rooms
                WHERE
                    RoomId = @RoomId;
                """;

            return await _connection.QueryFirstOrDefaultAsync<RoomDto>(sql, new { RoomId = roomId});
        }
    }
}
