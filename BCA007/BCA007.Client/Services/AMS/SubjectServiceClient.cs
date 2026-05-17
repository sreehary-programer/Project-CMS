using System.Net.Http.Json;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.AMS;

namespace BCA007.Client.Services.AMS
{
    public class SubjectServiceClient:ISubjectService
    {
        private readonly HttpClient _http;
        public SubjectServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<SubjectDto> CreateAsync(SubjectDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/subject/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<SubjectDto>()
                ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/subject/delete/{id}");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<SubjectDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<SubjectDto>>("/api/subject/getall") ?? [];
        }

        public async Task<SubjectDto> UpdateAsync(SubjectDto dto)
        {
            var response = await _http.PutAsJsonAsync("/api/subject/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<SubjectDto>()
                ?? throw new ApplicationException("Invalid server response");
        }

    }
}
