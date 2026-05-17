using BCA007.Shared.DTOs;
using BCA007.Shared.DTOs.Library;
using BCA007.Shared.Service.Bus;
using BCA007.Shared.Service.Library;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Bus
{
    public class BusRouteServiceClient : IBusRouteService
    {

        private readonly HttpClient _http;

        public BusRouteServiceClient(HttpClient _http)
        {
            this._http = _http;
        }
        public async Task<BusRouteDto> CreateAsync(BusRouteDto dto)
        {
            var response = await _http.PostAsJsonAsync("/api/BusRoute/create", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BusRouteDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/BusRoute/delete/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<BusRouteDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<BusRouteDto>>("/api/BusRoute/getall") ?? new List<BusRouteDto>();
        }

        public async Task<BusRouteDto> UpdateAsync(BusRouteDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/BusRoute/edit", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BusRouteDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
