using System.Net.Http.Json;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.AMS;
using static System.Net.WebRequestMethods;

namespace BCA007.Client.Services.AMS
{
    public class ResultServiceClient : IResultService
    {
        private readonly HttpClient _http;

        public ResultServiceClient(HttpClient http)
        {
            _http = http;
        }

        public async Task UpdateResults(List<ResultViewDto> dto)
        {
            var response = await _http.PutAsJsonAsync("api/result/update", dto);
            response.EnsureSuccessStatusCode();
        }


        public async Task<List<ResultViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ResultViewDto>>("/api/result/getall") ?? new List<ResultViewDto>();
        }

        public async Task<List<ResultViewDto>> GetResultsByExamId(int? examId)
        {
            return await _http.GetFromJsonAsync<List<ResultViewDto>>
                ($"api/result/byexam/{examId}")
                ?? new List<ResultViewDto>();
        }

        public async Task DeleteAsync(int id)
        {
            await _http.DeleteAsync($"api/result/{id}");
        }
    }
}
