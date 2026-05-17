using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.AMS;
using BCA007.Shared.Service.Attendance;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Period
{
    public class PeriodServiceClient : IPeriodService
    {
        private readonly HttpClient _http;

        public PeriodServiceClient(HttpClient _http)
        {
            this._http = _http;
        }

        public async Task<List<PeriodDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<PeriodDto>>("/api/period/getall") ?? new List<PeriodDto>();
        }
        public async Task<PeriodDto> CreateAsync(PeriodDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/period/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<PeriodDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task<PeriodDto> UpdateAsync(PeriodDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/period/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<PeriodDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/period/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }
        public async Task<int> GetIntegerValue()
        {
            var tst= await _http.GetFromJsonAsync<int>("/api/period/test"); 
            
            return tst;
        }

        public async Task<string> GetStringValue()
        {
            string tst = await _http.GetStringAsync("/api/period/teststr") ??"";
            return tst ;
        }
    }
}
