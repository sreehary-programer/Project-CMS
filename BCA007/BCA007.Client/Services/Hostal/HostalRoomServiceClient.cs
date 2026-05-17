using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Hostal;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Hostal
{
    public class HostalRoomServiceClient : IHostalRoomService
    {
        private readonly HttpClient _http;

        public HostalRoomServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<HostalRoomDto> CreateAsync(HostalRoomDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/HostalRoom/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<HostalRoomDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task<HostalRoomDto> UpdateAsync(HostalRoomDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/HostalRoom/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<HostalRoomDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/HostalRoom/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<HostalOccupiedViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<HostalOccupiedViewDto>>("/api/HostalRoom/getall") ?? new List<HostalOccupiedViewDto>();
        }

       
    }
}
