using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Library;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BCA007.Client.Services.Library
{
    public class BookServiceClient : IBookService
    {
        private readonly HttpClient _http;

        public BookServiceClient(HttpClient http)
        {
            _http = http;
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/Book/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
        }
        public async Task<BookDto> CreateAsync(BookDto dto, Stream? fileStream, string? fileName)
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

            var response = await _http.PostAsync("/api/Book/create", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookDto>()!;
        }

        public async Task<BookDto> UpdateAsync(BookDto dto, Stream? fileStream, string? fileName)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"), "dto");

            if (fileStream != null && fileName != null)
            {
                content.Add(new StreamContent(fileStream), "file", fileName);
            }

            var response = await _http.PutAsync("/api/Book/edit", content);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookDto>()!;
        }
        public async Task<List<BookViewDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<BookViewDto>>("/api/Book/getall") ?? new List<BookViewDto>();
        }

        public async Task<BookDto> UpdateIssueAsync(BookDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/Book/editissue", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }

        public async Task<BookDto> UpdateReturnAsync(BookDto dto)
        {
            var response = await _http.PutAsJsonAsync($"/api/Book/editissue", dto);

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<BookDto>()
                   ?? throw new ApplicationException("Invalid server response");
        }
    }
}
