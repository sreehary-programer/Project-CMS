using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Users;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Users
{
    public class StaffPaymentServiceClient : IStaffPaymentService
    {
        private readonly HttpClient _httpClient;

        public StaffPaymentServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StaffPaymentDto> AddStaffPaymentAsync(StaffPaymentDto payment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/StaffPayment", payment);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StaffPaymentDto>();
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }

        public async Task<bool> DeleteStaffPaymentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/StaffPayment/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<StaffPaymentViewDto>> GetAllStaffPaymentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<StaffPaymentViewDto>>("api/StaffPayment");
        }

        public async Task<StaffPaymentDto> GetStaffPaymentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<StaffPaymentDto>($"api/StaffPayment/{id}");
        }

        public async Task<StaffPaymentDto> UpdateStaffPaymentAsync(StaffPaymentDto payment)
        {
            var response = await _httpClient.PutAsJsonAsync("api/StaffPayment", payment);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StaffPaymentDto>();
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }
}
