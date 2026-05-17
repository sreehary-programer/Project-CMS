using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Course;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Course
{
    public class SemesterServiceClient : ISemesterService
    {
        private readonly HttpClient _http;

        public SemesterServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<SemesterDto> CreateAsync(SemesterDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Semester/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<SemesterDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Semester/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<SemesterDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<SemesterDto>>("/api/Semester/getall") ?? new List<SemesterDto>();
        }

        public async Task<SemesterDto> UpdateAsync(SemesterDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/Semester/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<SemesterDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
