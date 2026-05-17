using BCA007.Shared.DTOs.Library;
using BCA007.Shared.Service.Bus;
using BCA007.Shared.Service.Library;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Bus
{
    public class BusAssignmentServiceClient : IBusAssignmentService
    {
        private readonly HttpClient _http;

        public BusAssignmentServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<BusAssignmentDto> CreateAsync(BusAssignmentDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/BusAssignment/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BusAssignmentDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/BusAssignment/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<BusAssignmentDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<BusAssignmentDto>>("/api/BusAssignment/getall") ?? new List<BusAssignmentDto>();
        }

        public async Task<BusAssignmentDto> UpdateAsync(BusAssignmentDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/BusAssignment/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BusAssignmentDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}