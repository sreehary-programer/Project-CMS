using System.Net.Http.Json;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.AMS;

namespace BCA007.Client.Services.AMS
{
    public class ExamServiceClient : IExamService
    {
        private readonly HttpClient _http;

        public ExamServiceClient(HttpClient http)
        {
            _http = http;
        }
        //public async Task<ExamTimeTableDto> CreateAsync(ExamTimeTableDto dto)
        //{
        //    var response = await _http.PostAsJsonAsync("/api/exam/create", dto);

        //    if (!response.IsSuccessStatusCode)
        //        throw new ApplicationException(await response.Content.ReadAsStringAsync());

        //    return await response.Content.ReadFromJsonAsync<ExamTimeTableDto>()
        //           ?? throw new ApplicationException("Invalid server response");
        //}

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/exam/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<ExamTimeTableViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ExamTimeTableViewDto>>("/api/exam/getall") ?? new List<ExamTimeTableViewDto>();
        }

        public async Task<ExamTimeTableDto> UpdateAsync(ExamTimeTableDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/exam/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<ExamTimeTableDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
