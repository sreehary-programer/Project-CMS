using BCA007.Shared.DTOs;
using BCA007.Shared.Service.AMS;
using System.Net.Http.Json;

namespace BCA007.Client.Services.AMS
{
    public class TimeTableServiceClient : ITimeTableService
    {
        private readonly HttpClient _http;

        public TimeTableServiceClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<TimetableDto> CreateAsync(TimetableDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/timetable/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<TimetableDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/timetable/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<TimetableViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<TimetableViewDto>>("/api/timetable/getall") ?? new List<TimetableViewDto>();
        }

        public async Task<TimetableDto> UpdateAsync(TimetableDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/timetable/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<TimetableDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
