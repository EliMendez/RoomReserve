using BookingService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookingService.Application.Dto.Rooms;

namespace BookingService.Infrastructure.Clients
{
    public class RoomRateServiceClient: IRoomRateServiceClient
    {
        private readonly HttpClient _httpClient;

        public RoomRateServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<RoomRateDto>> GetRoomRatesAsync(int roomId)
        {
            var response = await _httpClient.GetAsync($"api/rooms/{roomId}/rates");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<IEnumerable<RoomRateDto>>();
        }
    }
}
