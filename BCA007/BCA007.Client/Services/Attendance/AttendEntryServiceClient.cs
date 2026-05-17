using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Attendance;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Attendance
{
    public class AttendEntryServiceClient : IAttendEntryService
    {
        private readonly HttpClient _http;

        public AttendEntryServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<AttendEntryDto> CreateAsync(AttendEntryDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/attendentry/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<AttendEntryDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task<AttendEntryDto> UpdateAsync(AttendEntryDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/attendentry/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<AttendEntryDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/attendentry/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<AttendEntryViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<AttendEntryViewDto>>("/api/attendentry/getall") ?? new List<AttendEntryViewDto>();
        }

        public async Task<List<AttendEntryViewDto>> GetAttendanceById(int? FrmId)
        {
            return await _http.GetFromJsonAsync<List<AttendEntryViewDto>>
                ($"api/attendentry/byattend/{FrmId}")
                ?? new List<AttendEntryViewDto>();
        }

    }
}
