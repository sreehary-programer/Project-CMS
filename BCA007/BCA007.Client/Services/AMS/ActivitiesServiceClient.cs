using System.Net.Http.Json;
using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.AMS;

namespace BCA007.Client.Services.AMS
{
    public class ActivitiesServiceClient : IActivitiesService
    {
        private readonly HttpClient _http;

        public ActivitiesServiceClient(HttpClient http)
        {
           _http = http;
        }

        public async Task<ActivitiesDto> CreateAsync(ActivitiesDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/activities/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<ActivitiesDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/activities/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<ActivitiesDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ActivitiesDto>>("/api/activities/getall") ?? new List<ActivitiesDto>();
        }

        public async Task<ActivitiesDto> UpdateAsync(ActivitiesDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/activities/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<ActivitiesDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
