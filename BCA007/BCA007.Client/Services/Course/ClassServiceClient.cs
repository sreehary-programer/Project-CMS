using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Course;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Course
{
    public class ClassServiceClient : IClassService
    {
        private readonly HttpClient _http;

        public ClassServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public  async Task<ClassDto> CreateAsync(ClassDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/class/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<ClassDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task<ClassDto> UpdateAsync(ClassDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/class/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<ClassDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/class/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public  async Task<List<ClassViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ClassViewDto>>("/api/class/getall") ?? new List<ClassViewDto>();
        }

    }
}
