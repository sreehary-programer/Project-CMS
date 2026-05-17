using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Hostal;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Hostal
{
    public class HostaltServiceClient : IHostaltService
    {
        private readonly HttpClient _http;

        public HostaltServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<HostalRoomDto> CreateAsync(HostalRoomDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Hostalt/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<HostalRoomDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task<HostalRoomDto> UpdateAsync(HostalRoomDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/Hostalt/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<HostalRoomDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Hostalt/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<HostalViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<HostalViewDto>>("/api/Hostalt/getall") ?? new List<HostalViewDto>();
        }
        public async Task DeallocateAsync(string userName)
        {
            var response = await _http.PutAsync($"api/Hostalt/deallocate/{userName}", null);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

    }
}
