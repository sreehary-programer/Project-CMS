using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Hostal;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Hostal
{
    public class HostelServiceClient : IHostalServices
    {
        private readonly HttpClient _http;

        public HostelServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<HostalDto> CreateAsync(HostalDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Hostal/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<HostalDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task<HostalDto> UpdateAsync(HostalDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/Hostal/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<HostalDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Hostal/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<HostalDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<HostalDto>>("/api/Hostal/getall") ?? new List<HostalDto>();
        }
    }
}
