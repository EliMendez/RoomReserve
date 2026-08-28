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
    public class RoomServiceClient: IRoomServiceClient
    {
        private readonly HttpClient _httpClient;

        public RoomServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RoomDto?> GetRoomByIdAsync(int roomId)
        {
            var response = await _httpClient.GetAsync($"api/rooms/{roomId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<RoomDto>();
        }
    }
}
