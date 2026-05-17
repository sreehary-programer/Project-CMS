using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Attendance;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Attendance
{
    public class AttendFormServiceClient : IAttendFormService
    {
        private readonly HttpClient _http;

        public AttendFormServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        //public async Task<AttendFormDto> CreateAsync(AttendFormDto dto)
        //{
        //    var response = await _http.PostAsJsonAsync("api/attendform/create", dto);

        //    if (!response.IsSuccessStatusCode)
        //        throw new ApplicationException(await response.Content.ReadAsStringAsync());

        //    return await response.Content.ReadFromJsonAsync<AttendFormDto>()
        //           ?? throw new ApplicationException("Invalid server response");
        //}

        public async Task<AttendFormDto> UpdateAsync(AttendFormDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/attendform/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<AttendFormDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/attendform/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<AttendFormViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<AttendFormViewDto>>("/api/attendform/getall") ?? new List<AttendFormViewDto>();
        }

    }
}
