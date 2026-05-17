using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Users;

namespace BCA007.Client.Services.Users
{
    public class AdminServiceClient : IAdminService
    {
        private readonly HttpClient _http;

        public AdminServiceClient(HttpClient http)
        {
            _http = http;
        }
        public async Task<AdminDto> CreateAsync(AdminDto dto, Stream? fileStream, string? fileName)
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

            var response = await _http.PostAsync("/api/admin/create", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<AdminDto>()!;
        }

        public async Task DeleteAsync(int id)
        {

            var response = await _http.DeleteAsync($"/api/admin/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task<List<AdminViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<AdminViewDto>>("/api/admin/getall") ?? new List<AdminViewDto>();
        }

        public async Task<AdminDto> UpdateAsync(AdminDto dto, Stream? fileStream, string? fileName)
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

            var response = await _http.PutAsync("/api/admin/edit", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<AdminDto>()!;
        }
    }
}
