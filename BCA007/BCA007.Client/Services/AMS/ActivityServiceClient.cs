using System.Net.Http.Json;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.AMS;

namespace BCA007.Client.Services.AMS
{
    public class ActivityServiceClient : IActivityService
    {
        private readonly HttpClient _http;

        public ActivityServiceClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<ActivityDto> CreateAsync(ActivityDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/activity/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<ActivityDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async  Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/activity/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<ActivityViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ActivityViewDto>>("/api/activity/getall") ?? new List<ActivityViewDto>();
        }

        public async Task<ActivityDto> UpdateAsync(ActivityDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/activity/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<ActivityDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
