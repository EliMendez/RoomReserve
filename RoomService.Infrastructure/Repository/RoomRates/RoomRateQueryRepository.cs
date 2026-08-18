using Dapper;
using RoomService.Application.Dto.RoomRates;
using RoomService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Infrastructure.Repository.RoomRates
{
    public class RoomRateQueryRepository : IRoomRateQueryRepository
    {
        private readonly IDbConnection _connection;

        public RoomRateQueryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<RoomRateDto>> GetAllAsync(int roomId)
        {
            const string sql = """
                SELECT 
                    RoomRateId,
                    RoomId,
                    StartTime,
                    EndTime,
                    PricePerHour
                FROM RoomRates
                WHERE RoomId = @RoomId
                ORDER BY RoomRateId DESC;
                """;

            return await _connection.QueryAsync<RoomRateDto>(sql, new { RoomId = roomId });
        }
    }
}
