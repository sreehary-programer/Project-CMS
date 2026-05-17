using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Course;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Course
{
    public class DivisionServiceClient : IDivisionService
    {
        private readonly HttpClient _http;

        public DivisionServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<DivisionDto> CreateAsync(DivisionDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/Division/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<DivisionDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/Division/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<DivisionDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<DivisionDto>>("/api/Division/getall") ?? new List<DivisionDto>();
        }

        public async Task<DivisionDto> UpdateAsync(DivisionDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/Division/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<DivisionDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
