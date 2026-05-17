using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Student;
using System.Net.Http.Json;

namespace BCA007.Client.Services.Student
{
    public class StudentPaymentServiceClient : IStudentPaymentService
    {
        private readonly HttpClient _httpClient;

        public StudentPaymentServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<StudentFeeDto> AddStudentFeeAsync(StudentFeeDto fee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/StudentPayment/Fees", fee);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudentFeeDto>();
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Server error: {response.StatusCode} - {error}");
        }

        public async Task<StudentPaymentDto> AddStudentPaymentAsync(StudentPaymentDto payment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/StudentPayment/Payments", payment);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudentPaymentDto>();
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Server error: {response.StatusCode} - {error}");
        }

        public async Task<StudentPaymentDto> UpdateStudentPaymentAsync(StudentPaymentDto payment)
        {
            var response = await _httpClient.PutAsJsonAsync("api/StudentPayment/Payments", payment);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudentPaymentDto>();
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Server error: {response.StatusCode} - {error}");
        }

        public async Task<bool> DeleteStudentPaymentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/StudentPayment/Payments/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<StudentPaymentDto> GetStudentPaymentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<StudentPaymentDto>($"api/StudentPayment/Payments/{id}");
        }

        public async Task<bool> DeleteStudentFeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/StudentPayment/Fees/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<FeeTypeDto>> GetAllFeeTypesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<FeeTypeDto>>("api/StudentPayment/FeeTypes");
        }

        public async Task<IEnumerable<StudentFeeViewDto>> GetAllStudentFeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<StudentFeeViewDto>>("api/StudentPayment/Fees");
        }

        public async Task<IEnumerable<StudentPaymentViewDto>> GetAllStudentPaymentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<StudentPaymentViewDto>>("api/StudentPayment/Payments");
        }

        public async Task<StudentFeeDto> GetStudentFeeByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<StudentFeeDto>($"api/StudentPayment/Fees/{id}");
        }

        public async Task<StudentFeeDto> UpdateStudentFeeAsync(StudentFeeDto fee)
        {
            var response = await _httpClient.PutAsJsonAsync("api/StudentPayment/Fees", fee);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StudentFeeDto>();
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Server error: {response.StatusCode} - {error}");
        }
        public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<StatusDto>>("api/StudentPayment/Statuses");
        }

        public async Task<bool> UpdateStudentFeeStatusAsync(int id, int statusId)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/StudentPayment/Fees/{id}/Status", statusId);
            return response.IsSuccessStatusCode;
        }
    }
}
