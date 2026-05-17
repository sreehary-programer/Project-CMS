using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.Library;
using BCA007.Shared.Service.Bus;
using BCA007.Shared.Service.Library;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BCA007.Client.Services.Bus
{
    public class BusServiceClient : IBusService
    {
        private readonly HttpClient _http;

        public BusServiceClient(HttpClient http)
        {
            _http = http;
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/Bus/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
        public async Task<BusDto> CreateAsync(BusDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/Bus/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BusDto>()!;
        }

        public async Task<BusDto> UpdateAsync(BusDto dto)
        {
            var response = await _http.PutAsJsonAsync("/api/Bus/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BusDto>()!;
        }
        public async Task<List<BusViewsDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<BusViewsDto>>("/api/Bus/getall") ?? new List<BusViewsDto>();
        }

        public async Task<List<DriverViewDto>> GetDriversAsync()
        {
            return await _http.GetFromJsonAsync<List<DriverViewDto>>("/api/Bus/getdrivers") ?? new List<DriverViewDto>();
        }
    }
}
