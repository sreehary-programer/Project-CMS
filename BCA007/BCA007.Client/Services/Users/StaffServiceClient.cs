using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Users;

namespace BCA007.Client.Services.Users
{
    public class StaffServiceClient:IStaffService
    {
        private readonly HttpClient _http;

        public StaffServiceClient(HttpClient http)
        {
            _http = http;
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/staff/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task<List<StaffViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<StaffViewDto>>("/api/staff/getall") ?? new List<StaffViewDto>();
        }

        public async Task<StaffDto> CreateAsync(StaffDto dto, Stream? fileStream, string? fileName)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json"), "dto");

            if (fileStream != null && fileName != null)
            {
                content.Add(new StreamContent(fileStream), "file", fileName);
            }

            var response = await _http.PostAsync("/api/staff/create", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<StaffDto>()!;
        }

        public async Task<StaffDto> UpdateAsync(StaffDto dto, Stream? fileStream, string? fileName)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(
                JsonSerializer.Serialize(dto)
                , Encoding.UTF8
                , "application/json"), "dto");

            if (fileStream != null && fileName != null)
            {
                content.Add(new StreamContent(fileStream), "file", fileName);
            }

            var response = await _http.PutAsync("/api/staff/edit", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<StaffDto>()!;
        }
        public async Task UpdatePaymentStatusAsync(int staffId, string? status, DateTime? dueDate)
        {
            var dto = new StaffPaymentStatusUpdateDto { Status = status, DueDate = dueDate };
            var response = await _http.PutAsJsonAsync($"/api/Staff/{staffId}/payment-status", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

    }

}
