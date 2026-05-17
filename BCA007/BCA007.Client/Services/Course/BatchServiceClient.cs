using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Course;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Course
{
    public class BatchServiceClient : IBatchService
    {
        private readonly HttpClient _http;

        public BatchServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<BatchDto> CreateAsync(BatchDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Batch/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BatchDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/batch/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<BatchDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<BatchDto>>("/api/batch/getall") ?? new List<BatchDto>();
        }

        public async Task<BatchDto> UpdateAsync(BatchDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/batch/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BatchDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
