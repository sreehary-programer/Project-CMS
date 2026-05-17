using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Users;

namespace BCA007.Client.Services.Users
{
    public class ParentServiceClient : IParentService
    {
        private readonly HttpClient _http;

        public ParentServiceClient(HttpClient http)
        {
            _http = http;
        }
        public async Task<ParentDto> CreateAsync(ParentDto dto, Stream? fileStream, string? fileName)
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

            var response = await _http.PostAsync("/api/parent/create", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<ParentDto>()!;
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/parent/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }

        public async Task<List<ParentViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ParentViewDto>>("/api/parent/getall") ?? new List<ParentViewDto>();
        }

        public async Task<ParentDto> UpdateAsync(ParentDto dto, Stream? fileStream, string? fileName)
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

            var response = await _http.PutAsync("/api/parent/edit", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<ParentDto>()!;
        }
    }
}
